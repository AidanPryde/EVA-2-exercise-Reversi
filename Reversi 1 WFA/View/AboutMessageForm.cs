
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Reversi_WFA.View
{
    /// <summary>
    /// The about message window type.
    /// </summary>
    public partial class AboutMessageForm : Form
    {

        #region Constructor

        /// <summary>
        /// Generated stuff. Do not change.
        /// </summary>
        public AboutMessageForm()
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

            // Set up the little image icon. 
            Bitmap image = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reversi.Resources.p.png"));
            pictureBox.Image = image;
        }

        /// <summary>
        /// The ok button click event handler.
        /// </summary>
        /// <param name="sender">This _okButton, we do not use it as a param.</param>
        /// <param name="e">Auto param, we do not use it.</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

    }
}
