using Reversi.Persistence;
using System;
using System.Threading.Tasks;

namespace Reversi.Model
{
    /// <summary>
    /// Type of the Reversi game.
    /// </summary>
    public class ReversiGameModel
    {
        #region Fields

        private IReversiDataAccess _dataAccess;
        private ReversiTable _table;

        #endregion

        #region Properties

        #endregion

        #region Events

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataAccess"></param>
        public ReversiGameModel(IReversiDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _table = new ReversiTable();
        }

        #endregion

        #region Public game methods

        /// <summary>
        /// 
        /// </summary>
        public void NewGame()
        {
            _table = new ReversiTable();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public async Task LoadGame(String path)
        {
            if (_dataAccess == null)
            {
                throw new InvalidOperationException("No data access is provided.");
            }

            _table = await _dataAccess.Load(path);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        public async Task SaveGame(String path)
        {
            if (_dataAccess == null)
            { 
                throw new InvalidOperationException("No data access is provided.");
            }

            await _dataAccess.Save(path, _table);
        }

        #endregion

        #region Private game methods

        #endregion

        #region Private event methods

        #endregion
    }
}
