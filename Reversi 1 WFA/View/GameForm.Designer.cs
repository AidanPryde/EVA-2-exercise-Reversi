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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fileLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.fileExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSizeSmallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSizeMediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameSizeLargeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.helpAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.topFlowLayoutPanelForAll = new System.Windows.Forms.FlowLayoutPanel();
            this.player1GroupBox = new System.Windows.Forms.GroupBox();
            this.player1TimeValueLabel = new System.Windows.Forms.Label();
            this.player1TimeNameLabel = new System.Windows.Forms.Label();
            this.topFlowLayoutPanelForButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.passButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.player2GroupBox = new System.Windows.Forms.GroupBox();
            this.player2TimeValueLabel = new System.Windows.Forms.Label();
            this.player2TimeNameLabel = new System.Windows.Forms.Label();
            this.bottomButtonPanel = new System.Windows.Forms.Panel();
            this.loadFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.mainFlowLayoutPanel.SuspendLayout();
            this.topFlowLayoutPanelForAll.SuspendLayout();
            this.player1GroupBox.SuspendLayout();
            this.topFlowLayoutPanelForButtons.SuspendLayout();
            this.player2GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(505, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewToolStripMenuItem,
            this.toolStripSeparator2,
            this.fileLoadToolStripMenuItem,
            this.fileSaveToolStripMenuItem,
            this.toolStripSeparator1,
            this.fileExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // fileNewToolStripMenuItem
            // 
            this.fileNewToolStripMenuItem.Name = "fileNewToolStripMenuItem";
            this.fileNewToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.fileNewToolStripMenuItem.Text = "New";
            this.fileNewToolStripMenuItem.Click += new System.EventHandler(this.fileNewToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // fileLoadToolStripMenuItem
            // 
            this.fileLoadToolStripMenuItem.Name = "fileLoadToolStripMenuItem";
            this.fileLoadToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.fileLoadToolStripMenuItem.Text = "Load";
            this.fileLoadToolStripMenuItem.Click += new System.EventHandler(this.fileLoadToolStripMenuItem_Click);
            // 
            // fileSaveToolStripMenuItem
            // 
            this.fileSaveToolStripMenuItem.Enabled = false;
            this.fileSaveToolStripMenuItem.Name = "fileSaveToolStripMenuItem";
            this.fileSaveToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.fileSaveToolStripMenuItem.Text = "Save";
            this.fileSaveToolStripMenuItem.Click += new System.EventHandler(this.fileSaveToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // fileExitToolStripMenuItem
            // 
            this.fileExitToolStripMenuItem.Name = "fileExitToolStripMenuItem";
            this.fileExitToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.fileExitToolStripMenuItem.Text = "Exit";
            this.fileExitToolStripMenuItem.Click += new System.EventHandler(this.fileExitToolStripMenuItem_Click);
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
            this.gameSizeSmallToolStripMenuItem,
            this.gameSizeMediumToolStripMenuItem,
            this.gameSizeLargeToolStripMenuItem});
            this.gameSizeToolStripMenuItem.Name = "gameSizeToolStripMenuItem";
            this.gameSizeToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.gameSizeToolStripMenuItem.Text = "Size";
            // 
            // gameSizeSmallToolStripMenuItem
            // 
            this.gameSizeSmallToolStripMenuItem.Checked = true;
            this.gameSizeSmallToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.gameSizeSmallToolStripMenuItem.Enabled = false;
            this.gameSizeSmallToolStripMenuItem.Name = "gameSizeSmallToolStripMenuItem";
            this.gameSizeSmallToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.gameSizeSmallToolStripMenuItem.Text = "Small (10 × 10)";
            this.gameSizeSmallToolStripMenuItem.Click += new System.EventHandler(this.gameSizeSmallToolStripMenuItem_Click);
            // 
            // gameSizeMediumToolStripMenuItem
            // 
            this.gameSizeMediumToolStripMenuItem.Name = "gameSizeMediumToolStripMenuItem";
            this.gameSizeMediumToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.gameSizeMediumToolStripMenuItem.Text = "Medium (20 × 20)";
            this.gameSizeMediumToolStripMenuItem.Click += new System.EventHandler(this.gameSizeMediumToolStripMenuItem_Click);
            // 
            // gameSizeLargeToolStripMenuItem
            // 
            this.gameSizeLargeToolStripMenuItem.Name = "gameSizeLargeToolStripMenuItem";
            this.gameSizeLargeToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.gameSizeLargeToolStripMenuItem.Text = "Large  (30 × 30)";
            this.gameSizeLargeToolStripMenuItem.Click += new System.EventHandler(this.gameSizeLargeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpRulesToolStripMenuItem,
            this.toolStripSeparator4,
            this.helpAboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpRulesToolStripMenuItem
            // 
            this.helpRulesToolStripMenuItem.Name = "helpRulesToolStripMenuItem";
            this.helpRulesToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.helpRulesToolStripMenuItem.Text = "Rules";
            this.helpRulesToolStripMenuItem.Click += new System.EventHandler(this.helpRulesToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(178, 6);
            // 
            // helpAboutToolStripMenuItem
            // 
            this.helpAboutToolStripMenuItem.Name = "helpAboutToolStripMenuItem";
            this.helpAboutToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.helpAboutToolStripMenuItem.Text = "About Reversi Game";
            this.helpAboutToolStripMenuItem.Click += new System.EventHandler(this.helpAboutToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 136);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(505, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.toolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(669, 17);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "toolStripStatusLabel";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mainFlowLayoutPanel
            // 
            this.mainFlowLayoutPanel.AutoSize = true;
            this.mainFlowLayoutPanel.Controls.Add(this.topFlowLayoutPanelForAll);
            this.mainFlowLayoutPanel.Controls.Add(this.bottomButtonPanel);
            this.mainFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.mainFlowLayoutPanel.Location = new System.Drawing.Point(0, 24);
            this.mainFlowLayoutPanel.Name = "mainFlowLayoutPanel";
            this.mainFlowLayoutPanel.Size = new System.Drawing.Size(505, 112);
            this.mainFlowLayoutPanel.TabIndex = 2;
            // 
            // topFlowLayoutPanelForAll
            // 
            this.topFlowLayoutPanelForAll.AutoSize = true;
            this.topFlowLayoutPanelForAll.Controls.Add(this.player1GroupBox);
            this.topFlowLayoutPanelForAll.Controls.Add(this.topFlowLayoutPanelForButtons);
            this.topFlowLayoutPanelForAll.Controls.Add(this.player2GroupBox);
            this.topFlowLayoutPanelForAll.Location = new System.Drawing.Point(3, 3);
            this.topFlowLayoutPanelForAll.Name = "topFlowLayoutPanelForAll";
            this.topFlowLayoutPanelForAll.Size = new System.Drawing.Size(499, 106);
            this.topFlowLayoutPanelForAll.TabIndex = 0;
            // 
            // player1GroupBox
            // 
            this.player1GroupBox.Controls.Add(this.player1TimeValueLabel);
            this.player1GroupBox.Controls.Add(this.player1TimeNameLabel);
            this.player1GroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.player1GroupBox.Location = new System.Drawing.Point(3, 3);
            this.player1GroupBox.Name = "player1GroupBox";
            this.player1GroupBox.Size = new System.Drawing.Size(200, 100);
            this.player1GroupBox.TabIndex = 0;
            this.player1GroupBox.TabStop = false;
            this.player1GroupBox.Text = "Player 1";
            // 
            // player1TimeValueLabel
            // 
            this.player1TimeValueLabel.AutoSize = true;
            this.player1TimeValueLabel.Location = new System.Drawing.Point(70, 20);
            this.player1TimeValueLabel.Name = "player1TimeValueLabel";
            this.player1TimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.player1TimeValueLabel.TabIndex = 1;
            this.player1TimeValueLabel.Text = "0";
            // 
            // player1TimeNameLabel
            // 
            this.player1TimeNameLabel.AutoSize = true;
            this.player1TimeNameLabel.Location = new System.Drawing.Point(7, 20);
            this.player1TimeNameLabel.Name = "player1TimeNameLabel";
            this.player1TimeNameLabel.Size = new System.Drawing.Size(57, 13);
            this.player1TimeNameLabel.TabIndex = 0;
            this.player1TimeNameLabel.Text = "Used time:";
            // 
            // topFlowLayoutPanelForButtons
            // 
            this.topFlowLayoutPanelForButtons.Controls.Add(this.passButton);
            this.topFlowLayoutPanelForButtons.Controls.Add(this.pauseButton);
            this.topFlowLayoutPanelForButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.topFlowLayoutPanelForButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.topFlowLayoutPanelForButtons.Location = new System.Drawing.Point(209, 3);
            this.topFlowLayoutPanelForButtons.Name = "topFlowLayoutPanelForButtons";
            this.topFlowLayoutPanelForButtons.Size = new System.Drawing.Size(81, 100);
            this.topFlowLayoutPanelForButtons.TabIndex = 0;
            // 
            // passButton
            // 
            this.passButton.Enabled = false;
            this.passButton.Location = new System.Drawing.Point(3, 3);
            this.passButton.Name = "passButton";
            this.passButton.Size = new System.Drawing.Size(75, 23);
            this.passButton.TabIndex = 0;
            this.passButton.Text = "Pass";
            this.passButton.UseVisualStyleBackColor = true;
            this.passButton.Click += new System.EventHandler(this.passButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new System.Drawing.Point(3, 32);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 1;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // player2GroupBox
            // 
            this.player2GroupBox.Controls.Add(this.player2TimeValueLabel);
            this.player2GroupBox.Controls.Add(this.player2TimeNameLabel);
            this.player2GroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.player2GroupBox.Location = new System.Drawing.Point(296, 3);
            this.player2GroupBox.Name = "player2GroupBox";
            this.player2GroupBox.Size = new System.Drawing.Size(200, 100);
            this.player2GroupBox.TabIndex = 1;
            this.player2GroupBox.TabStop = false;
            this.player2GroupBox.Text = "Player 2";
            // 
            // player2TimeValueLabel
            // 
            this.player2TimeValueLabel.AutoSize = true;
            this.player2TimeValueLabel.Location = new System.Drawing.Point(70, 20);
            this.player2TimeValueLabel.Name = "player2TimeValueLabel";
            this.player2TimeValueLabel.Size = new System.Drawing.Size(13, 13);
            this.player2TimeValueLabel.TabIndex = 1;
            this.player2TimeValueLabel.Text = "0";
            // 
            // player2TimeNameLabel
            // 
            this.player2TimeNameLabel.AutoSize = true;
            this.player2TimeNameLabel.Location = new System.Drawing.Point(7, 20);
            this.player2TimeNameLabel.Name = "player2TimeNameLabel";
            this.player2TimeNameLabel.Size = new System.Drawing.Size(57, 13);
            this.player2TimeNameLabel.TabIndex = 0;
            this.player2TimeNameLabel.Text = "Used time:";
            // 
            // bottomButtonPanel
            // 
            this.bottomButtonPanel.Location = new System.Drawing.Point(511, 0);
            this.bottomButtonPanel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.bottomButtonPanel.Name = "bottomButtonPanel";
            this.bottomButtonPanel.Size = new System.Drawing.Size(0, 0);
            this.bottomButtonPanel.TabIndex = 1;
            // 
            // loadFileDialog
            // 
            this.loadFileDialog.Filter = "Reversi game files (*.reversi)|*.reversi|All files (*.*)|*.*";
            this.loadFileDialog.Title = "Loading reversi game.";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Title = "Saving reversi game.";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(505, 158);
            this.Controls.Add(this.mainFlowLayoutPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "GameForm";
            this.Text = "Reversi";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.mainFlowLayoutPanel.ResumeLayout(false);
            this.mainFlowLayoutPanel.PerformLayout();
            this.topFlowLayoutPanelForAll.ResumeLayout(false);
            this.player1GroupBox.ResumeLayout(false);
            this.player1GroupBox.PerformLayout();
            this.topFlowLayoutPanelForButtons.ResumeLayout(false);
            this.player2GroupBox.ResumeLayout(false);
            this.player2GroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem fileLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileSaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem fileExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSizeSmallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSizeMediumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameSizeLargeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel mainFlowLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel topFlowLayoutPanelForAll;
        private System.Windows.Forms.GroupBox player1GroupBox;
        private System.Windows.Forms.Label player1TimeValueLabel;
        private System.Windows.Forms.Label player1TimeNameLabel;
        private System.Windows.Forms.FlowLayoutPanel topFlowLayoutPanelForButtons;
        private System.Windows.Forms.Button passButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.GroupBox player2GroupBox;
        private System.Windows.Forms.Label player2TimeValueLabel;
        private System.Windows.Forms.Label player2TimeNameLabel;
        private System.Windows.Forms.Panel bottomButtonPanel;
        private System.Windows.Forms.OpenFileDialog loadFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

