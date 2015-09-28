using Reversi.Model;
using Reversi.Persistence;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reversi.View
{
    /// <summary>
    /// Game window type
    /// </summary>
    public partial class GameForm : Form
    {

        #region Constant Default Values

        /// <summary>
        /// Array of the allowed table sizes. It is readonly.
        /// </summary>
        public readonly Int32[] _supportedGameTableSizesArray = new int[] { 10, 20, 30 };
        /// <summary>
        ///The default table size. It is readonly.
        /// </summary>
        private readonly Int32 _tableSizeSettingDefault = 10;

        #endregion

        #region Fields

        private IReversiDataAccess _dataAccess;
        private ReversiGameModel _model;
        private Button[,] _buttonGrid;

        #endregion

        #region Constructor

        public GameForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form event Handlers

        private void GameForm_Load(object sender, EventArgs e)
        {
            _dataAccess = new ReversiFileDataAccess(_supportedGameTableSizesArray);
            _model = new ReversiGameModel(_dataAccess, _tableSizeSettingDefault);
        }

        private void fileNewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gameSizeSmallToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gameSizeMediumToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gameSizeLargeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void passButton_Click(object sender, EventArgs e)
        {

        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }

        #endregion

        
    }
}
