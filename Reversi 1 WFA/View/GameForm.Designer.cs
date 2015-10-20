namespace Reversi.View
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fileNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._fileLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._fileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._fileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._gameSizeSmallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._gameSizeMediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._gameSizeLargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._helpRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this._helpAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this._toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._topFlowLayoutPanelForAll = new System.Windows.Forms.FlowLayoutPanel();
            this._player1GroupBox = new System.Windows.Forms.GroupBox();
            this._player1TimeValueLabel = new System.Windows.Forms.Label();
            this.player1TimeNameLabel = new System.Windows.Forms.Label();
            this._topFlowLayoutPanelForButtons = new System.Windows.Forms.FlowLayoutPanel();
            this._passButton = new System.Windows.Forms.Button();
            this._pauseButton = new System.Windows.Forms.Button();
            this._player2GroupBox = new System.Windows.Forms.GroupBox();
            this._player2TimeValueLabel = new System.Windows.Forms.Label();
            this.player2TimeNameLabel = new System.Windows.Forms.Label();
            this._bottomButtonPanel = new System.Windows.Forms.Panel();
            this._loadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this._saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this._menuStrip.SuspendLayout();
            this._statusStrip.SuspendLayout();
            this._mainFlowLayoutPanel.SuspendLayout();
            this._topFlowLayoutPanelForAll.SuspendLayout();
            this._player1GroupBox.SuspendLayout();
            this._topFlowLayoutPanelForButtons.SuspendLayout();
            this._player2GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuStrip
            // 
            this._menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this._menuStrip.Location = new System.Drawing.Point(0, 0);
            this._menuStrip.Name = "_menuStrip";
            this._menuStrip.Size = new System.Drawing.Size(524, 24);
            this._menuStrip.TabIndex = 0;
            this._menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._fileNewToolStripMenuItem,
            this.toolStripSeparator2,
            this._fileLoadToolStripMenuItem,
            this._fileSaveToolStripMenuItem,
            this.toolStripSeparator1,
            this._fileExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // _fileNewToolStripMenuItem
            // 
            this._fileNewToolStripMenuItem.Name = "_fileNewToolStripMenuItem";
            this._fileNewToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this._fileNewToolStripMenuItem.Text = "New";
            this._fileNewToolStripMenuItem.Click += new System.EventHandler(this.fileNewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // _fileLoadToolStripMenuItem
            // 
            this._fileLoadToolStripMenuItem.Name = "_fileLoadToolStripMenuItem";
            this._fileLoadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this._fileLoadToolStripMenuItem.Text = "Load";
            this._fileLoadToolStripMenuItem.Click += new System.EventHandler(this.fileLoadToolStripMenuItem_Click);
            // 
            // _fileSaveToolStripMenuItem
            // 
            this._fileSaveToolStripMenuItem.Enabled = false;
            this._fileSaveToolStripMenuItem.Name = "_fileSaveToolStripMenuItem";
            this._fileSaveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this._fileSaveToolStripMenuItem.Text = "Save";
            this._fileSaveToolStripMenuItem.Click += new System.EventHandler(this.fileSaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // _fileExitToolStripMenuItem
            // 
            this._fileExitToolStripMenuItem.Name = "_fileExitToolStripMenuItem";
            this._fileExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this._fileExitToolStripMenuItem.Text = "Exit";
            this._fileExitToolStripMenuItem.Click += new System.EventHandler(this.fileExitToolStripMenuItem_Click);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameSizeToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // gameSizeToolStripMenuItem
            // 
            this.gameSizeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._gameSizeSmallToolStripMenuItem,
            this._gameSizeMediumToolStripMenuItem,
            this._gameSizeLargeToolStripMenuItem});
            this.gameSizeToolStripMenuItem.Name = "gameSizeToolStripMenuItem";
            this.gameSizeToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.gameSizeToolStripMenuItem.Text = "Size";
            // 
            // _gameSizeSmallToolStripMenuItem
            // 
            this._gameSizeSmallToolStripMenuItem.Checked = true;
            this._gameSizeSmallToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this._gameSizeSmallToolStripMenuItem.Enabled = false;
            this._gameSizeSmallToolStripMenuItem.Name = "_gameSizeSmallToolStripMenuItem";
            this._gameSizeSmallToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this._gameSizeSmallToolStripMenuItem.Text = "Small (10 × 10)";
            this._gameSizeSmallToolStripMenuItem.Click += new System.EventHandler(this.gameSizeSmallToolStripMenuItem_Click);
            // 
            // _gameSizeMediumToolStripMenuItem
            // 
            this._gameSizeMediumToolStripMenuItem.Name = "_gameSizeMediumToolStripMenuItem";
            this._gameSizeMediumToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this._gameSizeMediumToolStripMenuItem.Text = "Medium (20 × 20)";
            this._gameSizeMediumToolStripMenuItem.Click += new System.EventHandler(this.gameSizeMediumToolStripMenuItem_Click);
            // 
            // _gameSizeLargeToolStripMenuItem
            // 
            this._gameSizeLargeToolStripMenuItem.Name = "_gameSizeLargeToolStripMenuItem";
            this._gameSizeLargeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this._gameSizeLargeToolStripMenuItem.Text = "Large  (30 × 30)";
            this._gameSizeLargeToolStripMenuItem.Click += new System.EventHandler(this.gameSizeLargeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._helpRulesToolStripMenuItem,
            this.toolStripSeparator4,
            this._helpAboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // _helpRulesToolStripMenuItem
            // 
            this._helpRulesToolStripMenuItem.Name = "_helpRulesToolStripMenuItem";
            this._helpRulesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this._helpRulesToolStripMenuItem.Text = "Rules";
            this._helpRulesToolStripMenuItem.Click += new System.EventHandler(this.helpRulesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(144, 6);
            // 
            // _helpAboutToolStripMenuItem
            // 
            this._helpAboutToolStripMenuItem.Name = "_helpAboutToolStripMenuItem";
            this._helpAboutToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this._helpAboutToolStripMenuItem.Text = "About Reversi";
            this._helpAboutToolStripMenuItem.Click += new System.EventHandler(this.helpAboutToolStripMenuItem_Click);
            // 
            // _statusStrip
            // 
            this._statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._toolStripStatusLabel});
            this._statusStrip.Location = new System.Drawing.Point(0, 132);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(524, 22);
            this._statusStrip.SizingGrip = false;
            this._statusStrip.TabIndex = 1;
            this._statusStrip.Text = "statusStrip1";
            // 
            // _toolStripStatusLabel
            // 
            this._toolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this._toolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._toolStripStatusLabel.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this._toolStripStatusLabel.Name = "_toolStripStatusLabel";
            this._toolStripStatusLabel.Size = new System.Drawing.Size(509, 18);
            this._toolStripStatusLabel.Spring = true;
            this._toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _mainFlowLayoutPanel
            // 
            this._mainFlowLayoutPanel.Controls.Add(this._topFlowLayoutPanelForAll);
            this._mainFlowLayoutPanel.Controls.Add(this._bottomButtonPanel);
            this._mainFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._mainFlowLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this._mainFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this._mainFlowLayoutPanel.Name = "_mainFlowLayoutPanel";
            this._mainFlowLayoutPanel.Size = new System.Drawing.Size(524, 108);
            this._mainFlowLayoutPanel.TabIndex = 2;
            // 
            // _topFlowLayoutPanelForAll
            // 
            this._topFlowLayoutPanelForAll.AutoSize = true;
            this._topFlowLayoutPanelForAll.Controls.Add(this._player1GroupBox);
            this._topFlowLayoutPanelForAll.Controls.Add(this._topFlowLayoutPanelForButtons);
            this._topFlowLayoutPanelForAll.Controls.Add(this._player2GroupBox);
            this._topFlowLayoutPanelForAll.Location = new System.Drawing.Point(2, 2);
            this._topFlowLayoutPanelForAll.Margin = new System.Windows.Forms.Padding(2);
            this._topFlowLayoutPanelForAll.Name = "_topFlowLayoutPanelForAll";
            this._topFlowLayoutPanelForAll.Size = new System.Drawing.Size(492, 104);
            this._topFlowLayoutPanelForAll.TabIndex = 0;
            // 
            // _player1GroupBox
            // 
            this._player1GroupBox.Controls.Add(this._player1TimeValueLabel);
            this._player1GroupBox.Controls.Add(this.player1TimeNameLabel);
            this._player1GroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._player1GroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._player1GroupBox.Location = new System.Drawing.Point(2, 2);
            this._player1GroupBox.Margin = new System.Windows.Forms.Padding(2);
            this._player1GroupBox.Name = "_player1GroupBox";
            this._player1GroupBox.Padding = new System.Windows.Forms.Padding(0);
            this._player1GroupBox.Size = new System.Drawing.Size(200, 100);
            this._player1GroupBox.TabIndex = 0;
            this._player1GroupBox.TabStop = false;
            this._player1GroupBox.Text = "Player 1";
            // 
            // _player1TimeValueLabel
            // 
            this._player1TimeValueLabel.AutoSize = true;
            this._player1TimeValueLabel.Location = new System.Drawing.Point(67, 17);
            this._player1TimeValueLabel.Name = "_player1TimeValueLabel";
            this._player1TimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this._player1TimeValueLabel.TabIndex = 1;
            this._player1TimeValueLabel.Text = "0";
            // 
            // player1TimeNameLabel
            // 
            this.player1TimeNameLabel.AutoSize = true;
            this.player1TimeNameLabel.Location = new System.Drawing.Point(4, 17);
            this.player1TimeNameLabel.Name = "player1TimeNameLabel";
            this.player1TimeNameLabel.Size = new System.Drawing.Size(57, 13);
            this.player1TimeNameLabel.TabIndex = 0;
            this.player1TimeNameLabel.Text = "Used time:";
            // 
            // _topFlowLayoutPanelForButtons
            // 
            this._topFlowLayoutPanelForButtons.AutoSize = true;
            this._topFlowLayoutPanelForButtons.Controls.Add(this._passButton);
            this._topFlowLayoutPanelForButtons.Controls.Add(this._pauseButton);
            this._topFlowLayoutPanelForButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this._topFlowLayoutPanelForButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this._topFlowLayoutPanelForButtons.Location = new System.Drawing.Point(206, 8);
            this._topFlowLayoutPanelForButtons.Margin = new System.Windows.Forms.Padding(2, 8, 2, 2);
            this._topFlowLayoutPanelForButtons.Name = "_topFlowLayoutPanelForButtons";
            this._topFlowLayoutPanelForButtons.Size = new System.Drawing.Size(80, 50);
            this._topFlowLayoutPanelForButtons.TabIndex = 0;
            // 
            // _passButton
            // 
            this._passButton.CausesValidation = false;
            this._passButton.Enabled = false;
            this._passButton.FlatAppearance.BorderSize = 0;
            this._passButton.Location = new System.Drawing.Point(0, 0);
            this._passButton.Margin = new System.Windows.Forms.Padding(0);
            this._passButton.Name = "_passButton";
            this._passButton.Size = new System.Drawing.Size(80, 25);
            this._passButton.TabIndex = 0;
            this._passButton.Text = "Pass";
            this._passButton.UseVisualStyleBackColor = true;
            this._passButton.Click += new System.EventHandler(this.passButton_Click);
            // 
            // _pauseButton
            // 
            this._pauseButton.CausesValidation = false;
            this._pauseButton.Enabled = false;
            this._pauseButton.FlatAppearance.BorderSize = 0;
            this._pauseButton.Location = new System.Drawing.Point(0, 25);
            this._pauseButton.Margin = new System.Windows.Forms.Padding(0);
            this._pauseButton.Name = "_pauseButton";
            this._pauseButton.Size = new System.Drawing.Size(80, 25);
            this._pauseButton.TabIndex = 1;
            this._pauseButton.Text = "Pause";
            this._pauseButton.UseVisualStyleBackColor = true;
            this._pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // _player2GroupBox
            // 
            this._player2GroupBox.Controls.Add(this._player2TimeValueLabel);
            this._player2GroupBox.Controls.Add(this.player2TimeNameLabel);
            this._player2GroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._player2GroupBox.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this._player2GroupBox.Location = new System.Drawing.Point(290, 2);
            this._player2GroupBox.Margin = new System.Windows.Forms.Padding(2);
            this._player2GroupBox.Name = "_player2GroupBox";
            this._player2GroupBox.Padding = new System.Windows.Forms.Padding(0);
            this._player2GroupBox.Size = new System.Drawing.Size(200, 100);
            this._player2GroupBox.TabIndex = 1;
            this._player2GroupBox.TabStop = false;
            this._player2GroupBox.Text = "Player 2";
            // 
            // _player2TimeValueLabel
            // 
            this._player2TimeValueLabel.AutoSize = true;
            this._player2TimeValueLabel.Location = new System.Drawing.Point(67, 17);
            this._player2TimeValueLabel.Name = "_player2TimeValueLabel";
            this._player2TimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this._player2TimeValueLabel.TabIndex = 1;
            this._player2TimeValueLabel.Text = "0";
            // 
            // player2TimeNameLabel
            // 
            this.player2TimeNameLabel.AutoSize = true;
            this.player2TimeNameLabel.Location = new System.Drawing.Point(4, 17);
            this.player2TimeNameLabel.Name = "player2TimeNameLabel";
            this.player2TimeNameLabel.Size = new System.Drawing.Size(57, 13);
            this.player2TimeNameLabel.TabIndex = 0;
            this.player2TimeNameLabel.Text = "Used time:";
            // 
            // _bottomButtonPanel
            // 
            this._bottomButtonPanel.Location = new System.Drawing.Point(500, 2);
            this._bottomButtonPanel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this._bottomButtonPanel.Name = "_bottomButtonPanel";
            this._bottomButtonPanel.Size = new System.Drawing.Size(0, 0);
            this._bottomButtonPanel.TabIndex = 1;
            // 
            // _loadFileDialog
            // 
            this._loadFileDialog.Filter = "Reversi game files (*.reversi)|*.reversi|All files (*.*)|*.*";
            this._loadFileDialog.Title = "Loading reversi game.";
            // 
            // _saveFileDialog
            // 
            this._saveFileDialog.Filter = "Reversi game files (*.reversi)|*.reversi";
            this._saveFileDialog.Title = "Saving reversi game.";
            // 
            // GameForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(524, 154);
            this.Controls.Add(this._mainFlowLayoutPanel);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this._menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this._menuStrip;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reversi";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this._menuStrip.ResumeLayout(false);
            this._menuStrip.PerformLayout();
            this._statusStrip.ResumeLayout(false);
            this._statusStrip.PerformLayout();
            this._mainFlowLayoutPanel.ResumeLayout(false);
            this._mainFlowLayoutPanel.PerformLayout();
            this._topFlowLayoutPanelForAll.ResumeLayout(false);
            this._topFlowLayoutPanelForAll.PerformLayout();
            this._player1GroupBox.ResumeLayout(false);
            this._player1GroupBox.PerformLayout();
            this._topFlowLayoutPanelForButtons.ResumeLayout(false);
            this._player2GroupBox.ResumeLayout(false);
            this._player2GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _menuStrip;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fileNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem _fileLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _fileSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem _fileExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _helpRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _helpAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _gameSizeSmallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _gameSizeMediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _gameSizeLargeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel _toolStripStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel _mainFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel _topFlowLayoutPanelForAll;
        private System.Windows.Forms.GroupBox _player1GroupBox;
        private System.Windows.Forms.Label _player1TimeValueLabel;
        private System.Windows.Forms.Label player1TimeNameLabel;
        private System.Windows.Forms.FlowLayoutPanel _topFlowLayoutPanelForButtons;
        private System.Windows.Forms.Button _passButton;
        private System.Windows.Forms.Button _pauseButton;
        private System.Windows.Forms.GroupBox _player2GroupBox;
        private System.Windows.Forms.Label _player2TimeValueLabel;
        private System.Windows.Forms.Label player2TimeNameLabel;
        private System.Windows.Forms.Panel _bottomButtonPanel;
        private System.Windows.Forms.OpenFileDialog _loadFileDialog;
        private System.Windows.Forms.SaveFileDialog _saveFileDialog;
    }
}

