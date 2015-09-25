using System;

namespace Reversi.Model
{
    /// <summary>
    /// Type of the Reversi update table event argument.
    /// </summary>
    public class ReversiUpdateTableEventArgs : EventArgs
    {
        private Int32 _updatedFieldsCount;
        private Int32[] _updatedFieldsDatas;
        private Int32 _player1Points;
        private Int32 _player2Points;

        /// <summary>
        /// Quary of the '_updatedFieldsCount' field value.
        /// </summary>
        public Int32 UpdatedFieldsCount
        {
            get
            {
                return _updatedFieldsCount;
            }
        }

        /// <summary>
        /// Quary of the '_updatedFieldsDatas' field value.
        /// </summary>
        public Int32[] UpdatedFieldsDatas
        {
            get
            {
                return _updatedFieldsDatas;
            }
        }

        /// <summary>
        /// Quary of the '_player1Points' field value. The actual points of player 1 has.
        /// </summary>
        public Int32 Player1Points
        {
            get
            {
                return _player1Points;
            }
        }

        /// <summary>
        /// Quary of the '_player2Points' field value. The actual points of player 2 has.
        /// </summary>
        public Int32 Player2Points
        {
            get
            {
                return _player2Points;
            }
        }

        /// <summary>
        /// Creating Reversi update table event argument instance.
        /// If the "updatedFieldsSerialNumbers" parameter equels to the max (table size * table size),
        /// then we send the new values only, without the coordinates. Otherwise we send the data as:
        /// X, Y, new data, X, Y, new data, ... .
        /// </summary>
        /// <param name="updatedFieldsCount">The updated fields count.</param>
        /// <param name="updatedFieldsDatas">The updated fields data.</param>
        /// <param name="player1Points">The points of player 1 has.</param>
        /// <param name="player2Points">The points of player 2 has.</param>
        public ReversiUpdateTableEventArgs(Int32 updatedFieldsCount, Int32[] updatedFieldsDatas,
            Int32 player1Points, Int32 player2Points)
        {
            _updatedFieldsCount = updatedFieldsCount;
            _updatedFieldsDatas = updatedFieldsDatas;
            _player1Points = player1Points;
            _player2Points = player2Points;
        }
    }
}
