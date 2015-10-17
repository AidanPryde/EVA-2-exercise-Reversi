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

        private Boolean IsPlayer1TurnOn;

        private AboutMessageForm _aboutMessageForm;

        private Int32 _topRowMinimumWidth;
        private Int32 _topRowMinimumHeight;
        private Int32 _player1GroupBoxMarginLeftDefault;
        private Int32 _bottomButtonPanelMarginLeftDefault;

        private Button[,] _buttonGrid;
        private Boolean _saved;

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
            _topRowMinimumWidth = _topFlowLayoutPanelForAll.Width + _topFlowLayoutPanelForAll.Margin.Left + _topFlowLayoutPanelForAll.Margin.Right;
            _topRowMinimumHeight = _topFlowLayoutPanelForAll.Height + _topFlowLayoutPanelForAll.Margin.Top + _topFlowLayoutPanelForAll.Margin.Bottom;
            _player1GroupBoxMarginLeftDefault = _player1GroupBox.Margin.Left;
            _bottomButtonPanelMarginLeftDefault = _bottomButtonPanel.Margin.Left;

            this.Width = _topRowMinimumWidth + (2 * 8);
            this.Height = _topRowMinimumHeight + _menuStrip.Height + _statusStrip.Height + 37;

            _dataAccess = new ReversiFileDataAccess(_supportedGameTableSizesArray);

            _model = new ReversiGameModel(_dataAccess, _tableSizeSettingDefault);
            _model.SetGameEnded += new EventHandler<ReversiSetGameEndedEventArgs>(Model_SetGameEnded);
            _model.UpdatePlayerTime += new EventHandler<ReversiUpdatePlayerTimeEventArgs>(Model_UpdatePlayerTime);
            _model.UpdateTable += new EventHandler<ReversiUpdateTableEventArgs>(Model_UpdateTable);

            _saved = true;
        }

        private void fileNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _model.NewGame();

            _fileSaveToolStripMenuItem.Enabled = true;
            _saved = false;
            _pauseButton.Enabled = true;
            _pauseButton.Text = "Pause";
        }

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
                catch (ReversiDataException)
                {
                    MessageBox.Show("Játék betöltése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a fájlformátum.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _fileSaveToolStripMenuItem.Enabled = true;
                    _saved = true;
                }
            }
        }

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
                catch (ReversiDataException)
                {
                    MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

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
        }

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

        private void helpRulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, " Always the white starts the game. If he can he chooses a put down location, only if he can not he passes. Then the black do the same then the white again, and so on ... . \n You have to straddle the enemy put downs to make a put down and to make them yours. You can do it in all directions. The game ends if no one can make a put down. The player with the more put downs win.", "Reversi game", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void helpAboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_aboutMessageForm == null)
            {
                _aboutMessageForm = new AboutMessageForm();
            }
            
            _aboutMessageForm.ShowDialog();
        }

        private void passButton_Click(object sender, EventArgs e)
        {
            try
            {
                _passButton.Enabled = false;
                _model.Pass();
                _saved = false;
            }
            catch (Exception ex)
            {
                if (ex is ReversiGameException)
                {

                }
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                if (ex is ReversiGameException)
                {

                }
            }
        }

        private void gameButton_Clicked(object sender, EventArgs e)
        {
            _saved = false;
            Button button = (sender as Button);
            Int32 x = (button.TabIndex - 1000) / _model.ActiveTableSize;
            Int32 y = (button.TabIndex - 1000) % _model.ActiveTableSize;
            try
            { 
                _model.PutDown(x, y);
            }
            catch (Exception ex)
            {
                if (ex is ReversiGameException)
                {

                }
            }
        }

        #endregion

        #region Model event handlers

        private void Model_SetGameEnded(Object sender, ReversiSetGameEndedEventArgs e)
        {
            _pauseButton.Enabled = false;
            _saved = true;
            _fileSaveToolStripMenuItem.Enabled = false;
            MessageBox.Show("Game Ended", "Player 1 points: " + e.Player1Points.ToString() + ", player 2 points: " + e.Player2Points.ToString() + ".");
        }

        private void Model_UpdatePlayerTime(Object sender, ReversiUpdatePlayerTimeEventArgs e)
        {
            if (e.IsPlayer1TimeNeedUpdate)
            {
                _player1TimeValueLabel.Invoke((MethodInvoker)(() => _player1TimeValueLabel.Text = e.NewTime.ToString()));
            }
            else
            {
                _player2TimeValueLabel.Invoke((MethodInvoker)(() => _player2TimeValueLabel.Text = e.NewTime.ToString()));
            }
        }

        private void Model_UpdateTable(Object sender, ReversiUpdateTableEventArgs e)
        {
            if (e.UpdatedFieldsCount == 0)
            {
                setButtonGridUp();

                IsPlayer1TurnOn = true;

                Int32 index = 0;
                for (Int32 x = 0; x < _model.ActiveTableSize; ++x)
                {
                    for (Int32 y = 0; y < _model.ActiveTableSize; ++y)
                    {
                        updateButtonGrid(x, y, e.UpdatedFieldsDatas[index]);
                        _buttonGrid[x, y].Text = x.ToString() + " " + y.ToString(); // Help
                        ++index;
                    }
                }
            }
            else
            {
                if (e.IsPassingTurnOn)
                {
                    _passButton.Enabled = true;
                }

                IsPlayer1TurnOn = !IsPlayer1TurnOn;

                for (Int32 i = 0; i < e.UpdatedFieldsCount; i += 3)
                {
                    updateButtonGrid(e.UpdatedFieldsDatas[i], e.UpdatedFieldsDatas[i + 1], e.UpdatedFieldsDatas[i + 2]);
                }
            }

            //_player1TimeValueLabel.Text = e.Player1Points.ToString();
            //_player2TimeValueLabel.Text = e.Player2Points.ToString();
        }

        #endregion

        #region Private method
        private void setButtonGridUp()
        {
            if (_buttonGrid == null || _model.ActiveTableSize != _buttonGrid.GetLength(0))
            {
                _bottomButtonPanel.Controls.Clear();
                _buttonGrid = new Button[_model.ActiveTableSize, _model.ActiveTableSize]; //TODO: only create what we need.

                _bottomButtonPanel.Height = ((_gameButtonSize - 1) * _model.ActiveTableSize) + 1;
                _bottomButtonPanel.Width = _bottomButtonPanel.Height;

                Height = _topRowMinimumHeight + _menuStrip.Height + _statusStrip.Height + 37 + _bottomButtonPanel.Height + (2 * 2) + 2;

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
            
                for (Int32 x = 0; x < _model.ActiveTableSize; ++x)
                {
                    for (Int32 y = 0; y < _model.ActiveTableSize; ++y)
                    {
                        _buttonGrid[x, y] = new Button();
                        _buttonGrid[x, y].Location = new Point(x + x * (_gameButtonSize - 2), y + y * (_gameButtonSize - 2));
                        _buttonGrid[x, y].Size = new Size(_gameButtonSize, _gameButtonSize);
                        _buttonGrid[x, y].Font = new Font(FontFamily.GenericSansSerif, 5, FontStyle.Bold);
                        _buttonGrid[x, y].Enabled = true;
                        _buttonGrid[x, y].TabIndex = 1000 + (x * _model.ActiveTableSize) + y;
                        _buttonGrid[x, y].FlatStyle = FlatStyle.Flat;
                        _buttonGrid[x, y].FlatAppearance.BorderSize = 1;
                        _buttonGrid[x, y].MouseClick += new MouseEventHandler(gameButton_Clicked);
                        _buttonGrid[x, y].BackColor = Color.White;
                        _buttonGrid[x, y].Margin = new Padding(0);
                        _buttonGrid[x, y].Padding = new Padding(0);
                        _buttonGrid[x, y].CausesValidation = false;
                        _buttonGrid[x, y].TabStop = false;

                        _bottomButtonPanel.Controls.Add(_buttonGrid[x, y]);
                    }
                }
            }
        }

        private void updateButtonGrid(Int32 x, Int32 y, Int32 value)
        {
            if (IsPlayer1TurnOn && value == 6)
            {
                _buttonGrid[x, y].Text = value.ToString();
                _buttonGrid[x, y].Enabled = true;
            }
            else if (!IsPlayer1TurnOn && value == 3)
            {
                _buttonGrid[x, y].Text = value.ToString();
                _buttonGrid[x, y].Enabled = true;
            }
            else if (value == 4)
            {
                _buttonGrid[x, y].Text = value.ToString();
                _buttonGrid[x, y].Enabled = true;
            }
            else
            {
                _buttonGrid[x, y].Text = value.ToString();
                _buttonGrid[x, y].Enabled = false;
            }

            switch (value)
            {
                case -1:
                    _buttonGrid[x, y].BackColor = Color.Red;
                    break;

                case 1:
                    _buttonGrid[x, y].BackColor = Color.Blue;
                    break;

                case 3:
                    _buttonGrid[x, y].BackColor = Color.BlueViolet;
                    break;

                case 6:
                    _buttonGrid[x, y].BackColor = Color.OrangeRed;
                    break;

                case 4:
                    _buttonGrid[x, y].BackColor = Color.Green;
                    break;

                case 5:
                    _buttonGrid[x, y].BackColor = Color.White;
                    break;

                default:
                    break;
            }
        }

        #endregion

    }
}
