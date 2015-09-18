using System;
using System.Threading.Tasks;

namespace Reversi.Persistence
{
    /// <summary>
    /// Reversi file handler interface.
    /// </summary>
    public interface IReversiDataAccess
    {
        /// <summary>
        /// Loading file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <returns>A square Reversi game table, uploaded with the read data.</returns>
        Task<ReversiTable> Load(String path);

        /// <summary>
        /// Saving file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="table">A square Reversi game table, that we write into the truncated file.</param>
        Task Save(String fileName, ReversiTable path);
    }
}
