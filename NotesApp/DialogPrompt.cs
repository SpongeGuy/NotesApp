using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NotesApp
{

    
    public partial class DialogPrompt : Form
    {
        public bool isDontAskAgainChecked;

        private const int MW_NCLBUTTONDOWN = 0x00A1;
        private const int HT_CAPTION = 0x02;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public DialogPrompt()
        {
            InitializeComponent();

            dontAskCheckBox.Checked = false;
            isDontAskAgainChecked = dontAskCheckBox.Checked;
            

            this.StartPosition = FormStartPosition.CenterParent;

            titleBar.MouseDown += (sender, e) =>
            {
                ReleaseCapture();
                SendMessage(Handle, MW_NCLBUTTONDOWN, HT_CAPTION, 0);
            };
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            if (dontAskCheckBox.Checked) isDontAskAgainChecked = true;
            this.Close();
        }

        private void noButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            if (dontAskCheckBox.Checked) isDontAskAgainChecked = true;
            this.Close();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            if (dontAskCheckBox.Checked) isDontAskAgainChecked = true;
            this.Close();
        }

        public void ChangePromptMessage(params string[] messages)
        {
            string prompt = "";
            promptText.Clear();
            foreach (string message in messages)
            {
                prompt += message + Environment.NewLine;
            }
            promptText.Text = prompt;
        }
    }
}
