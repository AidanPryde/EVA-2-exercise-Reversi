using System;
using System.Windows.Forms;

namespace Reversi.View
{
    public partial class AboutMessageForm : Form
    {
        public AboutMessageForm()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutMessageForm_Load(object sender, EventArgs e)
        {
            // Define the border style of the form to a dialog box.
            FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set the MaximizeBox to false to remove the maximize box.
            MaximizeBox = false;

            // Set the MinimizeBox to false to remove the minimize box.
            MinimizeBox = false;

            // Set the start position of the form to the center of the screen.
            StartPosition = FormStartPosition.CenterScreen;

            // TODO: Set up a relative link?
            pictureBox.ImageLocation = "C:\\Users\\Márton\\Documents\\Visual Studio 2015\\Projects\\EVA 2 exercise Reversi\\Reversi 1 WFA\\Resource\\p.png";
        }
    }
}
