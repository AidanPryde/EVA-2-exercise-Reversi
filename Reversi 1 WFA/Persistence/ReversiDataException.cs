using System;

namespace Reversi.Persistence
{
    public enum ReversiDataExceptionType { UnknownException, StreamReaderException, ReadLineAsyncException, FormatException }

    /// <summary>
    /// The type of the Reversi data access exception.
    /// </summary>
    public class ReversiDataException : Exception
    {
        /// <summary>
        /// Create Reversi data access exception instance.
        /// </summary>
        public ReversiDataException(String source, String message, ReversiDataExceptionType exceptionTyep) { }
    }
}
