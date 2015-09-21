using System;

namespace Reversi.Persistence
{
    public enum ReversiDataExceptionType { UnknownException, StreamReaderException, ReadLineAsyncException, FormatException }

    /// <summary>
    /// The type of the Reversi data access exception.
    /// </summary>
    public class ReversiDataException : Exception
    {

        #region Fields

        private String _message;
        ReversiDataExceptionType _exceptionTyep;

        #endregion

        #region Properties
        override public String Message
        {
            get
            {
                return _message;
            }
        }
        #endregion

        /// <summary>
        /// Create Reversi data access exception instance.
        /// </summary>
        public ReversiDataException(String source, String message, ReversiDataExceptionType exceptionTyep)
        {
            this.Source = source;
            _message = message;
            _exceptionTyep = exceptionTyep;
        }
    }
}
