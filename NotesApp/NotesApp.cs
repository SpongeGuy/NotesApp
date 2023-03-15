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
        enum WindowSizeStatuses
        {
            Normal,
            WaitingSettingsMenu,
            SettingsMenu,
            Half
        }
        public DataTable table;
        WindowSizeStatuses windowSizeStatus;
        
        

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
        // end bullshit

        // BEGIN SETTINGS
        public Size windowSizeHalf = new Size(554, 481);
        public Size windowSizeNormal = new Size(1027, 481);
        public Size windowSizeSettings = new Size(1344, 481);

        //public Color formBackground = new Color(Color.FromArgb);






        // END SETTINGS


        // --------------------INITIALIZATION AND MISC--------------------
        public NotesApp()
        {
            // SET ALL CONTROL PROPERTIES HERE
            InitializeComponent();
            windowSizeStatus = WindowSizeStatuses.Normal;
            this.Size = windowSizeNormal;

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
            // END CONTROL PROPERTIES
        }

        private void LoadApp(object sender, EventArgs e)
        {
            try
            {
                table = new DataTable();
                table.Columns.Add("Title", typeof(String));
                table.Columns.Add("Note", typeof(String));
                table.Columns.Add("Date", typeof(DateTime));
                // add date type

                FormData data = Serializer.LoadData();
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
                        row["Date"] = data.packet.dates[index];
                    }
                    dataGridNotes.DataSource = table;
                }

                int dateWidth = dataGridNotes.Width / 3;
                int titleWidth = dataGridNotes.Width / 2 + (dataGridNotes.Width / 2 - dateWidth);

                dataGridNotes.Columns["Title"].Width = titleWidth;
                dataGridNotes.Columns["Note"].Visible = false;
                dataGridNotes.Columns["Date"].Visible = true;
                dataGridNotes.Columns["Date"].Width = dateWidth;


                dataGridNotes.ClearSelection();
                
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
            List<DateTime> dates = new List<DateTime>();
            foreach (DataGridViewRow row in dataGridNotes.Rows)
            {
                // add the cell values to each list
                titles.Add(row.Cells[0].Value.ToString());
                notes.Add(row.Cells[1].Value.ToString());
                dates.Add(DateTime.Parse(row.Cells[2].Value.ToString()));
            }
            RawRowsPacket packet = new RawRowsPacket();
            packet.titles = titles;
            packet.notes = notes;
            packet.dates = dates;
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
                        dataGridNotes.Rows[index].Cells[2].Value = DateTime.Now;
                        MessageHandler.StatusMessage(statusLabel, $"Overwrote note '{title}'.");
                        status = SaveStatus.Overwrite;
                    }
                }

                if (status == SaveStatus.Normal)
                {
                    MessageHandler.StatusMessage(statusLabel, $"Saved note as '{titleTextBox.Text}'.");
                    table.Rows.Add(titleTextBox.Text, noteTextBox.Text, DateTime.Now);
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

        // --------------------OPTIONS FIELDS--------------------

    }

    [Serializable]
    public class RawRowsPacket
    {
        public List<string> titles;
        public List<string> notes;
        public List<DateTime> dates;
    }

    public class MessageHandler
    {
        static System.Timers.Timer timer = new System.Timers.Timer(3000);

        public static void DebugWrite(params string[] messages)
        {
            foreach (string message in messages)
            {
                System.Diagnostics.Debug.WriteLine(message);
            }
        }

        public static void StatusMessage(System.Windows.Forms.Label label, string message)
        {
            timer.Stop();

            // this is my dumbass patchwork way to reset the timer
            timer.Enabled = false;
            timer.Elapsed -= DummyEventHandler;
            timer.Elapsed += DummyEventHandler;
            timer.Enabled = true;

            timer.Elapsed += (sender, e) =>
            {
                // this invoke ensures that the command inside is called on the same thread as the one that created the control
                // without the invoke the program would crash when this is called
                label.Invoke((MethodInvoker)delegate
                {
                    label.Visible = false;
                });
            };
            timer.AutoReset = false;

            label.Visible = true;
            label.Text = message;
            timer.Start();
        }

        public static void DummyEventHandler(object sender, EventArgs e) { }
    }
}
