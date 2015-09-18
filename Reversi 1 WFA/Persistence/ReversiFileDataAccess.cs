using System;
using System.IO;
using System.Threading.Tasks;

namespace Reversi.Persistence
{
    /// <summary>
    /// The type of the Reversi file manager.
    /// </summary>
    public class ReversiFileDataAccess : IReversiDataAccess
    {
        /// <summary>
        /// Loading file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <returns>A square Reversi game table, uploaded with the read data.</returns>
        public async Task<ReversiTable> Load(String path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path)) // opening file
                {
                    ReversiTable table = new ReversiTable(); // creating table
                    
                    return table;
                }
            }
            catch
            {
                throw new ReversiDataException();
            }
        }

        /// <summary>
        /// Saving file.
        /// </summary>
        /// <param name="path">File path.</param>
        /// <param name="table">A square Reversi game table, that we write into the truncated file.</param>
        public async Task Save(String path, ReversiTable table)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // opening file
                {
                    
                }
            }
            catch
            {
                throw new ReversiDataException();
            }
        }
    }
}
