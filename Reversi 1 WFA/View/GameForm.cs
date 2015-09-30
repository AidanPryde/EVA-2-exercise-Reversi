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
        private readonly Int32 _gameButtonSize = 25;

        #endregion

        #region Fields

        private IReversiDataAccess _dataAccess;
        private ReversiGameModel _model;
        private Button[,] _buttonGrid;
        private Boolean _saved;
        private AboutMessageForm aboutMessageForm;

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
            // Define the border style of the form to a dialog box.
            //FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            //MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            //MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            //StartPosition = FormStartPosition.CenterScreen;

            _dataAccess = new ReversiFileDataAccess(_supportedGameTableSizesArray);
            _model = new ReversiGameModel(_dataAccess, _tableSizeSettingDefault);
            _model.SetGameEnded += new EventHandler<ReversiSetGameEndedEventArgs>(Model_SetGameEnded);
            _model.UpdatePlayerTime += new EventHandler<ReversiUpdatePlayerTimeEventArgs>(Model_UpdatePlayerTime);
            _model.UpdateTable += new EventHandler<ReversiUpdateTableEventArgs>(Model_UpdateTable);
            _saved = true;
        }

        private void fileNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setButtonGridUp();

            _model.NewGame();

            fileSaveToolStripMenuItem.Enabled = true;
            _saved = false;
            pauseButton.Enabled = true;
            pauseButton.Text = "Pause";
        }

        private async void fileLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (loadFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGame(loadFileDialog.FileName);
                    fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                    pauseButton.Enabled = true;
                }
                catch (ReversiDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                }
            }
        }

        private async void fileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGame(saveFileDialog.FileName);
                    _saved = true;
                    pauseButton.Enabled = true;
                }
                catch (ReversiDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void fileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.GamePause();

            if (!_saved)
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Reversi game", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Close();
                }
            }
            else
            {
                Close();
            }
        }

        private void gameSizeSmallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.TableSizeSetting = 10;

            gameSizeSmallToolStripMenuItem.Enabled = false; //TODO: One is useless.
            gameSizeSmallToolStripMenuItem.Checked = true;

            gameSizeMediumToolStripMenuItem.Enabled = true;
            gameSizeMediumToolStripMenuItem.Checked = false;

            gameSizeLargeToolStripMenuItem.Enabled = true;
            gameSizeLargeToolStripMenuItem.Checked = false;
        }

        private void gameSizeMediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.TableSizeSetting = 20;

            gameSizeSmallToolStripMenuItem.Enabled = true; //TODO: One is useless.
            gameSizeSmallToolStripMenuItem.Checked = false;

            gameSizeMediumToolStripMenuItem.Enabled = false;
            gameSizeMediumToolStripMenuItem.Checked = true;

            gameSizeLargeToolStripMenuItem.Enabled = true;
            gameSizeLargeToolStripMenuItem.Checked = false;
        }

        private void gameSizeLargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.TableSizeSetting = 30;

            gameSizeSmallToolStripMenuItem.Enabled = true; //TODO: One is useless.
            gameSizeSmallToolStripMenuItem.Checked = false;

            gameSizeMediumToolStripMenuItem.Enabled = true;
            gameSizeMediumToolStripMenuItem.Checked = false;

            gameSizeLargeToolStripMenuItem.Enabled = false;
            gameSizeLargeToolStripMenuItem.Checked = true;
        }

        private void helpRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, " Always the white starts the game. If he can he chooses a put down location, only if he can not he passes. Then the black do the same then the white again, and so on ... . \n You have to straddle the enemy put downs to make a put down and to make them yours. You can do it in all directions. The game ends if no one can make a put down. The player with the more put downs win.", "Reversi game", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void helpAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aboutMessageForm == null)
            {
                aboutMessageForm = new AboutMessageForm();
            }
            
            aboutMessageForm.ShowDialog();
        }

        private void passButton_Click(object sender, EventArgs e)
        {
            
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (pauseButton.Text == "Pause")
            {
                _model.GamePause();
                pauseButton.Text = "Unpause";
            }
            else if (pauseButton.Text == "Unpause")
            {
                _model.GameUnpause();
                pauseButton.Text = "Pause";
            }
        }

        private void gameButton_Clicked(object sender, EventArgs e)
        {
            _saved = false;
        }

        #endregion

        #region Model event handlers

        private void Model_SetGameEnded(Object sender, ReversiSetGameEndedEventArgs e)
        {

        }

        private void Model_UpdatePlayerTime(Object sender, ReversiUpdatePlayerTimeEventArgs e)
        {

        }

        private void Model_UpdateTable(Object sender, ReversiUpdateTableEventArgs e)
        {

        }

        #endregion

        #region Private method
        private void setButtonGridUp()
        {
            if (_buttonGrid == null || _model.TableSizeSetting != _buttonGrid.GetLength(0))
            {
                _buttonGrid = new Button[_model.TableSizeSetting, _model.TableSizeSetting];
                bottomButtonPanel.Height = (_gameButtonSize + 2) * _model.TableSizeSetting;
                bottomButtonPanel.Width = bottomButtonPanel.Height;
                Int32 widthDifferencia = mainFlowLayoutPanel.Width - bottomButtonPanel.Width;
                if (widthDifferencia > 0)
                {

                }
                else if (widthDifferencia < 0)
                {

                }
            }

            _buttonGrid[0, 0] = new Button();
            _buttonGrid[0, 0].Text = "aa";
            //_buttonGrid[0, 0].Location = new Point(150 + x + 50, 30 + y * 50);
            _buttonGrid[0, 0].Size = new Size(25, 25); // méret
            _buttonGrid[0, 0].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold); // betűtípus
            _buttonGrid[0, 0].Enabled = false; // kikapcsolt állapot
            _buttonGrid[0, 0].TabIndex = 100 + 0 * _model.TableSizeSetting + 0; // a gomb számát a TabIndex-ben tároljuk
            _buttonGrid[0, 0].FlatStyle = FlatStyle.Flat; // lapított stípus
            _buttonGrid[0, 0].MouseClick += new MouseEventHandler(gameButton_Clicked);
            _buttonGrid[0, 0].Text = String.Empty;
            _buttonGrid[0, 0].Enabled = false;
            _buttonGrid[0, 0].BackColor = Color.White;
            bottomButtonPanel.Controls.Add(_buttonGrid[0, 0]);

            _buttonGrid[0, 1] = new Button();
            _buttonGrid[0, 1].Text = "aa";
            _buttonGrid[0, 1].Location = new Point(150 + 0 + 50, 30 + 1 * 50);
            _buttonGrid[0, 1].Size = new Size(30, 30); // méret
            _buttonGrid[0, 1].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold); // betűtípus
            _buttonGrid[0, 1].Enabled = false; // kikapcsolt állapot
            _buttonGrid[0, 1].TabIndex = 100 + 0 * _model.TableSizeSetting + 1; // a gomb számát a TabIndex-ben tároljuk
            _buttonGrid[0, 1].FlatStyle = FlatStyle.Flat; // lapított stípus
            _buttonGrid[0, 1].MouseClick += new MouseEventHandler(gameButton_Clicked);
            _buttonGrid[0, 1].Text = String.Empty;
            _buttonGrid[0, 1].Enabled = false;
            _buttonGrid[0, 1].BackColor = Color.White;
            bottomButtonPanel.Controls.Add(_buttonGrid[0, 1]);

            /*for (Int32 x = 0; x < _model.TableSizeSetting; ++x)
            {
                for (Int32 y = 0; y < _model.TableSizeSetting; ++y)
                {
                    _buttonGrid[x, y] = new Button();
                    _buttonGrid[x, y].Text = "aa";
                    //_buttonGrid[x, y].Location = new Point(150 + x + 50, 30 + y * 50);
                    _buttonGrid[x, y].Size = new Size(50, 50); // méret
                    _buttonGrid[x, y].Font = new Font(FontFamily.GenericSansSerif, 25, FontStyle.Bold); // betűtípus
                    _buttonGrid[x, y].Enabled = false; // kikapcsolt állapot
                    _buttonGrid[x, y].TabIndex = 100 + x * _model.TableSizeSetting + y; // a gomb számát a TabIndex-ben tároljuk
                    _buttonGrid[x, y].FlatStyle = FlatStyle.Flat; // lapított stípus
                    _buttonGrid[x, y].MouseClick += new MouseEventHandler(gameButton_Clicked);
                    _buttonGrid[x, y].Text = String.Empty;
                    _buttonGrid[x, y].Enabled = false;
                    _buttonGrid[x, y].BackColor = Color.White;
                    bottomButtonPanel.Controls.Add(_buttonGrid[x, y]);
                }
            }*/

        }

        #endregion

    }
}
