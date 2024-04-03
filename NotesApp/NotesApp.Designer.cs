
namespace NotesApp
{
    partial class NotesApp
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotesApp));
            this.dataGridNotes = new System.Windows.Forms.DataGridView();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.noteTextBox = new System.Windows.Forms.TextBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.readButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.titleBar = new System.Windows.Forms.Panel();
            this.resizeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.colorBLabel = new System.Windows.Forms.Label();
            this.colorGLabel = new System.Windows.Forms.Label();
            this.colorBTrackBar = new System.Windows.Forms.TrackBar();
            this.colorGTrackBar = new System.Windows.Forms.TrackBar();
            this.colorRLabel = new System.Windows.Forms.Label();
            this.colorRTrackBar = new System.Windows.Forms.TrackBar();
            this.settingDeleteWithoutPromptCheckBox = new System.Windows.Forms.CheckBox();
            this.settingClearSaveWithoutPromptCheckBox = new System.Windows.Forms.CheckBox();
            this.resizeBar = new System.Windows.Forms.Panel();
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.folderComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNotes)).BeginInit();
            this.titleBar.SuspendLayout();
            this.settingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridNotes
            // 
            this.dataGridNotes.AllowUserToAddRows = false;
            this.dataGridNotes.AllowUserToDeleteRows = false;
            this.dataGridNotes.AllowUserToOrderColumns = true;
            this.dataGridNotes.AllowUserToResizeColumns = false;
            this.dataGridNotes.AllowUserToResizeRows = false;
            this.dataGridNotes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridNotes.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridNotes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridNotes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridNotes.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(171)))), ((int)(((byte)(156)))), ((int)(((byte)(145)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridNotes.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridNotes.Location = new System.Drawing.Point(556, 61);
            this.dataGridNotes.Name = "dataGridNotes";
            this.dataGridNotes.ReadOnly = true;
            this.dataGridNotes.RowHeadersVisible = false;
            this.dataGridNotes.RowTemplate.Height = 25;
            this.dataGridNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridNotes.Size = new System.Drawing.Size(457, 487);
            this.dataGridNotes.TabIndex = 0;
            this.dataGridNotes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.cell_DoubleClick);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.titleTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.titleTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.titleTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.titleTextBox.Location = new System.Drawing.Point(13, 32);
            this.titleTextBox.MaxLength = 48;
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(367, 22);
            this.titleTextBox.TabIndex = 1;
            // 
            // noteTextBox
            // 
            this.noteTextBox.AcceptsTab = true;
            this.noteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.noteTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.noteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.noteTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.noteTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.noteTextBox.Location = new System.Drawing.Point(12, 61);
            this.noteTextBox.Multiline = true;
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.noteTextBox.Size = new System.Drawing.Size(529, 458);
            this.noteTextBox.TabIndex = 2;
            this.noteTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.noteTextBox_KeyDown);
            // 
            // clearButton
            // 
            this.clearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(65)))), ((int)(((byte)(56)))));
            this.clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.clearButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.clearButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.clearButton.Location = new System.Drawing.Point(385, 32);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = false;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(65)))), ((int)(((byte)(56)))));
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.saveButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.saveButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.saveButton.Location = new System.Drawing.Point(466, 32);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // readButton
            // 
            this.readButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.readButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(65)))), ((int)(((byte)(56)))));
            this.readButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.readButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.readButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.readButton.Location = new System.Drawing.Point(385, 525);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(75, 23);
            this.readButton.TabIndex = 5;
            this.readButton.Text = "Read";
            this.readButton.UseVisualStyleBackColor = false;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(65)))), ((int)(((byte)(56)))));
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deleteButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.deleteButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.deleteButton.Location = new System.Drawing.Point(466, 525);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete";
            this.deleteButton.UseVisualStyleBackColor = false;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // titleBar
            // 
            this.titleBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(38)))), ((int)(((byte)(35)))));
            this.titleBar.Controls.Add(this.resizeButton);
            this.titleBar.Controls.Add(this.titleLabel);
            this.titleBar.Controls.Add(this.minimizeButton);
            this.titleBar.Controls.Add(this.exitButton);
            this.titleBar.Location = new System.Drawing.Point(-3, -5);
            this.titleBar.Name = "titleBar";
            this.titleBar.Size = new System.Drawing.Size(1344, 29);
            this.titleBar.TabIndex = 7;
            // 
            // resizeButton
            // 
            this.resizeButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.resizeButton.BackColor = System.Drawing.Color.Transparent;
            this.resizeButton.FlatAppearance.BorderSize = 0;
            this.resizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki;
            this.resizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Olive;
            this.resizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resizeButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resizeButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.resizeButton.Location = new System.Drawing.Point(1230, 1);
            this.resizeButton.Name = "resizeButton";
            this.resizeButton.Size = new System.Drawing.Size(57, 29);
            this.resizeButton.TabIndex = 3;
            this.resizeButton.TabStop = false;
            this.resizeButton.Text = "□";
            this.resizeButton.UseVisualStyleBackColor = false;
            this.resizeButton.Click += new System.EventHandler(this.resizeButton_Click);
            this.resizeButton.MouseEnter += new System.EventHandler(this.resizeButton_MouseEnter);
            this.resizeButton.MouseLeave += new System.EventHandler(this.resizeButton_MouseLeave);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.titleLabel.Location = new System.Drawing.Point(9, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(42, 14);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Notes";
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.minimizeButton.BackColor = System.Drawing.Color.Transparent;
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki;
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Olive;
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.minimizeButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.minimizeButton.Location = new System.Drawing.Point(1173, 1);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(57, 29);
            this.minimizeButton.TabIndex = 1;
            this.minimizeButton.TabStop = false;
            this.minimizeButton.Text = "-";
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.exitButton.BackColor = System.Drawing.Color.Transparent;
            this.exitButton.FlatAppearance.BorderSize = 0;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkKhaki;
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(30)))), ((int)(((byte)(17)))));
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exitButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.exitButton.Location = new System.Drawing.Point(1287, 1);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(57, 29);
            this.exitButton.TabIndex = 0;
            this.exitButton.TabStop = false;
            this.exitButton.Text = "X";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.statusLabel.ForeColor = System.Drawing.Color.Gainsboro;
            this.statusLabel.Location = new System.Drawing.Point(13, 529);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(56, 14);
            this.statusLabel.TabIndex = 8;
            this.statusLabel.Text = "BITCHES";
            // 
            // settingsGroup
            // 
            this.settingsGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.settingsGroup.Controls.Add(this.colorComboBox);
            this.settingsGroup.Controls.Add(this.colorBLabel);
            this.settingsGroup.Controls.Add(this.colorGLabel);
            this.settingsGroup.Controls.Add(this.colorBTrackBar);
            this.settingsGroup.Controls.Add(this.colorGTrackBar);
            this.settingsGroup.Controls.Add(this.colorRLabel);
            this.settingsGroup.Controls.Add(this.colorRTrackBar);
            this.settingsGroup.Controls.Add(this.settingDeleteWithoutPromptCheckBox);
            this.settingsGroup.Controls.Add(this.settingClearSaveWithoutPromptCheckBox);
            this.settingsGroup.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.settingsGroup.ForeColor = System.Drawing.Color.Gainsboro;
            this.settingsGroup.Location = new System.Drawing.Point(1019, 32);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(313, 516);
            this.settingsGroup.TabIndex = 9;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "Settings";
            // 
            // colorComboBox
            // 
            this.colorComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.colorComboBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.colorComboBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.colorComboBox.FormattingEnabled = true;
            this.colorComboBox.Items.AddRange(new object[] {
            "Background",
            "TextBox",
            "Button",
            "TitleBar",
            "Highlight",
            "Text",
            ""});
            this.colorComboBox.Location = new System.Drawing.Point(15, 69);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(138, 22);
            this.colorComboBox.TabIndex = 13;
            this.colorComboBox.SelectedIndexChanged += new System.EventHandler(this.colorComboBox_SelectedIndexChanged);
            // 
            // colorBLabel
            // 
            this.colorBLabel.AutoSize = true;
            this.colorBLabel.Location = new System.Drawing.Point(252, 145);
            this.colorBLabel.Name = "colorBLabel";
            this.colorBLabel.Size = new System.Drawing.Size(28, 14);
            this.colorBLabel.TabIndex = 7;
            this.colorBLabel.Text = "255";
            // 
            // colorGLabel
            // 
            this.colorGLabel.AutoSize = true;
            this.colorGLabel.Location = new System.Drawing.Point(252, 123);
            this.colorGLabel.Name = "colorGLabel";
            this.colorGLabel.Size = new System.Drawing.Size(28, 14);
            this.colorGLabel.TabIndex = 6;
            this.colorGLabel.Text = "255";
            // 
            // colorBTrackBar
            // 
            this.colorBTrackBar.Location = new System.Drawing.Point(6, 141);
            this.colorBTrackBar.Maximum = 255;
            this.colorBTrackBar.Name = "colorBTrackBar";
            this.colorBTrackBar.Size = new System.Drawing.Size(250, 45);
            this.colorBTrackBar.TabIndex = 5;
            this.colorBTrackBar.TabStop = false;
            this.colorBTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.colorBTrackBar.Scroll += new System.EventHandler(this.colorTrackBar_Scroll);
            // 
            // colorGTrackBar
            // 
            this.colorGTrackBar.Location = new System.Drawing.Point(6, 119);
            this.colorGTrackBar.Maximum = 255;
            this.colorGTrackBar.Name = "colorGTrackBar";
            this.colorGTrackBar.Size = new System.Drawing.Size(250, 45);
            this.colorGTrackBar.TabIndex = 4;
            this.colorGTrackBar.TabStop = false;
            this.colorGTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.colorGTrackBar.Scroll += new System.EventHandler(this.colorTrackBar_Scroll);
            // 
            // colorRLabel
            // 
            this.colorRLabel.AutoSize = true;
            this.colorRLabel.Location = new System.Drawing.Point(252, 101);
            this.colorRLabel.Name = "colorRLabel";
            this.colorRLabel.Size = new System.Drawing.Size(28, 14);
            this.colorRLabel.TabIndex = 3;
            this.colorRLabel.Text = "255";
            // 
            // colorRTrackBar
            // 
            this.colorRTrackBar.Location = new System.Drawing.Point(6, 97);
            this.colorRTrackBar.Maximum = 255;
            this.colorRTrackBar.Name = "colorRTrackBar";
            this.colorRTrackBar.Size = new System.Drawing.Size(250, 45);
            this.colorRTrackBar.TabIndex = 2;
            this.colorRTrackBar.TabStop = false;
            this.colorRTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.colorRTrackBar.Scroll += new System.EventHandler(this.colorTrackBar_Scroll);
            // 
            // settingDeleteWithoutPromptCheckBox
            // 
            this.settingDeleteWithoutPromptCheckBox.AutoSize = true;
            this.settingDeleteWithoutPromptCheckBox.Location = new System.Drawing.Point(15, 45);
            this.settingDeleteWithoutPromptCheckBox.Name = "settingDeleteWithoutPromptCheckBox";
            this.settingDeleteWithoutPromptCheckBox.Size = new System.Drawing.Size(215, 18);
            this.settingDeleteWithoutPromptCheckBox.TabIndex = 1;
            this.settingDeleteWithoutPromptCheckBox.TabStop = false;
            this.settingDeleteWithoutPromptCheckBox.Text = "Delete notes without prompt";
            this.settingDeleteWithoutPromptCheckBox.UseVisualStyleBackColor = true;
            this.settingDeleteWithoutPromptCheckBox.CheckedChanged += new System.EventHandler(this.settingDeleteWithoutPromptCheckBox_CheckedChanged);
            // 
            // settingClearSaveWithoutPromptCheckBox
            // 
            this.settingClearSaveWithoutPromptCheckBox.AutoSize = true;
            this.settingClearSaveWithoutPromptCheckBox.Location = new System.Drawing.Point(15, 21);
            this.settingClearSaveWithoutPromptCheckBox.Name = "settingClearSaveWithoutPromptCheckBox";
            this.settingClearSaveWithoutPromptCheckBox.Size = new System.Drawing.Size(236, 18);
            this.settingClearSaveWithoutPromptCheckBox.TabIndex = 0;
            this.settingClearSaveWithoutPromptCheckBox.TabStop = false;
            this.settingClearSaveWithoutPromptCheckBox.Text = "Clear/Save note without prompt";
            this.settingClearSaveWithoutPromptCheckBox.UseVisualStyleBackColor = true;
            this.settingClearSaveWithoutPromptCheckBox.CheckedChanged += new System.EventHandler(this.settingClearSaveWithoutPromptCheckBox_CheckedChanged);
            // 
            // resizeBar
            // 
            this.resizeBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resizeBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(38)))), ((int)(((byte)(35)))));
            this.resizeBar.Location = new System.Drawing.Point(-3, 558);
            this.resizeBar.Name = "resizeBar";
            this.resizeBar.Size = new System.Drawing.Size(1344, 14);
            this.resizeBar.TabIndex = 10;
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.searchTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.searchTextBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.searchTextBox.Location = new System.Drawing.Point(556, 34);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(297, 22);
            this.searchTextBox.TabIndex = 11;
            this.searchTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyUp);
            // 
            // folderComboBox
            // 
            this.folderComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.folderComboBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.folderComboBox.ForeColor = System.Drawing.Color.Gainsboro;
            this.folderComboBox.FormattingEnabled = true;
            this.folderComboBox.Items.AddRange(new object[] {
            "Default"});
            this.folderComboBox.Location = new System.Drawing.Point(859, 33);
            this.folderComboBox.Name = "folderComboBox";
            this.folderComboBox.Size = new System.Drawing.Size(154, 22);
            this.folderComboBox.TabIndex = 12;
            this.folderComboBox.Text = "All";
            this.folderComboBox.SelectedIndexChanged += new System.EventHandler(this.folderComboBox_SelectedIndexChanged);
            this.folderComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.folderComboBox_KeyDown);
            this.folderComboBox.Leave += new System.EventHandler(this.folderComboBox_Leave);
            // 
            // NotesApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(1341, 570);
            this.Controls.Add(this.folderComboBox);
            this.Controls.Add(this.searchTextBox);
            this.Controls.Add(this.resizeBar);
            this.Controls.Add(this.settingsGroup);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.titleBar);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.clearButton);
            this.Controls.Add(this.noteTextBox);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.dataGridNotes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 35);
            this.Name = "NotesApp";
            this.Text = "Notes";
            this.Load += new System.EventHandler(this.LoadApp);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNotes)).EndInit();
            this.titleBar.ResumeLayout(false);
            this.titleBar.PerformLayout();
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorBTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorGTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colorRTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridNotes;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.TextBox noteTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Panel titleBar;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button resizeButton;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.Panel resizeBar;
        private System.Windows.Forms.TextBox searchTextBox;
        private System.Windows.Forms.ComboBox folderComboBox;
        private System.Windows.Forms.CheckBox settingDeleteWithoutPromptCheckBox;
        private System.Windows.Forms.CheckBox settingClearSaveWithoutPromptCheckBox;
        private System.Windows.Forms.TrackBar colorRTrackBar;
        private System.Windows.Forms.Label colorRLabel;
        private System.Windows.Forms.Label colorBLabel;
        private System.Windows.Forms.Label colorGLabel;
        private System.Windows.Forms.TrackBar colorBTrackBar;
        private System.Windows.Forms.TrackBar colorGTrackBar;
        private System.Windows.Forms.ComboBox colorComboBox;
    }
}

