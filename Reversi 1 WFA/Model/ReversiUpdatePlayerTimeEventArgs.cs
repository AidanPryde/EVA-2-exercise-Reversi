using System;

namespace Reversi.Model
{
    /// <summary>
    /// Type of the Reversi update player time event argument.
    /// </summary>
    public class ReversiUpdatePlayerTimeEventArgs : EventArgs
    {
        private Boolean _isPlayer1TimeOn;
        private Int32 _newTime;

        /// <summary>
        /// Quary of the '_type' field value.
        /// </summary>
        public Boolean IsPlayer1TimeOn
        {
            get
            {
                return _isPlayer1TimeOn;
            }
        }

        /// <summary>
        /// Quary of the '_newTime' field value.
        /// </summary>
        public Int32 NewTime
        {
            get
            {
                return _newTime;
            }
        }

        /// <summary>
        /// Creating Reversi update player time event argument instance.
        /// If the "type" is set to 0, then set both time to 0.
        /// If the "type" is set to 1, then set player 1 time to "newTime".
        /// If the "type" is set to 2, then set player 2 time to "newTime".
        /// </summary>
        /// <param name="type">The type of the update</param>
        /// <param name="newTime">The new time for the update.</param>
        public ReversiUpdatePlayerTimeEventArgs(Boolean isPlayer1TimeOn, Int32 newTime)
        {
            _isPlayer1TimeOn = isPlayer1TimeOn;
            _newTime = newTime;
        }
    }
}
