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

        private Int32[,] _table;

        private Boolean _isPlayer1TurnOn;
        private Int32 _player1Points;
        private Int32 _player2Points;

        private Int32[] _possiblePutDownsCoordinates;
        private Int32 _possiblePutDownsCoordinatesCount;

        private Timer _timer;
        private Boolean _isGameStarted;

        private Int32 _tableSizeSetting;

        #endregion

        #region Properties

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

        #region Constructor

        /// <summary>
        /// The Reversi game model constructor. It generates a valid game state.
        /// </summary>
        /// <param name="dataAccess">The data access.</param>
        public ReversiGameModel()
        {
            _dataAccess = new ReversiFileDataAccess(_supportedGameTableSizesArray);
            _tableSizeSetting = _tableSizeSettingDefault;
            _isGameStarted = false;

            _timer = new Timer(1000.0);
            _timer.Elapsed += Timer_Elapsed;
        }

        #endregion

        #region Public game methods

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
            _tableSizeSetting = _data.TableSize;

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

        public void PutDown()
        {
            if (_isGameStarted && _timer.Enabled)
            {

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

        #region Private game methods

        private void InitializeFields(Boolean isLoadedGame)
        {
            if (_data.TableSize != _table.GetLength(0))
            {
                _table = new Int32[_data.TableSize, _data.TableSize];
                _possiblePutDownsCoordinates = new Int32[_data.TableSize * _data.TableSize * 2]; //TODO: It can be smaller. How much?
            }

            Int32[] updatedFieldsDatas = new Int32[_data.TableSize * _data.TableSize];

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
            _table[_data.TableSize - 1, _data.TableSize - 1] = 1;
            _table[_data.TableSize, _data.TableSize] = 1;

            // The starting put downs for player 2.
            _table[_data.TableSize - 1, _data.TableSize] = 2;
            _table[_data.TableSize, _data.TableSize - 1] = 2;

            // The possible put down coordinates around the starting points.
            _table[_data.TableSize - 2, _data.TableSize - 2] = 3;
            _possiblePutDownsCoordinates[0] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[1] = _data.TableSize - 2;
            _table[_data.TableSize - 1, _data.TableSize - 2] = 3;
            _possiblePutDownsCoordinates[2] = _data.TableSize - 1;
            _possiblePutDownsCoordinates[3] = _data.TableSize - 2;
            _table[_data.TableSize, _data.TableSize - 2] = 3;
            _possiblePutDownsCoordinates[4] = _data.TableSize;
            _possiblePutDownsCoordinates[5] = _data.TableSize - 2;
            _table[_data.TableSize + 1, _data.TableSize - 2] = 3;
            _possiblePutDownsCoordinates[6] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[7] = _data.TableSize - 2;
            _table[_data.TableSize + 1, _data.TableSize - 1] = 3;
            _possiblePutDownsCoordinates[8] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[9] = _data.TableSize - 1;
            _table[_data.TableSize + 1, _data.TableSize] = 3;
            _possiblePutDownsCoordinates[10] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[11] = _data.TableSize;
            _table[_data.TableSize + 1, _data.TableSize + 1] = 3;
            _possiblePutDownsCoordinates[12] = _data.TableSize + 1;
            _possiblePutDownsCoordinates[13] = _data.TableSize + 1;
            _table[_data.TableSize, _data.TableSize + 1] = 3;
            _possiblePutDownsCoordinates[14] = _data.TableSize;
            _possiblePutDownsCoordinates[15] = _data.TableSize + 1;
            _table[_data.TableSize - 1, _data.TableSize + 1] = 3;
            _possiblePutDownsCoordinates[16] = _data.TableSize - 1;
            _possiblePutDownsCoordinates[17] = _data.TableSize + 1;
            _table[_data.TableSize - 2, _data.TableSize + 1] = 3;
            _possiblePutDownsCoordinates[18] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[19] = _data.TableSize + 1;
            _table[_data.TableSize - 2, _data.TableSize] = 3;
            _possiblePutDownsCoordinates[20] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[21] = _data.TableSize;
            _table[_data.TableSize - 2, _data.TableSize - 1] = 3;
            _possiblePutDownsCoordinates[22] = _data.TableSize - 2;
            _possiblePutDownsCoordinates[23] = _data.TableSize - 1;

            // The staring points of the players.
            _player1Points = 2;
            _player2Points = 2;

            // We loaded the game.
            if (isLoadedGame)
            {
                _data.UpdateModelField(_table, _possiblePutDownsCoordinatesCount, _possiblePutDownsCoordinates, _isPlayer1TurnOn, _player1Points, _player2Points);
            }
            
            // We started at least one game.
            _isGameStarted = true;
        }

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
        }

        #endregion

    }
}
