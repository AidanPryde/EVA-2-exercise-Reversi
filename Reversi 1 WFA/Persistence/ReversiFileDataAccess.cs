using System;
using System.IO;
using System.Threading.Tasks;
using System.Security;

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
                    // Read the first line of the file, then split it bye one space.
                    String line = await reader.ReadLineAsync();
                    String[] numbers = line.Split(' ');

                    // Setup values with the processed data. The ordering is set by the file format.
                    Int32 tableSize = Int32.Parse(numbers[0]);
                    Int32 player1Time = Int32.Parse(numbers[1]);
                    Int32 player2Time = Int32.Parse(numbers[2]);

                    ReversiTable table = new ReversiTable(tableSize, player1Time, player2Time); // létrehozzuk a táblát

                    for (Int32 i = 0; i < tableSize; ++i)
                    {
                        // Read a line of the file, then split it bye one space.
                        line = await reader.ReadLineAsync();
                        numbers = line.Split(' ');

                        // Setup values of the 
                        for (Int32 j = 0; j < tableSize; ++j)
                        {
                            table[i, j] = Int32.Parse(numbers[j]);
                        }
                    }

                    return table;
                }
            }
            catch (Exception e)
            {
                // To send the type of the exception to 'ReversiDataException'.
                ReversiDataExceptionType exceptionType = ReversiDataExceptionType.UnknownException;
                //TODO: ArgumentNullException can throne by Prase() and by StreamReader(String). We have to find out which one throwed it.
                // The 'Parse()' function exceptions.
                if (e is ArgumentNullException || e is FormatException || e is OverflowException)
                {
                    exceptionType = ReversiDataExceptionType.FormatException;
                    throw new ReversiDataException(e.Source, e.Message, exceptionType);
                }

                // The 'ReadLineAsync()' function exceptions.
                if (e is ArgumentOutOfRangeException || e is ObjectDisposedException || e is InvalidOperationException)
                {
                    exceptionType = ReversiDataExceptionType.StreamReaderException;
                    throw new ReversiDataException(e.Source, e.Message, exceptionType);
                }

                // The StreamReader(String) constructior exception.
                if (e is ArgumentException || e is ArgumentNullException || e is FileNotFoundException || e is DirectoryNotFoundException || e is IOException)
                {
                    exceptionType = ReversiDataExceptionType.StreamReaderException;
                    throw new ReversiDataException(e.Source, e.Message, exceptionType);
                }

                throw new ReversiDataException(e.Source, e.Message, exceptionType);
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
                    writer.Write(table.Size + " " + table); // kiírjuk a méreteket
                    await writer.WriteLineAsync();
                    for (Int32 i = 0; i < table.Size; i++)
                    {
                        for (Int32 j = 0; j < table.Size; j++)
                        {
                            await writer.WriteAsync(table[i, j] + " "); // kiírjuk az értékeket
                        }
                        await writer.WriteLineAsync();
                    }
                }
            }
            catch (Exception e)
            {
                // To send the type of the exception to 'ReversiDataException'.
                ReversiDataExceptionType exceptionType = ReversiDataExceptionType.UnknownException;
                //TODO: ArgumentNullException can throne by Prase() and by StreamReader(String). We have to find out which one throwed it.
                // The 'Parse()' function exceptions.
                if (e is ArgumentNullException || e is FormatException || e is OverflowException)
                {
                    exceptionType = ReversiDataExceptionType.FormatException;
                    throw new ReversiDataException(e.Source, e.Message, exceptionType);
                }

                // The 'ReadLineAsync()' function exceptions.
                if (e is ArgumentOutOfRangeException || e is ObjectDisposedException || e is InvalidOperationException)
                {
                    exceptionType = ReversiDataExceptionType.StreamReaderException;
                    throw new ReversiDataException(e.Source, e.Message, exceptionType);
                }

                // The StreamReader(String) constructior exception.
                if (e is ArgumentException || e is ArgumentNullException || e is FileNotFoundException || e is DirectoryNotFoundException
                    || e is IOException || e is UnauthorizedAccessException || e is SecurityException)
                {
                    exceptionType = ReversiDataExceptionType.StreamReaderException;
                    throw new ReversiDataException(e.Source, e.Message, exceptionType);
                }

                throw new ReversiDataException(e.Source, e.Message, exceptionType);
            }
        }
    }
}
