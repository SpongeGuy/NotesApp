
namespace NotesApp
{
    partial class DialogPrompt
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
            this.buttonExit = new System.Windows.Forms.Button();
            this.titleBar = new System.Windows.Forms.Panel();
            this.promptText = new System.Windows.Forms.TextBox();
            this.yesButton = new System.Windows.Forms.Button();
            this.noButton = new System.Windows.Forms.Button();
            this.dontAskCheckBox = new System.Windows.Forms.CheckBox();
            this.titleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackColor = System.Drawing.Color.Transparent;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki;
            this.buttonExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(30)))), ((int)(((byte)(17)))));
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonExit.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonExit.Location = new System.Drawing.Point(225, 7);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(23, 22);
            this.buttonExit.TabIndex = 1;
            this.buttonExit.TabStop = false;
            this.buttonExit.Text = "X";
            this.buttonExit.UseVisualStyleBackColor = false;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // titleBar
            // 
            this.titleBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(38)))), ((int)(((byte)(35)))));
            this.titleBar.Controls.Add(this.buttonExit);
            this.titleBar.Location = new System.Drawing.Point(-1, -6);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(248, 29);
            this.titleBar.TabIndex = 2;
            // 
            // promptText
            // 
            this.promptText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(28)))));
            this.promptText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.promptText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.promptText.ForeColor = System.Drawing.Color.Gainsboro;
            this.promptText.Location = new System.Drawing.Point(12, 29);
            this.promptText.Multiline = true;
            this.promptText.Name = "promptText";
            this.promptText.Size = new System.Drawing.Size(220, 90);
            this.promptText.TabIndex = 3;
            this.promptText.TabStop = false;
            this.promptText.Text = "I am an insane guy.,\r\nAre you sure you want to continue?\r\nfr?";
            this.promptText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yesButton
            // 
            this.yesButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(65)))), ((int)(((byte)(56)))));
            this.yesButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.yesButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.yesButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.yesButton.Location = new System.Drawing.Point(12, 155);
            this.yesButton.Name = "yesButton";
            this.yesButton.Size = new System.Drawing.Size(75, 23);
            this.yesButton.TabIndex = 4;
            this.yesButton.Text = "Yes";
            this.yesButton.UseVisualStyleBackColor = false;
            this.yesButton.Click += new System.EventHandler(this.yesButton_Click);
            // 
            // noButton
            // 
            this.noButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(65)))), ((int)(((byte)(56)))));
            this.noButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.noButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.noButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.noButton.Location = new System.Drawing.Point(157, 155);
            this.noButton.Name = "noButton";
            this.noButton.Size = new System.Drawing.Size(75, 23);
            this.noButton.TabIndex = 5;
            this.noButton.Text = "No";
            this.noButton.UseVisualStyleBackColor = false;
            this.noButton.Click += new System.EventHandler(this.noButton_Click);
            // 
            // dontAskCheckBox
            // 
            this.dontAskCheckBox.AutoSize = true;
            this.dontAskCheckBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dontAskCheckBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.dontAskCheckBox.Location = new System.Drawing.Point(50, 125);
            this.dontAskCheckBox.Name = "dontAskCheckBox";
            this.dontAskCheckBox.Size = new System.Drawing.Size(131, 18);
            this.dontAskCheckBox.TabIndex = 6;
            this.dontAskCheckBox.Text = "Don\'t ask again";
            this.dontAskCheckBox.UseVisualStyleBackColor = true;
            // 
            // DialogPrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(244, 190);
            this.Controls.Add(this.dontAskCheckBox);
            this.Controls.Add(this.noButton);
            this.Controls.Add(this.yesButton);
            this.Controls.Add(this.promptText);
            this.Controls.Add(this.titleBar);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DialogPrompt";
            this.Text = "Form1";
            this.titleBar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.TextBox promptText;
        private System.Windows.Forms.Button yesButton;
        private System.Windows.Forms.Button noButton;
        private System.Windows.Forms.CheckBox dontAskCheckBox;
    }
}