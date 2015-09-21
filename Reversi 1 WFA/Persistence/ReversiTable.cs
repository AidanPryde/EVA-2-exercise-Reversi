using System;

namespace Reversi.Persistence
{
    /// <summary>
    /// The type of the square game table.
    /// </summary>
    public class ReversiTable
    {

        #region Constant Default Values

        private const Int32 defaultTableSize = 10; // The default table size.

        #endregion

        #region Fields

        private Int32 _tableSize; // The size of the table.
        private Int32[,] _tableValues; // The table itself.

        private Int32 _player1Time;
        private Int32 _player2Time;

        #endregion

        #region Properties

        /// <summary>
        /// The query of a '_tableValues' field value.
        /// </summary>
        /// <param name="x">Horizontal coordinate of a #_tableValues#'s field.</param>
        /// <param name="y">Vertical coordinate of a #_tableValues#'s field.</param>
        /// <returns></returns>
        public Int32 this[Int32 x, Int32 y]
        {
            get
            {
                if (x < 0 || x >= _tableSize)
                {
                    throw new ArgumentOutOfRangeException("x", x, "The X coordinate is out of range 0, ... , " + (_tableSize - 1).ToString() + ".");
                }
                if (y < 0 || y >= _tableSize)
                {
                    throw new ArgumentOutOfRangeException("y", y, "The Y coordinate is out of range 0, ... , " + (_tableSize - 1).ToString() + ".");
                }
                return _tableValues[x,y];
            }

            set
            {
                if (x < 0 || x >= _tableSize)
                {
                    throw new ArgumentOutOfRangeException("x", x, "The X coordinate is out of range 0, ... , " + (_tableSize - 1).ToString() + ".");
                }
                if (y < 0 || y >= _tableSize)
                {
                    throw new ArgumentOutOfRangeException("y", y, "The Y coordinate is out of range 0, ... , " + (_tableSize - 1).ToString() + ".");
                }
                _tableValues[x, y] = value;
            }
        }

        public Int32 Size
        {
            get
            {
                return _tableSize;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creating default square game table instance.
        /// </summary>
        public ReversiTable() :
            this(defaultTableSize)
        {
        }

        /// <summary>
        /// Creating game table instance.
        /// </summary>
        /// <param name="tableSize">The size of the square game table.</param>
        public ReversiTable(Int32 tableSize, Int32 player1Time = 0, Int32 player2Time = 0)
        {
            if (tableSize < 0)
            {
                throw new ArgumentOutOfRangeException("tableSize", tableSize, "The size of the table is out of range 0, ... .");
            }
            _tableSize = tableSize;
            _tableValues = new Int32[_tableSize, _tableSize];

            _player1Time = player1Time;
            _player2Time = player2Time;
        }

        #endregion

    }
}
