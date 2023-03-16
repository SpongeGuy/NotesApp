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

        public int defaultWindowHeight = 550;
        public int windowHeight;
        public Size windowSizeHalf;
        public Size windowSizeNormal;
        public Size windowSizeSettings;

        public string rootFolderName = "All";



        // END SETTINGS


        // --------------------INITIALIZATION AND MISC--------------------
        public NotesApp()
        {
            // BEGIN CONTROL PROPERTIES
            InitializeComponent();
            

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

        private void LoadApp(object sender, EventArgs e)
        {
            FormData data = Serializer.LoadData();
            if (data != null)
            {
                firstLaunch = false;
            }
            if (firstLaunch)
            {
                // SET DEFAULT VALUES FOR FIRST LAUNCH HERE
                windowSizeStatus = WindowSizeStatuses.Normal;
                windowHeight = 550;
                updateWindowHeight(windowHeight);
                this.Size = windowSizeNormal;
            }
            else
            {
                // LOAD SETTINGS FROM DATA HERE

                // begin window size load
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
                // end window size load
                
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

        private DialogResult CreateDialogPrompt(params string[] messages)
        {
            DialogPrompt prompt = new DialogPrompt();
            prompt.ChangePromptMessage(messages);
            return prompt.ShowDialog();
        }

        private void unfocusCurrentCell()
        {
            if (dataGridNotes.CurrentCell != null)
            {
                dataGridNotes.ClearSelection();
                dataGridNotes.CurrentCell = null;
            }
        }

        private void safelyCloseApp()
        {
            Serializer.SaveData(this);
            this.Close();
        }

        // --------------------BUTTONS BEHAVIORS--------------------
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
                DialogResult result = CreateDialogPrompt("Are you sure you want to", "delete this note?", $"Everything in '{title}'", "will be lost.");
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
                        DialogResult result = CreateDialogPrompt($"A note called '{title}'", "already exists.", "Do you want to overwrite it?");
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
                
                titleTextBox.Clear();
                noteTextBox.Clear();
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

        private void clearButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (titleTextBox.Text == "" && noteTextBox.Text == "") throw new Exception("no text in fields.");
                DialogResult result = CreateDialogPrompt("Are you sure you want to", "clear your title and note?", "All progress will be lost.");
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

        private void searchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // again, we're comparing the title of each note to the contents of the search bar text, then acting accordingly.
            foreach (DataGridViewRow row in dataGridNotes.Rows)
            {
                string title = row.Cells[0].Value.ToString();
                string folder = row.Cells[4].Value.ToString();
                dataGridNotes.CurrentCell = null; // this is necessary because if a cell is selected and it goes invisible, it crashes
                if (searchTextBox.Text != "" && title.StartsWith(searchTextBox.Text, StringComparison.OrdinalIgnoreCase) && (folder == folderComboBox.Text || folderComboBox.Text == rootFolderName))
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
            MessageHandler.DebugWrite(folderNames);
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
            }
        }

        private void folderComboBox_Leave(object sender, EventArgs e)
        {
            RefreshDataGridNotesFolderList();
            RefreshFolders();
        }

        // --------------------OPTIONS FIELDS--------------------

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
