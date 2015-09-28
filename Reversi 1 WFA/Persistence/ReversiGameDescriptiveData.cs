using System;

namespace Reversi.Persistence
{
    public class ReversiGameDescriptiveData
    {
        #region Fields

        /// <summary>
        /// The size of the game table.
        /// </summary>
        private Int32 _tableSize;

        /// <summary>
        /// The array of the put downs as pair of coordinates.
        /// </summary>
        private Int32[] _putDowns;
        /// <summary>
        /// Helper variable for maintaining '_putDowns' array. The amount of coordinates keeped in the '_putDowns' array.
        /// </summary>
        private Int32 _putDownsCount;

        /// <summary>
        /// Player 1 game time.
        /// </summary>
        private Int32 _player1Time;
        /// <summary>
        /// Player 2 game time.
        /// </summary>
        private Int32 _player2Time;

        #endregion

        #region Properties

        /// <summary>
        /// The query of a '_putDowns' array value, with the [] operator. It gives you one of the coordinate of a put down. 
        /// </summary>
        /// <param name="i">The 'i'. index of a '_putDowns' array.</param>
        /// <returns>The 'i'. value of a '_putDowns' array.</returns>
        public Int32 this[Int32 i]
        {
            get
            {
                return _putDowns[i];
            }

            set
            {
                _putDowns[i] = value;
            }
        }

        /// <summary>
        /// The query of a '_tableSize' field value. Which contains the size of the table.
        /// </summary>
        public Int32 TableSize
        {
            get
            {
                return _tableSize;
            }

            private set
            {
                _tableSize = value;
            }
        }

        /// <summary>
        /// The query of a '_player1Time' field value. Which contains the time player 1 used in seconds.
        /// We do not keep it up to date.
        /// </summary>
        public Int32 Player1Time
        {
            get
            {
                return _player1Time;
            }

            set
            {
                _player1Time = value;
            }
        }

        /// <summary>
        /// The query of a '_player2Time' field value. Which contains the time player 2 used in seconds.
        /// </summary>
        public Int32 Player2Time
        {
            get
            {
                return _player2Time;
            }

            set
            {
                _player2Time = value;
            }
        }

        /// <summary>
        /// The query of a '_putDowns' array size.
        /// </summary>
        public Int32 PutDownsCoordinatesCount
        {
            get
            {
                return _putDownsCount;
            }

            set
            {
                _putDownsCount = value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Creating the game descriptive data instance.
        /// </summary>
        /// <param name="tableSize">The size of the square game table.</param>
        /// <param name="player1Time">The player 1 play time.</param>
        /// <param name="player2Time">The player 2 play time.</param>
        /// <param name="putDownsCount">The put downs coordinates count.</param>
        public ReversiGameDescriptiveData(Int32 tableSize, Int32 player1Time = 0, Int32 player2Time = 0, Int32 putDownsCount = 0)
        {
            _tableSize = tableSize;

            _putDowns = new Int32[(_tableSize * 2) - 4];

            _player1Time = player1Time;
            _player2Time = player2Time;

            _putDownsCount = putDownsCount;
        }

        #endregion

        #region Public methodes

        #endregion

        #region Private methodes

        #endregion

    }
}
