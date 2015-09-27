using Reversi.Persistence;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace Reversi.Model
{
    /// <summary>
    /// Type of the Reversi game.
    /// </summary>
    public class ReversiGameModel
    {

        #region Constant Default Values

        public readonly Int32[] _supportedGameTableSizesArray = new int[] { 10, 20, 30 };
        private readonly Int32 _tableSizeSettingDefault = 10;

        #endregion

        #region Fields

        private IReversiDataAccess _dataAccess;

        private ReversiGameDescriptiveData _data;
        //TODO: Is it a state 4 possible?
        /// <summary>
        /// The table itself. Its values can be -1, 0, 1, 2, 3, 4, 5, 6.
        /// The 0 means it is uncharted field. No put downs even near.
        /// The -1 means it is a player 1 put down field.
        /// The 1 means it is a player 2 put down field.
        /// The 3 means it is a field, where player 2 can possible put down.
        /// The 4 means it is a field, where both player can possible put down.
        /// The 5 means it is a field, that is a candidate to be a possible put down in the next turn.
        /// The 6 means it is a field, where player 1 can possible put down.
        /// If we out of range it returns -2. We only check it if we are not sure.
        /// </summary>
        private Int32[,] _table;

        private Boolean _isPlayer1TurnOn;

        /// <summary>
        /// We save the player 1 points at index 0.
        /// We save the remaining put down positions at index 1.
        /// We save the player 2 points at index 2.
        /// </summary>
        private Int32[] _points;

        private Int32[] _possiblePutDownsCoordinates;
        private Int32 _possiblePutDownsCoordinatesCount;

        private Int32[] _reversedPutDownsCoordinates;
        private Int32 _reversedPutDownsCoordinatesCount;

        private Timer _timer;
        private Boolean _isGameStarted;

        private Int32 _tableSizeSetting;

        private Direction[] _allDirections;
        private Direction[] _allReversedDirections;

        #endregion

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public Int32 TableSizeSetting
        {
            set
            {
                for (Int32 i = 0; i < _supportedGameTableSizesArray.GetLength(0); ++i)
                {
                    if (value == _supportedGameTableSizesArray[0])
                    {
                        _tableSizeSetting = value;
                    }
                }

                String supportedGameTableSizesString = "";
                for (Int32 i = 0; i < _supportedGameTableSizesArray.GetLength(0); ++i)
                {
                    supportedGameTableSizesString += _supportedGameTableSizesArray[i].ToString() + " ";
                }

                throw new ArgumentOutOfRangeException("value", value, "Not supported table size. The supported table sizes: " + supportedGameTableSizesString + ".");
            }
        }

        #endregion

        #region Delegates

        private delegate void Direction(ref Int32 x, ref Int32 y);

        #endregion

        #region Events

        /// <summary>
        /// An in game second passed, we need to update one of the person's time on the view.
        /// </summary>
        public event EventHandler<ReversiUpdatePlayerTimeEventArgs> UpdatePlayerTime;

        /// <summary>
        /// The game table changed, we need to update it on the view.
        /// </summary>
        public event EventHandler<ReversiUpdateTableEventArgs> UpdateTable;

        /// <summary>
        /// The game ended, we need to tell it to the view.
        /// </summary>
        public event EventHandler<ReversiSetGameEndedEventArgs> SetGameEnded;

        #endregion

        #region Constructors

        /// <summary>
        /// The Reversi game model constructor. It generates a valid game state.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public ReversiGameModel()
        {
            _dataAccess = new ReversiFileDataAccess();
            _tableSizeSetting = _tableSizeSettingDefault;
            _isGameStarted = false;

            _timer = new Timer(1000.0);
            _timer.Elapsed += Timer_Elapsed;

            _allDirections = new Direction[] { ToUp, ToRightUp, ToRight, ToRightDown, ToDown, ToLeftDown, ToLeft, ToLeftUp };
            _allReversedDirections = new Direction[] { ToDown, ToLeftDown, ToLeft, ToLeftUp, ToUp, ToRightUp, ToRight, ToRightDown };
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Creating new reversi game with the presetted settings.
        /// </summary>
        public void NewGame()
        {
            _timer.Enabled = false;

            _data = new ReversiGameDescriptiveData(_tableSizeSetting);

            InitializeFields(false);

            _timer.Enabled = true;
        }

        /// <summary>
        /// Loading a reversi game. It may have an invalide state. We do not check if it is valid or not.
        /// </summary>
        /// <param name="path">The path to the file, that contains the saved game data.</param>
        public async Task LoadGame(String path)
        {
            _timer.Enabled = false;

            _data = await _dataAccess.Load(path);

            TableSizeSetting = _data.TableSize;
            InitializeFields(true);

            _timer.Enabled = true;
        }

        /// <summary>
        /// Saving the reversi game. It will be a valid save.
        /// </summary>
        /// <param name="path">The path to the file, that we will save our actual game data.</param>
        public async Task SaveGame(String path)
        {
            _timer.Enabled = false;

            await _dataAccess.Save(path, _data);

            _timer.Enabled = true;
        }

        public void PutDown(Int32 x, Int32 y)
        {
            if (_isGameStarted && _timer.Enabled && IsValidIndexes(x, y))
            {
                if (MakePutDown(x, y))
                {
                    _timer.Enabled = false;
                    OnSetGameEnded(new ReversiSetGameEndedEventArgs(_points[0], _points[2]));
                }
            }
        }

        public void GamePause()
        {
            if (_isGameStarted)
            {
                _timer.Enabled = false;
            }
        }

        public void GameUnPause()
        {
            if (_isGameStarted)
            {
                _timer.Enabled = true;
            }
        }

        #endregion

        #region Private methods

        private void InitializeFields(Boolean isLoadedGame)
        {
            if (_data.TableSize != _table.GetLength(0))
            {
                _table = new Int32[_data.TableSize, _data.TableSize];
                _possiblePutDownsCoordinates = new Int32[_data.TableSize * _data.TableSize * 2]; //TODO: It can be smaller. How much?
            }

            for (Int32 x = 0; x < _data.TableSize; ++x)
            {
                for (Int32 y = 0; y < _data.TableSize; ++y)
                {
                    // We clear the table.
                    _table[x, y] = 0;
                }
            }

            _isPlayer1TurnOn = true;

            // The 12 * 2 size for the 12 starting possible put down coordinates.
            _possiblePutDownsCoordinatesCount = 24;

            // The starting put downs for player 1.
            _table[_data.TableSize - 1, _data.TableSize - 1] = -1;
            _table[_data.TableSize, _data.TableSize] = -1;

            // The starting put downs for player 2.
            _table[_data.TableSize - 1, _data.TableSize] = 1;
            _table[_data.TableSize, _data.TableSize - 1] = 1;

            // The possible put down coordinates around the starting points.
            _table[_data.TableSize - 2, _data.TableSize - 2] = 5;
            _possiblePutDownsCoordinates[0] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[1] = _data.TableSize - 2;

            _table[_data.TableSize - 1, _data.TableSize - 2] = 3;
            _possiblePutDownsCoordinates[2] = _data.TableSize - 1;
            _possiblePutDownsCoordinates[3] = _data.TableSize - 2;

            _table[_data.TableSize, _data.TableSize - 2] = 6;
            _possiblePutDownsCoordinates[4] = _data.TableSize;
            _possiblePutDownsCoordinates[5] = _data.TableSize - 2;

            _table[_data.TableSize + 1, _data.TableSize - 2] = 5;
            _possiblePutDownsCoordinates[6] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[7] = _data.TableSize - 2;

            _table[_data.TableSize + 1, _data.TableSize - 1] = 6;
            _possiblePutDownsCoordinates[8] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[9] = _data.TableSize - 1;

            _table[_data.TableSize + 1, _data.TableSize] = 3;
            _possiblePutDownsCoordinates[10] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[11] = _data.TableSize;

            _table[_data.TableSize + 1, _data.TableSize + 1] = 5;
            _possiblePutDownsCoordinates[12] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[13] = _data.TableSize + 1;

            _table[_data.TableSize, _data.TableSize + 1] = 3;
            _possiblePutDownsCoordinates[14] = _data.TableSize;
            _possiblePutDownsCoordinates[15] = _data.TableSize + 1;

            _table[_data.TableSize - 1, _data.TableSize + 1] = 6;
            _possiblePutDownsCoordinates[16] = _data.TableSize - 1;
            _possiblePutDownsCoordinates[17] = _data.TableSize + 1;

            _table[_data.TableSize - 2, _data.TableSize + 1] = 5;
            _possiblePutDownsCoordinates[18] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[19] = _data.TableSize + 1;

            _table[_data.TableSize - 2, _data.TableSize] = 6;
            _possiblePutDownsCoordinates[20] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[21] = _data.TableSize;

            _table[_data.TableSize - 2, _data.TableSize - 1] = 3;
            _possiblePutDownsCoordinates[22] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[23] = _data.TableSize - 1;

            // The staring points of the players.
            _points = new Int32[] { 2, (_data.TableSize * _data.TableSize) - 4, 2 };

            // We loaded the game.
            if (isLoadedGame)
            {
                // We replay the game to see, if it is a valid one, and to update the model fields. 
                for (Int32 i = 0; i < _data.PutDownsCoordinatesCount; i += 2)
                {
                    // Corrupt loaded data.
                    if (!IsValidIndexes(_data[i], _data[i + 1]))
                    {
                        throw new ReversiDataException("asd", "asd", ReversiDataExceptionType.FormatException);
                    }

                    // Make the put down.
                    if (MakePutDown(_data[i], _data[i + 1], false))
                    {
                        throw new ReversiDataException("asd", "asd", ReversiDataExceptionType.FormatException);
                    }
                }
            }

            Int32[] updatedFieldsDatas = new Int32[_data.TableSize * _data.TableSize];

            Int32 k = 0;
            for (Int32 i = 0; i < _allDirections.GetLength(0); ++i)
            {
                for (Int32 j = 0; j < _allDirections.GetLength(0); ++j)
                {
                    updatedFieldsDatas[k] = _table[i, j];
                    ++k;
                }
            }

            OnUpdateTable(new ReversiUpdateTableEventArgs(0, updatedFieldsDatas, _points[0], _points[2]));
            
            // We started at least one game.
            _isGameStarted = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private Boolean MakePutDown(Int32 x, Int32 y, Boolean isUpdateNeeded = true)
        {
            // Updating the table put downs positions. 
            if (_isPlayer1TurnOn) // Player 1 put down.
            {
                if (_table[_data[x], _data[y]] == 3 || _table[_data[x], _data[y]] == 5)
                {
                    _table[_data[x], _data[y]] = 1;

                    for (Int32 i = 0; i < _allDirections.GetLength(0); ++i)
                    {
                        SearchAndReverse(_data[x], _data[y], _allDirections[i], _allReversedDirections[i]);
                    }
                }
                else
                {
                    throw new ReversiDataException("Source 01", "message 01", ReversiDataExceptionType.FormatException);
                }
            }
            else // Player 2 put down.
            {
                if (_table[_data[x], _data[y]] == 4)
                {
                    _table[_data[x], _data[y]] = 2;

                    for (Int32 i = 0; i < _allDirections.GetLength(0); ++i)
                    {
                        SearchAndReverse(_data[x], _data[y], _allDirections[i], _allReversedDirections[i]);
                    }
                }
                else
                {
                    throw new ReversiDataException("Source 02", "message 02", ReversiDataExceptionType.FormatException);
                }
            }

            // Updating the table old possible put downs positions and remove the one that was played on.
            for (Int32 i = 0; i < _possiblePutDownsCoordinatesCount; i += 2)
            {
                if (_table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] == 1
                    || _table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] == 2)
                {
                    _possiblePutDownsCoordinates[i] = _possiblePutDownsCoordinates[_possiblePutDownsCoordinatesCount - 2];
                    _possiblePutDownsCoordinates[i + 1] = _possiblePutDownsCoordinates[_possiblePutDownsCoordinatesCount - 1];
                    _possiblePutDownsCoordinatesCount -= 2;
                }
                else
                {
                    _table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] = 5;
                }

                for (Int32 j = 0; j < _allDirections.GetLength(0); ++j)
                {
                    SearchAndSetPossiblePutDown(_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1], _allDirections[j]);
                }
            }

            // Updating the table new possible put down positions and add them.
            for (Int32 i = 0; i < _allDirections.GetLength(0); ++i)
            {
                if (SearchAndAddThenSetPossiblePutDown(_possiblePutDownsCoordinates[x], _possiblePutDownsCoordinates[y], _allDirections[i]))
                {
                    Int32 xNew = _possiblePutDownsCoordinates[x];
                    Int32 yNew = _possiblePutDownsCoordinates[y];
                    _allDirections[i](ref xNew, ref yNew);
                    _possiblePutDownsCoordinates[_possiblePutDownsCoordinatesCount] = xNew;
                    _possiblePutDownsCoordinates[_possiblePutDownsCoordinatesCount + 1] = yNew;
                    _possiblePutDownsCoordinatesCount += 2;
                }
            }

            Boolean isOver = false;

            // Change the aktív player Boolean, if the other player can make a put down. It is over, if none can make a put donwn.
            for (Int32 i = 0; i < _possiblePutDownsCoordinatesCount; i += 2)
            {
                if (_isPlayer1TurnOn
                    && (_table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] == 4
                    || _table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] == 3))
                {
                    _isPlayer1TurnOn = !_isPlayer1TurnOn;
                    isOver = false;
                    break;
                }
                else if (!_isPlayer1TurnOn
                    && (_table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] == 4
                    || _table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]] == 6))
                {
                    _isPlayer1TurnOn = !_isPlayer1TurnOn;
                    isOver = false;
                    break;
                }
            }

            if (isUpdateNeeded)
            {
                Int32 updatedFieldsDatasCount = ((_possiblePutDownsCoordinatesCount + _reversedPutDownsCoordinatesCount) * 3) / 2;
                Int32[] updatedFieldsDatas = new Int32[updatedFieldsDatasCount];

                for (Int32 i = 0; i < _possiblePutDownsCoordinatesCount; i += 2)
                {
                    updatedFieldsDatas[i] = _possiblePutDownsCoordinates[i];
                    updatedFieldsDatas[i + 1] = _possiblePutDownsCoordinates[i + 1];
                    updatedFieldsDatas[i + 2] = _table[_possiblePutDownsCoordinates[i], _possiblePutDownsCoordinates[i + 1]];
                }

                for (Int32 i = 0; i < _reversedPutDownsCoordinatesCount; i += 2)
                {
                    updatedFieldsDatas[_possiblePutDownsCoordinatesCount + i] = _reversedPutDownsCoordinates[i];
                    updatedFieldsDatas[_possiblePutDownsCoordinatesCount + i + 1] = _reversedPutDownsCoordinates[i + 1];
                    updatedFieldsDatas[_possiblePutDownsCoordinatesCount + i + 2] = _table[_reversedPutDownsCoordinates[i], _reversedPutDownsCoordinates[i + 1]];
                }

                _data[_data.PutDownsCoordinatesCount] = x;
                _data[_data.PutDownsCoordinatesCount + 1] = y;
                _data.PutDownsCoordinatesCount += 2;

                OnUpdateTable(new ReversiUpdateTableEventArgs(updatedFieldsDatasCount, updatedFieldsDatas, _points[0], _points[2]));
            }

            if (isOver)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xFrom"></param>
        /// <param name="yFrom"></param>
        /// <param name="equelsTo"></param>
        /// <param name="changeTo"></param>
        /// <param name="direction"></param>
        /// <param name="reversedDirection"></param>
        private void SearchAndReverse(Int32 xFrom, Int32 yFrom, Direction direction, Direction reversedDirection)
        {
            Int32 valueOriginal = _table[xFrom, yFrom];
            Int32 xOriginal = xFrom;
            Int32 yOriginal = yFrom;

            // Step to the direction.
            direction(ref xFrom, ref yFrom);

            // Only interested if the searched position have the inverz value of the original position.
            if (GetSearchValue(ref xFrom, ref yFrom) == valueOriginal * -1)
            {
                // Step to the direction.
                direction(ref xFrom, ref yFrom);
                while (true)
                {
                    Int32 valueSearch = GetSearchValue(ref xFrom, ref yFrom);

                    if (valueSearch == valueOriginal) // We found the put down, that same us the original.
                    {
                        // We step back.
                        reversedDirection(ref xFrom, ref yFrom);

                        // We reverse all the put downs, we find between here and origin.
                        while (GetSearchValue(ref xFrom, ref yFrom) == valueOriginal * -1)
                        {
                            _table[xFrom, yFrom] = valueOriginal;
                            _reversedPutDownsCoordinates[_reversedPutDownsCoordinatesCount] = xFrom;
                            _reversedPutDownsCoordinates[_reversedPutDownsCoordinatesCount + 1] = yFrom;
                            _reversedPutDownsCoordinates[_reversedPutDownsCoordinatesCount + 2] = valueOriginal;
                            ++_reversedPutDownsCoordinatesCount;

                            ++(_points[valueOriginal + 1]);
                            --(_points[(valueOriginal * -1) + 1]);
                            --_points[1];
                            reversedDirection(ref xFrom, ref yFrom);
                        }
                        return;
                    }
                    else if (valueSearch == valueOriginal * -1) // We find an inverz value of the original position.
                    {
                        // We step to the direction.
                        direction(ref xFrom, ref yFrom);
                    }
                    else
                    {
                        // We run out of the table or we find a possible put down position.
                        return;
                    }
                }
            }
        }

        private void SearchAndSetPossiblePutDown(Int32 xFrom, Int32 yFrom, Direction direction)
        {
            Int32 valueOriginal = _table[xFrom, yFrom];
            Int32 xOriginal = xFrom;
            Int32 yOriginal = yFrom;

            if (valueOriginal == 4)
            {
                return;
            }

            direction(ref xFrom, ref yFrom);
            if (GetSearchValue(ref xFrom, ref yFrom) == 1)
            {
                if (valueOriginal == 3)
                {
                    return;
                }

                direction(ref xFrom, ref yFrom);
                while (true)
                {
                    Int32 valueSearch = GetSearchValue(ref xFrom, ref yFrom);
                    if (valueSearch == 2)
                    {
                        _table[xOriginal, yOriginal] -= 2;
                        return;
                    }
                    else if (valueSearch == 1)
                    {
                        direction(ref xFrom, ref yFrom);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            else if (GetSearchValue(ref xFrom, ref yFrom) == 2)
            {
                if (valueOriginal == 6)
                {
                    return;
                }

                direction(ref xFrom, ref yFrom);
                while (true)
                {
                    Int32 valueSearch = GetSearchValue(ref xFrom, ref yFrom);
                    if (valueSearch == 1)
                    {
                        ++(_table[xOriginal, yOriginal]);
                        return;
                    }
                    else if (valueSearch == 2)
                    {
                        direction(ref xFrom, ref yFrom);
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private Boolean SearchAndAddThenSetPossiblePutDown(Int32 xFrom, Int32 yFrom, Direction direction)
        {
            direction(ref xFrom, ref yFrom);
            if (GetSearchValue(ref xFrom, ref yFrom) == 0)
            {
                _table[xFrom, yFrom] = 5;
                for (Int32 i = 0; i < _allDirections.GetLength(0); ++i)
                {
                    SearchAndSetPossiblePutDown(xFrom, yFrom, _allDirections[i]);
                }

                return true;
            }

            return false;
        }

        private Int32 GetSearchValue(ref Int32 x, ref Int32 y)
        {
            if (x < 0 || x >= _data.TableSize || y < 0 || y >= _data.TableSize)
            {
                return _table[x, y];
            }

            return -2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Boolean IsValidIndexes(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _data.TableSize || y < 0 || y >= _data.TableSize)
            {
                return false;
            }

            return true;
        }

        #region Private delegates methods

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToUp(ref Int32 x, ref Int32 y)
        {
            --x;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToRightUp(ref Int32 x, ref Int32 y)
        {
            --x;
            ++y;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToRight(ref Int32 x, ref Int32 y)
        {
            ++y;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToRightDown(ref Int32 x, ref Int32 y)
        {
            ++x;
            ++y;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToDown(ref Int32 x, ref Int32 y)
        {
            ++x;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToLeftDown(ref Int32 x, ref Int32 y)
        {
            ++x;
            --y;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToLeft(ref Int32 x, ref Int32 y)
        {
            --y;
        }

        /// <summary>
        /// Set a new position from coordinates.
        /// </summary>
        /// <param name="x">The first coordinate.</param>
        /// <param name="y">The second coordinate.</param>
        private void ToLeftUp(ref Int32 x, ref Int32 y)
        {
            --x;
            --y;
        }

        #endregion

        #endregion

        #region Private event methods

        private void OnSetGameEnded(ReversiSetGameEndedEventArgs arg)
        {
            if (SetGameEnded != null)
            {
                SetGameEnded(this, arg);
            }
        }

        private void OnUpdatePlayerTime(ReversiUpdatePlayerTimeEventArgs arg)
        {
            if (UpdatePlayerTime != null)
            {
                UpdatePlayerTime(this, arg);
            }
        }

        private void OnUpdateTable(ReversiUpdateTableEventArgs arg)
        {
            if (UpdateTable != null)
            {
                UpdateTable(this, arg);
            }
        }

        private void Timer_Elapsed(Object sender, ElapsedEventArgs e)
        {
            if(_isPlayer1TurnOn)
            {
                ++(_data.Player1Time);
            }
            else
            {
                ++(_data.Player2Time);
            }
            OnUpdatePlayerTime(new ReversiUpdatePlayerTimeEventArgs(_data.Player1Time, _data.Player2Time));
        }

        #endregion

    }
}
