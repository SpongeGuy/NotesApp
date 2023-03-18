using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NotesApp
{
    public partial class NotesApp : Form
    {
        enum SaveStatus
        {
            Normal,
            Overwrite
        }
        public enum WindowSizeStatuses
        {
            Normal,
            WaitingSettingsMenu,
            SettingsMenu,
            Half
        }
        
        public DataTable table;
        public WindowSizeStatuses windowSizeStatus;

        public string rootFolderName = "All";

        // resize button holding control behaviors
        private System.Windows.Forms.Timer hoveringResizeButtonTimer;
        private bool ctrlHeldResizeButton = false;

        // this is to give panel mousedrag function
        private const int MW_NCLBUTTONDOWN = 0x00A1;
        private const int HT_CAPTION = 0x02;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private const int WM_SYSCOMMAND = 0x0112;
        private const int SC_SIZE = 0xF000;
        private const int WMSZ_BOTTOM = 6;
        // end bullshit

        // BEGIN SETTINGS
        public bool firstLaunch = true;

        // window height
        public int defaultWindowHeight = 550;
        public int windowHeight;
        public Size windowSizeHalf;
        public Size windowSizeNormal;
        public Size windowSizeSettings;

        // window prompt
        public bool settingDeleteWithoutPrompt;
        public bool settingClearSaveWithoutPrompt;

        // colors
        enum ColorEditStatus
        {
            Background,
            TitleBar,
            Button,
            Text,
            Highlight,
            TextBox
        }

        public Color backgroundColor;
        public Color titleBarColor;
        public Color buttonColor;
        public Color textColor;
        public Color highlightColor;
        public Color textBoxColor;
        ColorEditStatus colorEditStatus;
        
        

        List<Control> controlsWithText = new List<Control>();
        List<Control> buttons = new List<Control>();
        List<Control> textBoxes = new List<Control>();
        List<Control> titleControls = new List<Control>();

        // END SETTINGS


        // BEGIN INIT AND MISC
        public NotesApp()
        {
            colorEditStatus = ColorEditStatus.Background;

            // BEGIN CONTROL PROPERTIES
            InitializeComponent();

            populateControlLists();

            // this controls the resize button's behaviors when holding control (accessing options menu)
            hoveringResizeButtonTimer = new System.Windows.Forms.Timer();
            hoveringResizeButtonTimer.Interval = 100;
            hoveringResizeButtonTimer.Tick += new EventHandler(hoveringResizeButtonTimer_Tick);

            statusLabel.Visible = false;
            settingsGroup.Visible = false;

            
            // set titlebar objects to be moveable by the mouse
            titleBar.MouseDown += (sender, e) =>
            {
                ReleaseCapture();
                SendMessage(Handle, MW_NCLBUTTONDOWN, HT_CAPTION, 0);
            };
            titleLabel.MouseDown += (sender, e) =>
            {
                ReleaseCapture();
                SendMessage(Handle, MW_NCLBUTTONDOWN, HT_CAPTION, 0);
            };
            resizeBar.MouseDown += (sender, e) =>
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SYSCOMMAND, SC_SIZE + WMSZ_BOTTOM, 0);
                windowHeight = this.Height;
            };
            // END CONTROL PROPERTIES
        }

        private void populateControlLists()
        {

            titleControls.Add(titleBar);
            titleControls.Add(minimizeButton);
            titleControls.Add(resizeButton);
            titleControls.Add(exitButton);
            titleControls.Add(resizeBar);

            foreach (Control control in this.Controls)
            {
                Type controlType = control.GetType();
                bool hasTextProperty = controlType.GetProperty("Text") != null;
                if (hasTextProperty)
                {
                    controlsWithText.Add(control);
                }
                if (control is Button)
                {
                    buttons.Add(control);
                }
                if (control is TextBox or ComboBox)
                {
                    textBoxes.Add(control);
                }
            }
            // im not sure why I have to do this, but I guess i do otherwise it wont be in the lists
            controlsWithText.Add(colorComboBox);
            textBoxes.Add(colorComboBox);
            controlsWithText.Add(titleLabel);
            controlsWithText.Add(minimizeButton);
            controlsWithText.Add(resizeButton);
            controlsWithText.Add(exitButton);
        }

        private void LoadApp(object sender, EventArgs e)
        {
            FormData data = Serializer.LoadData();
            if (data != null)
            {
                firstLaunch = false;
            }
            //debug
            //firstLaunch = true;
            if (firstLaunch)
            {
                MessageHandler.DebugWrite("First launch. Populating settings with default values.");
                // SET DEFAULT VALUES FOR FIRST LAUNCH HERE
                // window height
                windowSizeStatus = WindowSizeStatuses.Normal;
                windowHeight = 550;
                updateWindowHeight(windowHeight);
                this.Size = windowSizeNormal;

                // warning prompt
                setClearSaveWithoutPrompt(false);
                setDeleteWithoutPrompt(false);
                updateColorLabels();

                // colors
                updateColorLabels();
                colorEditStatus = ColorEditStatus.Background;
                colorComboBox.Text = colorEditStatus.ToString();

                backgroundColor = Color.FromArgb(32, 31, 28);
                highlightColor = Color.FromArgb(171, 156, 145);
                titleBarColor = Color.FromArgb(46, 38, 35);
                textBoxColor = Color.FromArgb(64, 64, 64);
                buttonColor = Color.FromArgb(86, 65, 56);
                textColor = Color.Gainsboro;
                
                

                updateColorTrackBarValues();


            }
            else
            {
                MessageHandler.DebugWrite("Launching. Populating settings with values from data.spig.");
                // LOAD SETTINGS FROM DATA HERE

                // window height
                windowSizeStatus = data.windowSizeStatus;
                windowHeight = data.windowHeight;
                updateWindowHeight(windowHeight);
                switch (windowSizeStatus) // i might make a method to do this, but it might not be necessary (we'll see)
                {
                    case WindowSizeStatuses.Normal:
                        this.Size = windowSizeNormal;
                        resizeButton.Text = "□";
                        break;
                    case WindowSizeStatuses.Half:
                        this.Size = windowSizeHalf;
                        resizeButton.Text = "□□";
                        break;
                    case WindowSizeStatuses.SettingsMenu:
                        this.Size = windowSizeSettings;
                        settingsGroup.Visible = true;
                        resizeButton.Text = "□□";
                        break;
                }

                // warning prompt
                setClearSaveWithoutPrompt(data.settingClearSaveWithoutPrompt);
                setDeleteWithoutPrompt(data.settingDeleteWithoutPrompt);

                // colors
                
                backgroundColor = data.backgroundColor;
                highlightColor = data.highlightColor;
                titleBarColor = data.titleBarColor;
                textBoxColor = data.textBoxColor;
                buttonColor = data.buttonColor;
                textColor = data.textColor;
                // loop through color edit status because that's how I wrote updateColors() like adummy
                this.SuspendLayout();
                for (int i = 0; i < Enum.GetValues(typeof(ColorEditStatus)).Length; i++)
                {
                    colorEditStatus = (ColorEditStatus)Enum.Parse(typeof(ColorEditStatus), i.ToString());
                    MessageHandler.DebugWrite(colorEditStatus.ToString());
                    updateColorTrackBarValues();
                    updateColors();
                }
                colorEditStatus = ColorEditStatus.Background;
                colorComboBox.Text = colorEditStatus.ToString();
                updateColorLabels();
                this.ResumeLayout();





            }

            // table nonsense
            try
            {
                table = new DataTable();
                table.Columns.Add("Title", typeof(string));
                table.Columns.Add("Note", typeof(string));
                table.Columns.Add("DateCreated", typeof(DateTime));
                table.Columns.Add("DateModified", typeof(DateTime));
                table.Columns.Add("Folder", typeof(string));
                // add date type

                
                if (data == null)
                {
                    MessageHandler.DebugWrite("No data found. Creating new table.");
                    dataGridNotes.DataSource = table;
                }
                else
                {
                    // load/overwrite ALL DATA here from serializer
                    table = data.table;
                    
                    for (int index = 0; index < data.packet.notes.Count; index++)
                    {
                        DataRow row = table.NewRow();
                        row["Title"] = data.packet.titles[index];
                        row["Note"] = data.packet.notes[index];
                        row["DateCreated"] = data.packet.originalDates[index];
                        row["DateModified"] = data.packet.modifiedDates[index];
                        row["Folder"] = data.packet.folders[index];
                    }
                    dataGridNotes.DataSource = table;
                }

                int dateWidth = dataGridNotes.Width / 3;
                int titleWidth = dataGridNotes.Width / 2 + (dataGridNotes.Width / 2 - dateWidth);

                dataGridNotes.Columns["Title"].Width = titleWidth;
                dataGridNotes.Columns["Note"].Visible = false;
                dataGridNotes.Columns["DateCreated"].Visible = false;
                dataGridNotes.Columns["DateModified"].Visible = true;
                dataGridNotes.Columns["DateModified"].Width = dateWidth;
                dataGridNotes.Columns["Folder"].Visible = false;


                dataGridNotes.ClearSelection();
                RefreshFolders();
                
            }
            catch (Exception exception)
            {
                MessageHandler.DebugWrite("Couldn't load: " + exception.Message);
            }
        }

        public RawRowsPacket GetRawRows()
        {
            
            List<string> titles = new List<string>();
            List<string> notes = new List<string>();
            List<DateTime> originalDates = new List<DateTime>();
            List<DateTime> modifiedDates = new List<DateTime>();
            List<string> folders = new List<string>();
            foreach (DataGridViewRow row in dataGridNotes.Rows)
            {
                // add the cell values to each list
                titles.Add(row.Cells[0].Value.ToString());
                notes.Add(row.Cells[1].Value.ToString());
                originalDates.Add(DateTime.Parse(row.Cells[2].Value.ToString()));
                modifiedDates.Add(DateTime.Parse(row.Cells[3].Value.ToString()));
                folders.Add(row.Cells[4].Value.ToString());
            }
            RawRowsPacket packet = new RawRowsPacket();
            packet.titles = titles;
            packet.notes = notes;
            packet.originalDates = originalDates;
            packet.modifiedDates = originalDates;
            packet.folders = folders;
            return packet;
        }

        private DialogPrompt CreateDialogPrompt(params string[] messages)
        {
            DialogPrompt prompt = new DialogPrompt();
            prompt.ChangePromptMessage(messages);
            return prompt;
        }

        private void unfocusCurrentCell()
        {
            if (dataGridNotes.CurrentCell != null)
            {
                dataGridNotes.ClearSelection();
                dataGridNotes.CurrentCell = null;
            }
        }

        private void saveNote()
        {
            // add support for ctrl+s
            SaveStatus status = SaveStatus.Normal;
            try
            {
                if (noteTextBox.Text == "") throw new Exception("no text in note field.");
                if (titleTextBox.Text == "") titleTextBox.Text = "untitled";

                // check if title is duplicate of another title
                // if titles are duplicate, then overwrite note contents
                List<string> titles = new List<string>();
                for (int i = 0; i < dataGridNotes.Rows.Count; i++)
                {
                    titles.Add(dataGridNotes.Rows[i].Cells[0].Value.ToString());
                }
                int index = 0;
                foreach (string title in titles)
                {
                    if (titleTextBox.Text == title)
                    {
                        DialogPrompt prompt = CreateDialogPrompt($"A note called '{title}'", "already exists.", "Do you want to overwrite it?");
                        DialogResult result;
                        if (!settingClearSaveWithoutPrompt)
                        {
                            result = prompt.ShowDialog();
                        }
                        else result = DialogResult.Yes;
                        if (prompt.isDontAskAgainChecked) setClearSaveWithoutPrompt(true);

                        if (result == DialogResult.No) throw new Exception("cancelled operation.");
                        dataGridNotes.Rows[index].Cells[1].Value = noteTextBox.Text;
                        dataGridNotes.Rows[index].Cells[3].Value = DateTime.Now;

                        // FUTURE: ADD FUNCTIONALITY FOR SAME NAME FILES IN DIFFERENT FOLDERS
                        string folderName = folderComboBox.Text;
                        dataGridNotes.Rows[index].Cells[4].Value = folderComboBox.Text;
                        MessageHandler.StatusMessage(statusLabel, $"Overwrote note '{title}'.");
                        RefreshDataGridNotesFolderList();
                        RefreshFolders();

                        status = SaveStatus.Overwrite;
                    }
                    index++;
                }

                if (status == SaveStatus.Normal)
                {
                    MessageHandler.StatusMessage(statusLabel, $"Saved note as '{titleTextBox.Text}'.");
                    table.Rows.Add(titleTextBox.Text, noteTextBox.Text, DateTime.Now, DateTime.Now, folderComboBox.Text);
                    RefreshDataGridNotesFolderList();
                    RefreshFolders();
                }
                unfocusCurrentCell();
            }
            catch (Exception exception)
            {
                MessageHandler.StatusMessage(statusLabel, "Couldn't save: " + exception.Message);
            }
            finally
            {
                Serializer.SaveData(this);
            }
        }

        private void safelyCloseApp()
        {
            Serializer.SaveData(this);
            this.Close();
        }

        private void updateWindowHeight(int height)
        {
            try
            {
                windowSizeHalf = new Size(554, height);
                windowSizeNormal = new Size(1027, height);
                windowSizeSettings = new Size(1344, height);
            }
            catch (Exception exception)
            {
                MessageHandler.DebugWrite("Could not update height!");
            }

        }

        // END INIT AND MISC



        // BEGIN BUTTON FUNCTIONS
        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            unfocusCurrentCell();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                // check if datagridview's content is there
                if (dataGridNotes.Rows.Count <= 0 || dataGridNotes.CurrentCell == null) throw new Exception("invalid cell selection!");
                

                int index = dataGridNotes.CurrentCell.RowIndex;
                string title = dataGridNotes.Rows[index].Cells[0].Value.ToString();
                DialogPrompt prompt = CreateDialogPrompt("Are you sure you want to", "delete this note?", $"Everything in '{title}'", "will be lost.");
                DialogResult result = prompt.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    table.Rows[index].Delete();
                    MessageHandler.StatusMessage(statusLabel, $"Deleted note '{title}'.");
                }
                RefreshDataGridNotesFolderList();
                RefreshFolders();
                
            }
            catch (Exception exception)
            {
                MessageHandler.StatusMessage(statusLabel, "Couldn't delete: " + exception.Message);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveNote();
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            // this button will open a window to open a text file, then it will import the text file as long as everything is ok!
            try
            {

            }
            catch (Exception exception)
            {

            }
            finally
            {
                unfocusCurrentCell();
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (titleTextBox.Text == "" && noteTextBox.Text == "") throw new Exception("no text in fields.");
                DialogPrompt prompt = CreateDialogPrompt("Are you sure you want to", "clear your title and note?", "All progress will be lost.");
                DialogResult result = prompt.ShowDialog();
                if (result == DialogResult.Yes)
                {
                    noteTextBox.Clear();
                    titleTextBox.Clear();
                    MessageHandler.StatusMessage(statusLabel, "Cleared text fields.");
                }
                unfocusCurrentCell();
            }
            catch (Exception exception)
            {
                MessageHandler.StatusMessage(statusLabel, "Title box and notes box are already clear.");
            }
            
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            safelyCloseApp();
        }

        private void resizeButton_Click(object sender, EventArgs e)
        {
            // i would have to change this if i wanted the resize button to do other things,
            // but that probably wont happen lets be real
            updateWindowHeight(windowHeight);
            if (!ctrlHeldResizeButton)
            {
                switch (windowSizeStatus)
                {
                    case WindowSizeStatuses.Normal:
                        {
                            windowSizeStatus = WindowSizeStatuses.Half;
                            this.Size = windowSizeHalf;
                            resizeButton.Text = "□□";
                            settingsGroup.Visible = false;
                            break;
                        }

                    case WindowSizeStatuses.Half:
                        {
                            windowSizeStatus = WindowSizeStatuses.Normal;
                            this.Size = windowSizeNormal;
                            resizeButton.Text = "□";
                            settingsGroup.Visible = false;
                            break;
                        }

                    case WindowSizeStatuses.SettingsMenu:
                        {
                            windowSizeStatus = WindowSizeStatuses.Normal;
                            this.Size = windowSizeNormal;
                            resizeButton.Text = "□";
                            settingsGroup.Visible = false;
                            break;
                        }
                }
            }
            else
            {
                windowSizeStatus = WindowSizeStatuses.SettingsMenu;
                this.Size = windowSizeSettings;
                resizeButton.Text = "□□";
                settingsGroup.Visible = true;
            }
            
            unfocusCurrentCell();
        }

        private void resizeButton_MouseEnter(object sender, EventArgs e)
        {
            hoveringResizeButtonTimer.Start();
        }

        private void resizeButton_MouseLeave(object sender, EventArgs e)
        {
            hoveringResizeButtonTimer.Stop();
            // ensure that the button's text doesn't stick when leaving the button
            switch (windowSizeStatus)
            {
                case WindowSizeStatuses.Normal:
                    resizeButton.Text = "□";
                    break;
                case WindowSizeStatuses.Half:
                    resizeButton.Text = "□□";
                    break;
            }
        }

        private void hoveringResizeButtonTimer_Tick(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control && windowSizeStatus != WindowSizeStatuses.SettingsMenu)
            {
                resizeButton.Text = "□□□";
                /*
                 * There's definitely a better way of doing this.
                 * I wanted to include this bool as a status within enum WindowSizeStatuses, but
                 * I couldn't figure out how to have it work without windowSizeStatus (the variable)
                 * getting stuck at WaitingOptionsMenu (the enum value i wanted to make work)
                 * 
                 * maybe i fix later idk
                 */
                ctrlHeldResizeButton = true;
            }
            else
            {
                switch (windowSizeStatus)
                {
                    case WindowSizeStatuses.Normal:
                        resizeButton.Text = "□";
                        break;
                    case WindowSizeStatuses.Half:
                        resizeButton.Text = "□□";
                        break;
                }
                ctrlHeldResizeButton = false;
            }
            
            
        }

        // BEGIN SEARCH FUNCTIONS

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // again, we're comparing the title of each note to the contents of the search bar text, then acting accordingly.
            foreach (DataGridViewRow row in dataGridNotes.Rows)
            {
                // define elements needed for conditions
                string title = row.Cells[0].Value.ToString();
                DateTime date = DateTime.Parse(row.Cells[3].Value.ToString());
                string yearString = date.ToString("yyyy");
                string timeString = date.ToString("hh:mm tt");
                string folder = row.Cells[4].Value.ToString();
                dataGridNotes.CurrentCell = null; // this is necessary because if a cell is selected and it goes invisible, it crashes
                // define conditions for the search to be conclusive
                bool titleCondidion = title.StartsWith(searchTextBox.Text, StringComparison.OrdinalIgnoreCase);
                bool blankCondition = searchTextBox.Text != "";
                bool comboBoxCondition = folder == folderComboBox.Text || folderComboBox.Text == rootFolderName;
                bool dateCondition = date.ToString().StartsWith(searchTextBox.Text);
                bool yearCondition = yearString.StartsWith(searchTextBox.Text);
                bool timeCondition = timeString.StartsWith(searchTextBox.Text);
                // begin the search
                if (blankCondition && (titleCondidion || dateCondition || yearCondition || timeCondition) && comboBoxCondition)
                {
                    row.Visible = true;
                }
                else if (searchTextBox.Text == "")
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        private void RefreshFolders()
        {
            MessageHandler.DebugWrite("Refreshing folders!");
            List<string> folderNames = new List<string>();
            /*
             * This whole system is kinda complicated so im not surprised if ill forget how it works
             * First we have to grab the rows and add the folder names to a list
             * Then we clear the combo box's list
             * and add every folder name to the combo box's list
             * 
             * its very clever
             */
            foreach (DataGridViewRow row in dataGridNotes.Rows)
            {
                DataGridViewCell folderCell = row.Cells[4];
                string folderName = folderCell.Value.ToString();
                if (!folderNames.Contains(folderName))
                {
                    folderNames.Add(folderName);
                }
                
            }
            folderComboBox.Items.Clear();
            folderComboBox.Items.Add(rootFolderName);
            foreach (string folderName in folderNames)
            {
                if (folderName != rootFolderName)
                {
                    folderComboBox.Items.Add(folderName);
                }
            }
        }

        private void RefreshDataGridNotesFolderList()
        {
            MessageHandler.DebugWrite("Refreshing folder list!");
            /*
             * This method refreshes the dataGridView display according to the state of the combobox
             * it also includes some behaviors that disable broken behavior, like having blank name folders
             */
            bool nameExists = false;
            // determine if the string in combobox exists in its items list.
            // if it doesn't, then add the string to the list temporarily so the user can look at a blank screen.
            // if the user doesn't add a note under the folder's name, it'll be deleted through RefreshFolders()
            foreach (string folderName in folderComboBox.Items)
            {
                if (folderComboBox.Text == folderName) nameExists = true;
            }
            if (!nameExists && folderComboBox.Text != "")
            {
                folderComboBox.Items.Add(folderComboBox.Text);
            }
            // this foreach loop is fairly straightforward, it just compares the text in the combobox to the row's folder category value
            // once it's done a comparison, it acts accordingly
            foreach (DataGridViewRow row in dataGridNotes.Rows)
            {
                DataGridViewCell folderCell = row.Cells[4];
                string folderName = folderCell.Value.ToString();
                dataGridNotes.CurrentCell = null; // this is necessary because if a cell is selected and it goes invisible, it crashes
                if (folderComboBox.Text == rootFolderName)
                {
                    row.Visible = true;
                }
                else if (folderComboBox.Text != "" && folderName == folderComboBox.Text)
                {
                    row.Visible = true;
                }
                else
                {
                    row.Visible = false;
                }
            }
            if (folderComboBox.Text == "")
            {
                folderComboBox.Text = rootFolderName;
            }
        }

        private void folderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshDataGridNotesFolderList();
            
        }

        private void folderComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RefreshDataGridNotesFolderList();
                RefreshFolders();

                e.SuppressKeyPress = true;
                e.Handled = true;

                if (folderComboBox.Focused)
                {
                    this.Focus();
                }
            }
            
        }

        private void folderComboBox_Leave(object sender, EventArgs e)
        {
            RefreshDataGridNotesFolderList();
            RefreshFolders();
        }

        private void noteTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.S)
                {
                    saveNote();
                }
                if (e.KeyCode == Keys.C && !string.IsNullOrEmpty(noteTextBox.SelectedText))
                {
                    Clipboard.SetText(noteTextBox.SelectedText);
                }
                if (e.KeyCode == Keys.V && Clipboard.ContainsText())
                {
                    noteTextBox.SelectedText = Clipboard.GetText();
                }
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        // END SEARCH FUNCTIONS

        // BEGIN DATAGRIDVIEW FUNCTIONS

        private void cell_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // check if datagridview's content is there
                if (dataGridNotes.Rows.Count <= 0 || dataGridNotes.CurrentCell == null) throw new Exception("invalid cell selection!");

                // check if cell doesn't exist (idfk how this would happen)
                int index = dataGridNotes.CurrentCell.RowIndex;
                if (index <= -1) throw new Exception("cell is null!");

                titleTextBox.Text = table.Rows[index].ItemArray[0].ToString();
                noteTextBox.Text = table.Rows[index].ItemArray[1].ToString();
                MessageHandler.StatusMessage(statusLabel, $"Loaded note '{titleTextBox.Text}'.");

                folderComboBox.Text = dataGridNotes.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception exception)
            {
                MessageHandler.StatusMessage(statusLabel, "Couldn't open: " + exception.Message);
            }
            finally
            {
                unfocusCurrentCell();
            }
        }

        // END DATAGRIDVIEW FUNCTIONS

        // BEGIN SETTINGS FUNCTIONS

        // warning prompt settings
        private void setDeleteWithoutPrompt(bool value)
        {
            settingDeleteWithoutPromptCheckBox.Checked = value;
            settingDeleteWithoutPrompt = value;
        }

        private void settingDeleteWithoutPromptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setDeleteWithoutPrompt(settingDeleteWithoutPromptCheckBox.Checked);
        }

        private void setClearSaveWithoutPrompt(bool value)
        {
            settingClearSaveWithoutPromptCheckBox.Checked = value;
            settingClearSaveWithoutPrompt = value;
        }

        private void settingClearSaveWithoutPromptCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            setClearSaveWithoutPrompt(settingClearSaveWithoutPromptCheckBox.Checked);
        }

        // color settings


        // MAKE THIS CODE BETTER LATER, IM TIRED AND SLEEPY :(
        private void colorTrackBar_Scroll(object sender, EventArgs e)
        {
            updateColorLabels();
            updateColors();
        }

        private void updateColorLabels()
        {
            colorRLabel.Text = colorRTrackBar.Value.ToString();
            colorGLabel.Text = colorGTrackBar.Value.ToString();
            colorBLabel.Text = colorBTrackBar.Value.ToString();
        }

        private void updateColors()
        {
            switch (colorEditStatus)
            {
                case ColorEditStatus.Background:
                    backgroundColor = Color.FromArgb(colorRTrackBar.Value, colorGTrackBar.Value, colorBTrackBar.Value);
                    this.BackColor = backgroundColor;
                    break;
                case ColorEditStatus.TitleBar:
                    titleBarColor = Color.FromArgb(colorRTrackBar.Value, colorGTrackBar.Value, colorBTrackBar.Value);
                    foreach (Control control in titleControls)
                    {
                        control.BackColor = titleBarColor;
                    }
                    break;
                case ColorEditStatus.Button:
                    buttonColor = Color.FromArgb(colorRTrackBar.Value, colorGTrackBar.Value, colorBTrackBar.Value);
                    foreach (Control control in buttons)
                    {
                        control.BackColor = buttonColor;
                    }
                    break;
                case ColorEditStatus.Highlight:
                    highlightColor = Color.FromArgb(colorRTrackBar.Value, colorGTrackBar.Value, colorBTrackBar.Value);
                    dataGridNotes.DefaultCellStyle.SelectionBackColor = highlightColor;
                    break;
                case ColorEditStatus.Text:
                    textColor = Color.FromArgb(colorRTrackBar.Value, colorGTrackBar.Value, colorBTrackBar.Value);
                    dataGridNotes.DefaultCellStyle.ForeColor = textColor;
                    dataGridNotes.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255 - textColor.R, 255 - textColor.G, 255 - textColor.B);
                    foreach (Control control in controlsWithText)
                    {
                        control.ForeColor = textColor;
                    }
                    break;
                
                case ColorEditStatus.TextBox:
                    textBoxColor = Color.FromArgb(colorRTrackBar.Value, colorGTrackBar.Value, colorBTrackBar.Value);
                    dataGridNotes.BackgroundColor = textBoxColor;
                    dataGridNotes.DefaultCellStyle.BackColor = textBoxColor;
                    foreach (Control control in textBoxes)
                    {
                        control.BackColor = textBoxColor;
                    }
                    break;
            }
        }

        private void updateColorTrackBarValues()
        {
            Dictionary<ColorEditStatus, Color> colorMap = new Dictionary<ColorEditStatus, Color>
            {
                { ColorEditStatus.Background, backgroundColor },
                { ColorEditStatus.Button, buttonColor },
                { ColorEditStatus.Highlight, highlightColor },
                { ColorEditStatus.Text, textColor },
                { ColorEditStatus.TextBox, textBoxColor },
                { ColorEditStatus.TitleBar, titleBarColor }
            };

            if (colorMap.TryGetValue(colorEditStatus, out Color color))
            {
                colorRTrackBar.Value = color.R;
                colorGTrackBar.Value = color.G;
                colorBTrackBar.Value = color.B;
            }
            updateColorLabels();
        }

        private void colorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                colorEditStatus = (ColorEditStatus)Enum.Parse(typeof(ColorEditStatus), colorComboBox.Text);
                updateColorTrackBarValues();
            }
            catch (Exception exception)
            {
                MessageHandler.DebugWrite(exception.Message);
            }
            
        }
    }



    [Serializable]
    public class RawRowsPacket
    {
        public List<string> titles;
        public List<string> notes;
        public List<DateTime> dates;
        public List<DateTime> originalDates;
        public List<DateTime> modifiedDates;
        public List<string> folders;
    }

    
}
