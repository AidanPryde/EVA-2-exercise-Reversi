
using System;

namespace Reversi.Persistence
{
    /// <summary>
    /// The type of the Reversi data access exception.
    /// </summary>
    public class ReversiDataException : Exception
    {

        #region Fields

        private String _message;
        private String _info;

        #endregion

        #region Properties

        public String ReversiMessage
        {
            get
            {
                return _message;
            }
        }

        public String ReversiInfo
        {
            get
            {
                return _message;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create Reversi data access exception instance.
        /// </summary>
        public ReversiDataException(String message, String info)
        {
            _message = message;
            _info = info;
        }

        #endregion

    }
}
