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
        private Int32 _player1PossiblePutDownCount;
        private Int32[] _player1PossiblePutDownCoordinates;
        private Int32 _player2PossiblePutDownCount;
        private Int32[] _player2PossiblePutDownCoordinates;

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
        /// Quary of the '_player1PossiblePutDownCount' field value.
        /// </summary>
        public Int32 Player1PossiblePutDownCount
        {
            get
            {
                return _player1PossiblePutDownCount;
            }
        }

        /// <summary>
        /// Quary of the '_player1PossiblePutDownCoordinates' field value.
        /// </summary>
        public Int32[] Player1PossiblePutDownCoordinates
        {
            get
            {
                return _player1PossiblePutDownCoordinates;
            }
        }

        /// <summary>
        /// Quary of the '_player2PossiblePutDownCount' field value.
        /// </summary>
        public Int32 Player2PossiblePutDownCount
        {
            get
            {
                return _player2PossiblePutDownCount;
            }
        }

        /// <summary>
        /// Quary of the '_player2PossiblePutDownCoordinates' field value.
        /// </summary>
        public Int32[] Player2PossiblePutDownCoordinates
        {
            get
            {
                return _player2PossiblePutDownCoordinates;
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
        /// <param name="player1PossiblePutDownCount">The possible put down places count for player 1.</param>
        /// <param name="player1PossiblePutDownCoordinates">The coordinates of the possible put down places for player 1.</param>
        /// <param name="player2PossiblePutDownCount">The possible put down places count for player 2.</param>
        /// <param name="player2PossiblePutDownCoordinates">The coordinates of the possible put down places for player 2.</param>
        public ReversiUpdateTableEventArgs(Int32 updatedFieldsCount, Int32[] updatedFieldsDatas,
            Int32 player1PossiblePutDownCount, Int32[] player1PossiblePutDownCoordinates,
            Int32 player2PossiblePutDownCount, Int32[] player2PossiblePutDownCoordinates)
        {
            _updatedFieldsCount = updatedFieldsCount;
            _updatedFieldsDatas = updatedFieldsDatas;
            _player1PossiblePutDownCount = player1PossiblePutDownCount;
            _player1PossiblePutDownCoordinates = player1PossiblePutDownCoordinates;
            _player2PossiblePutDownCount = player2PossiblePutDownCount;
            _player2PossiblePutDownCoordinates = player2PossiblePutDownCoordinates;
        }
    }
}
