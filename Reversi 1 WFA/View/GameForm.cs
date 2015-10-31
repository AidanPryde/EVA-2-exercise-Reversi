
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
        private readonly Int32[] _supportedGameTableSizesArray = new Int32[] { 10, 20, 30 };
        /// <summary>
        /// The default table size. It is readonly.
        /// </summary>
        private readonly Int32 _tableSizeDefaultSetting = 10;
        /// <summary>
        /// The default button size. It is readonly.
        /// </summary>
        private readonly Int32 _gameButtonSize = 30;
        /// <summary>
        /// The primary screen height.
        /// </summary>
        private readonly Int32 _screenHeight = Screen.PrimaryScreen.Bounds.Height;

        #endregion

        #region Fields

        private IReversiDataAccess _dataAccess;
        private ReversiGameModel _model;

        /// <summary>
        /// To know which players turn is on.
        /// </summary>
        private Boolean IsPlayer1TurnOn;

        /// <summary>
        /// The about window type.
        /// </summary>
        private AboutMessageForm _aboutMessageForm;

        // For resizing the window to be symetric with different button sizes.
        private Int32 _topRowMinimumWidth;
        private Int32 _topRowMinimumHeight;
        private Int32 _player1GroupBoxMarginLeftDefault;
        private Int32 _bottomButtonPanelMarginLeftDefault;

        /// <summary>
        ///  The array we saved the active buttons.
        /// </summary>
        private Button[,] _buttonGrid;

        /// <summary>
        /// Helper to know when we can exit, without asked.
        /// </summary>
        private Boolean _saved;

        #endregion

        #region Constructor

        /// <summary>
        /// Generated stuff. Do not change.
        /// </summary>
        public GameForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Form event Handlers

        /// <summary>
        /// It is invoked, when the system build up the components of the window and the window itself.
        /// </summary>
        /// <param name="sender">This object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void GameForm_Load(object sender, EventArgs e)
        {
            // Resizing to be symetric.
            _topRowMinimumWidth = _topFlowLayoutPanelForAll.Width + _topFlowLayoutPanelForAll.Margin.Left + _topFlowLayoutPanelForAll.Margin.Right;
            _topRowMinimumHeight = _topFlowLayoutPanelForAll.Height + _topFlowLayoutPanelForAll.Margin.Top + _topFlowLayoutPanelForAll.Margin.Bottom;
            _player1GroupBoxMarginLeftDefault = _player1GroupBox.Margin.Left;
            _bottomButtonPanelMarginLeftDefault = _bottomButtonPanel.Margin.Left;

            Width = _topRowMinimumWidth + (2 * 8);
            Height = _topRowMinimumHeight + _menuStrip.Height + _statusStrip.Height + 37;

            // Init the data access type with the array, that contain the default table sizes.
            _dataAccess = new ReversiFileDataAccess(_supportedGameTableSizesArray);

            try
            {
                // Init model
                _model = new ReversiGameModel(_dataAccess, _tableSizeDefaultSetting);
            }
            catch (ReversiModelException)
            {
                MessageBox.Show("Model initialization problem." + System.Environment.NewLine + System.Environment.NewLine + "Unsupportable table size given to model. The program will close.", "Reversi");
                Close();
            }

            // Connect the events.
            _model.SetGameEnded += new EventHandler<ReversiSetGameEndedEventArgs>(model_SetGameEnded);
            _model.UpdatePlayerTime += new EventHandler<ReversiUpdatePlayerTimeEventArgs>(model_UpdatePlayerTime);
            _model.UpdateTable += new EventHandler<ReversiUpdateTableEventArgs>(model_UpdateTable);

            // We let the user exit, without asking anything at this point.
            _saved = true;
        }

        /// <summary>
        /// The new game menu item clicked event handler.
        /// </summary>
        /// <param name="sender">The _fileNewToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void fileNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // Init new game. With the preset table size or the default.
            _model.NewGame();

            _fileSaveToolStripMenuItem.Enabled = true;
            _saved = false;
            _pauseButton.Enabled = true;
            _pauseButton.Text = "Pause";
        }

        /// <summary>
        /// The load game menu item clicked event handler.
        /// </summary>
        /// <param name="sender">The _fileLoadToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private async void fileLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_loadFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.LoadGame(_loadFileDialog.FileName);
                    _fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                    _pauseButton.Enabled = true;
                }
                catch (ReversiDataException ex)
                {
                    MessageBox.Show(ex.ReversiMessage + System.Environment.NewLine + System.Environment.NewLine + ex.ReversiInfo, "Reversi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Reversi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                }
            }
        }

        /// <summary>
        /// The save game menu item clicked event handler.
        /// </summary>
        /// <param name="sender">The _fileSaveToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private async void fileSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await _model.SaveGame(_saveFileDialog.FileName);
                    _saved = true;
                    _pauseButton.Enabled = true;
                }
                catch (ReversiDataException ex)
                {
                    MessageBox.Show(ex.ReversiMessage + System.Environment.NewLine + System.Environment.NewLine + ex.ReversiInfo, "Reversi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Reversi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                }
            }
        }

        /// <summary>
        /// The exit game menu item clicked event handler.
        /// </summary>
        /// <param name="sender">The _fileExitToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void fileExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.Pause();

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

            _model.Unpause();
        }

        /// <summary>
        /// The set small size menu item clicked event handler. 10.
        /// </summary>
        /// <param name="sender">The _gameSizeSmallToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void gameSizeSmallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.TableSizeSetting = 10;

            _gameSizeSmallToolStripMenuItem.Enabled = false; //TODO: One is useless.
            _gameSizeSmallToolStripMenuItem.Checked = true;

            _gameSizeMediumToolStripMenuItem.Enabled = true;
            _gameSizeMediumToolStripMenuItem.Checked = false;

            _gameSizeLargeToolStripMenuItem.Enabled = true;
            _gameSizeLargeToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// The set medium size menu item clicked event handler. 20.
        /// </summary>
        /// <param name="sender">The _gameSizeMediumToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void gameSizeMediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.TableSizeSetting = 20;

            _gameSizeSmallToolStripMenuItem.Enabled = true; //TODO: One is useless.
            _gameSizeSmallToolStripMenuItem.Checked = false;

            _gameSizeMediumToolStripMenuItem.Enabled = false;
            _gameSizeMediumToolStripMenuItem.Checked = true;

            _gameSizeLargeToolStripMenuItem.Enabled = true;
            _gameSizeLargeToolStripMenuItem.Checked = false;
        }

        /// <summary>
        /// The set large size menu item clicked event handler. 30.
        /// </summary>
        /// <param name="sender">The _gameSizeLargeToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void gameSizeLargeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.TableSizeSetting = 30;

            _gameSizeSmallToolStripMenuItem.Enabled = true; //TODO: One is useless.
            _gameSizeSmallToolStripMenuItem.Checked = false;

            _gameSizeMediumToolStripMenuItem.Enabled = true;
            _gameSizeMediumToolStripMenuItem.Checked = false;

            _gameSizeLargeToolStripMenuItem.Enabled = false;
            _gameSizeLargeToolStripMenuItem.Checked = true;
        }

        /// <summary>
        /// The rules menu item clicked event handler.
        /// </summary>
        /// <param name="sender">The _helpRulesToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void helpRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, " Always the white starts the game. If he can he chooses a put down location, only if he can not he passes. Then the black do the same then the white again, and so on ... ." + Environment.NewLine + "You have to straddle the enemy put downs to make a put down and to make them yours. You can do it in all directions. The game ends if no one can make a put down. The player with the more put downs win.", "Reversi game", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        /// <summary>
        /// The about menu item clicked event handler.
        /// </summary>
        /// <param name="sender">The _helpAboutToolStripMenuItem object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void helpAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_aboutMessageForm == null)
            {
                _aboutMessageForm = new AboutMessageForm();
            }
            
            _aboutMessageForm.ShowDialog();
        }

        /// <summary>
        /// The pass button click event handler.
        /// </summary>
        /// <param name="sender">The _passButton object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void passButton_Click(object sender, EventArgs e)
        {
            _passButton.Enabled = false;
            _model.Pass();
            _saved = false;
        }

        /// <summary>
        /// The pause button click event handler.
        /// </summary>
        /// <param name="sender">The _pauseButton object, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (_pauseButton.Text == "Pause")
            {
                _pauseButton.Text = "Unpause";
                _model.Pause();
            }
            else if (_pauseButton.Text == "Unpause")
            {
                _pauseButton.Text = "Pause";
                _model.Unpause();
            }
        }

        /// <summary>
        /// All of the game button click event handler.
        /// </summary>
        /// <param name="sender">One of the game button.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void gameButton_Clicked(object sender, EventArgs e)
        {
            _saved = false;
            Button button = (sender as Button);
            Int32 x = (button.TabIndex - 1000) / _model.ActiveTableSize;
            Int32 y = (button.TabIndex - 1000) % _model.ActiveTableSize;

            _model.PutDown(x, y);
        }

        #endregion

        #region Model event handlers

        /// <summary>
        /// The model told us the game is over.
        /// </summary>
        /// <param name="sender">The _model object, we do not use it as a param.</param>
        /// <param name="e">We get the points of the players.</param>
        private void model_SetGameEnded(Object sender, ReversiSetGameEndedEventArgs e)
        {
            _pauseButton.Enabled = false;
            _saved = true;
            _fileSaveToolStripMenuItem.Enabled = false;
            if (e.Player1Points > e.Player2Points)
            {
                MessageBox.Show("Player 1 won." + Environment.NewLine + "Player 1 points: " + e.Player1Points.ToString() + ", player 2 points: " + e.Player2Points.ToString() + ".", "Game Ended");
            }
            else if (e.Player1Points < e.Player2Points)
            {
                MessageBox.Show("Player 2 won." + Environment.NewLine + "Player 1 points: " + e.Player1Points.ToString() + ", player 2 points: " + e.Player2Points.ToString() + ".", "Game Ended");
            }
            else
            {
                MessageBox.Show("It is a tie." + Environment.NewLine + "Player 1 points: " + e.Player1Points.ToString() + ", player 2 points: " + e.Player2Points.ToString() + ".", "Game Ended");
            }
        }

        /// <summary>
        /// The model told us to update one of the player play time.
        /// </summary>
        /// <param name="sender">The _model object, we do not use it as a param.</param>
        /// <param name="e">Contain the indicator bool that tell us which player time needs to updated and the new time.</param>
        private void model_UpdatePlayerTime(Object sender, ReversiUpdatePlayerTimeEventArgs e)
        {
            if (e.IsPlayer1TimeNeedUpdate)
            {
                // Because it was runing on different thread.
                _player1TimeValueLabel.Invoke((MethodInvoker)(() => _player1TimeValueLabel.Text = e.NewTime.ToString()));
            }
            else
            {
                // Because it was runing on different thread.
                _player2TimeValueLabel.Invoke((MethodInvoker)(() => _player2TimeValueLabel.Text = e.NewTime.ToString()));
            }
        }

        /// <summary>
        /// The model told us to update the table.
        /// </summary>
        /// <param name="sender">The _model object, we do not use it as a param.</param>
        /// <param name="e">Read about it at the ReversiUpdateTableEventArgs consturctor.</param>
        private void model_UpdateTable(Object sender, ReversiUpdateTableEventArgs e)
        {
            _toolStripStatusLabel.Text = "Player 1: " + e.Player1Points.ToString() + " -- Player 2: " + e.Player2Points.ToString();

            IsPlayer1TurnOn = e.IsPlayer1TurnOn;
            _passButton.Enabled = e.IsPassingTurnOn;

            if (e.UpdatedFieldsCount == 0)
            {
                setButtonGridUp();

                Int32 index = 0;
                for (Int32 x = 0; x < _model.ActiveTableSize; ++x)
                {
                    for (Int32 y = 0; y < _model.ActiveTableSize; ++y)
                    {
                        updateButtonGrid(x, y, e.UpdatedFieldsDatas[index]);
                        ++index;
                    }
                } 
            }
            else
            {
                for (Int32 i = 0; i < e.UpdatedFieldsCount; i += 3)
                {
                    updateButtonGrid(e.UpdatedFieldsDatas[i], e.UpdatedFieldsDatas[i + 1], e.UpdatedFieldsDatas[i + 2]);
                }
            }
        }

        #endregion

        #region Private method

        /// <summary>
        /// Generate the game buttons if necessary and save them in the _buttonGrid array.
        /// </summary>
        private void setButtonGridUp()
        {
            if (_buttonGrid == null || _model.ActiveTableSize != _buttonGrid.GetLength(0))
            {
                // The deffault game button size. We may have to make them smaller.
                Int32 usedGameButtonSize = _gameButtonSize;

                _bottomButtonPanel.Height = ((usedGameButtonSize - 1) * _model.ActiveTableSize) + 1;
                _bottomButtonPanel.Width = _bottomButtonPanel.Height;

                Int32 needingHeight = _topRowMinimumHeight + _menuStrip.Height + _statusStrip.Height + 37 + _bottomButtonPanel.Height + (2 * 2) + 2;

                Height = needingHeight;

                if (needingHeight > _screenHeight)
                {
                    while (needingHeight > _screenHeight)
                    {
                        usedGameButtonSize -= 2;
                        _bottomButtonPanel.Height = ((usedGameButtonSize - 1) * _model.ActiveTableSize) + 1;
                        _bottomButtonPanel.Width = _bottomButtonPanel.Height;

                        needingHeight = _topRowMinimumHeight + _menuStrip.Height + _statusStrip.Height + 37 + _bottomButtonPanel.Height + (2 * 2) + 2;

                        Height = needingHeight;
                    }
                }

                Int32 widthDifferencia = _topRowMinimumWidth - (_bottomButtonPanel.Width + _bottomButtonPanelMarginLeftDefault + _bottomButtonPanel.Margin.Right);

                if (widthDifferencia > 0)
                {
                    Width = _topRowMinimumWidth + (2 * 8);

                    _bottomButtonPanel.Margin = new Padding(_bottomButtonPanelMarginLeftDefault + (widthDifferencia / 2), _bottomButtonPanel.Margin.Top, _bottomButtonPanel.Margin.Right, _bottomButtonPanel.Margin.Bottom);
                    _player1GroupBox.Margin = new Padding(_player1GroupBoxMarginLeftDefault, _player1GroupBox.Margin.Top, _player1GroupBox.Margin.Right, _player1GroupBox.Margin.Bottom);
                }
                else if (widthDifferencia < 0)
                {
                    Width = _topRowMinimumWidth + (2 * 8) + (-widthDifferencia);

                    _bottomButtonPanel.Margin = new Padding(_bottomButtonPanelMarginLeftDefault, _bottomButtonPanel.Margin.Top, _bottomButtonPanel.Margin.Right, _bottomButtonPanel.Margin.Bottom);
                    _player1GroupBox.Margin = new Padding(_player1GroupBoxMarginLeftDefault + ((-widthDifferencia) / 2), _player1GroupBox.Margin.Top, _player1GroupBox.Margin.Right, _player1GroupBox.Margin.Bottom);
                }

                _bottomButtonPanel.Controls.Clear();
                _buttonGrid = new Button[_model.ActiveTableSize, _model.ActiveTableSize]; //TODO: only create what we need.

                for (Int32 x = 0; x < _model.ActiveTableSize; ++x)
                {
                    for (Int32 y = 0; y < _model.ActiveTableSize; ++y)
                    {
                        //TODO: Make circle buttons inside the grid.
                        _buttonGrid[x, y] = new Button();
                        _buttonGrid[x, y].Location = new Point(x + x * (usedGameButtonSize - 2), y + y * (usedGameButtonSize - 2));
                        _buttonGrid[x, y].Size = new Size(usedGameButtonSize, usedGameButtonSize);
                        _buttonGrid[x, y].Font = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Bold);
                        _buttonGrid[x, y].TabIndex = 1000 + (x * _model.ActiveTableSize) + y;
                        _buttonGrid[x, y].FlatStyle = FlatStyle.Popup;
                        _buttonGrid[x, y].BackColor = Color.YellowGreen;
                        _buttonGrid[x, y].MouseClick += new MouseEventHandler(gameButton_Clicked);
                        _buttonGrid[x, y].Margin = new Padding(0);
                        _buttonGrid[x, y].Padding = new Padding(0);
                        _buttonGrid[x, y].CausesValidation = false;
                        _buttonGrid[x, y].TabStop = false;

                        _bottomButtonPanel.Controls.Add(_buttonGrid[x, y]);
                    }
                }
            }
        }

        /// <summary>
        /// We update a button indicated by the X and Y coordinates with the help of the value.
        /// See more at ReversiGameModel _table field.
        /// </summary>
        /// <param name="x">First coordinate of the button.</param>
        /// <param name="y">Second coordinate of the button.</param>
        /// <param name="value">The value sent by the model.</param>
        private void updateButtonGrid(Int32 x, Int32 y, Int32 value)
        {
            switch (value)
            {
                case -1:
                    _buttonGrid[x, y].Text = "";
                    _buttonGrid[x, y].BackColor = Color.White;
                    _buttonGrid[x, y].Enabled = false;
                    break;

                case 0:
                    _buttonGrid[x, y].Text = "";
                    _buttonGrid[x, y].BackColor = Color.YellowGreen;
                    _buttonGrid[x, y].FlatAppearance.BorderColor = Color.YellowGreen;
                    _buttonGrid[x, y].Enabled = false;
                    break;

                case 1:
                    _buttonGrid[x, y].Text = "";
                    _buttonGrid[x, y].BackColor = Color.Black;
                    _buttonGrid[x, y].Enabled = false;
                    break;

                case 3:
                    //_buttonGrid[x, y].Text = value.ToString();
                    if (!IsPlayer1TurnOn)
                    {
                        _buttonGrid[x, y].Text = "o";
                        _buttonGrid[x, y].ForeColor = Color.Black;
                        _buttonGrid[x, y].Enabled = true;
                    }
                    else
                    {
                        _buttonGrid[x, y].Text = "";
                        _buttonGrid[x, y].BackColor = Color.YellowGreen;
                        _buttonGrid[x, y].Enabled = false;
                    }
                    break;

                case 6:
                    //_buttonGrid[x, y].Text = value.ToString();
                    if (IsPlayer1TurnOn)
                    {
                        _buttonGrid[x, y].Text = "o";
                        _buttonGrid[x, y].ForeColor = Color.White;
                        _buttonGrid[x, y].Enabled = true;
                    }
                    else
                    {
                        _buttonGrid[x, y].Text = "";
                        _buttonGrid[x, y].BackColor = Color.YellowGreen;
                        _buttonGrid[x, y].Enabled = false;
                    }
                    break;

                case 4:
                    //_buttonGrid[x, y].Text = value.ToString();
                    _buttonGrid[x, y].Text = "o";
                    if (IsPlayer1TurnOn)
                    {
                        _buttonGrid[x, y].ForeColor = Color.White;
                    }
                    else
                    {
                        _buttonGrid[x, y].ForeColor = Color.Black;
                    }
                    //_buttonGrid[x, y].ForeColor = Color.Gray;
                    _buttonGrid[x, y].Enabled = true;
                    break;

                case 5:
                    _buttonGrid[x, y].Text = "";

                    _buttonGrid[x, y].BackColor = Color.YellowGreen;
                    _buttonGrid[x, y].Enabled = false;
                    break;

                default:
                    throw new Exception("Model gave us a number, that we was not ready for, while updating the table view.");
            }
        }

        #endregion

    }
}
