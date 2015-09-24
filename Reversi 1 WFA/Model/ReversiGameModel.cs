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
        private Int32 _player1Points;
        private Int32 _player2Points;

        private Boolean _isGameStarted;
        private Timer _timer;

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

            if (_data.TableSize != _tableSizeSetting)
            {
                _data = new ReversiGameDescriptiveData(_tableSizeSetting);
            }

            InitializeFields(true);

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

            InitializeFields(false);

            _timer.Enabled = true;
        }

        /// <summary>
        /// Saving the reversi game. It will be a valid save.
        /// </summary>
        /// <param name="path">The path to the file, that we will save our actual game data.</param>
        public async Task SaveGame(String path)
        {
            _timer.Enabled = false;

            await _dataAccess.Save(path, _table);

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

        private void InitializeFields(Boolean isNewGame)
        {
            // We started a new game.
            if (isNewGame)
            {
                for (Int32 x = 0; x < _table.Size; ++x)
                {
                    for (Int32 y = 0; y < _table.Size; ++y)
                    {
                        // We clear the table.
                        _table[x, y] = 0;
                    }
                }

                // The 12 * 2 size for the 12 starting possible put down coordinates.
                _posiblePutDownCoordinatesCount = 24;

                // The starting points for player 1.
                _table[_table.Size - 1, _table.Size - 1] = 1;
                _table[_table.Size, _table.Size] = 1;

                // The starting points for player 2.
                _table[_table.Size - 1, _table.Size] = 2;
                _table[_table.Size, _table.Size - 1] = 2;


                // The possible put down coordinates around the starting points.
                _table[_table.Size - 2, _table.Size - 2] = 3;
                _posiblePutDownCoordinates[0] = _table.Size - 2;
                _posiblePutDownCoordinates[1] = _table.Size - 2;
                _table[_table.Size - 1, _table.Size - 2] = 3;
                _posiblePutDownCoordinates[2] = _table.Size - 1;
                _posiblePutDownCoordinates[3] = _table.Size - 2;
                _table[_table.Size, _table.Size - 2] = 3;
                _posiblePutDownCoordinates[4] = _table.Size;
                _posiblePutDownCoordinates[5] = _table.Size - 2;
                _table[_table.Size + 1, _table.Size - 2] = 3;
                _posiblePutDownCoordinates[6] = _table.Size + 1;
                _posiblePutDownCoordinates[7] = _table.Size - 2;
                _table[_table.Size + 1, _table.Size - 1] = 3;
                _posiblePutDownCoordinates[8] = _table.Size + 1;
                _posiblePutDownCoordinates[9] = _table.Size - 1;
                _table[_table.Size + 1, _table.Size] = 3;
                _posiblePutDownCoordinates[10] = _table.Size + 1;
                _posiblePutDownCoordinates[11] = _table.Size;
                _table[_table.Size + 1, _table.Size + 1] = 3;
                _posiblePutDownCoordinates[12] = _table.Size + 1;
                _posiblePutDownCoordinates[13] = _table.Size + 1;
                _table[_table.Size, _table.Size + 1] = 3;
                _posiblePutDownCoordinates[14] = _table.Size;
                _posiblePutDownCoordinates[15] = _table.Size + 1;
                _table[_table.Size - 1, _table.Size + 1] = 3;
                _posiblePutDownCoordinates[16] = _table.Size - 1;
                _posiblePutDownCoordinates[17] = _table.Size + 1;
                _table[_table.Size - 2, _table.Size + 1] = 3;
                _posiblePutDownCoordinates[18] = _table.Size - 2;
                _posiblePutDownCoordinates[19] = _table.Size + 1;
                _table[_table.Size - 2, _table.Size] = 3;
                _posiblePutDownCoordinates[20] = _table.Size - 2;
                _posiblePutDownCoordinates[21] = _table.Size;
                _table[_table.Size - 2, _table.Size - 1] = 3;
                _posiblePutDownCoordinates[22] = _table.Size - 2;
                _posiblePutDownCoordinates[23] = _table.Size - 1;


                // The staring points of the players.
                _player1Points = 2;
                _player2Points = 2;
            }
            else // We loaded the game.
            {
                _posiblePutDownCoordinatesCount = 0;

                for (Int32 x = 0; x < _table.Size; ++x)
                {
                    for (Int32 y = 0; y < _table.Size; ++y)
                    {
                        if (_table[x, y] == 1) // We count the player 1 points.
                        {
                            ++_player1Points;
                        }
                        else if (_table[x, y] == 2) // We count the player 2 points.
                        {
                            ++_player2Points;
                        }
                        else if (_table[x, y] == 3) // We count the possible put down coordinates and save them.
                        {
                            _posiblePutDownCoordinates[_posiblePutDownCoordinatesCount] = x;
                            _posiblePutDownCoordinates[_posiblePutDownCoordinatesCount + 1] = y;
                            _posiblePutDownCoordinatesCount += 2;
                        }
                    }
                }
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
            if(_table.IsPlayer1sTurn)
            {
                ++(_table.Player1Time);
            }
            else
            {
                ++(_table.Player2Time);
            }
        }

        #endregion

    }
}
