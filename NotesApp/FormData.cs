using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using NotesApp;
using System.Drawing;

namespace NotesApp
{
    [System.Serializable]
    public class FormData
    {
        public DataTable table;
        public List<string> titles;
        public List<string> notes;
        public List<DateTime> originalDates;
        public List<DateTime> modifiedDates;
        public List<string> folders;
        public RawRowsPacket packet;

        // window height
        public int windowHeight;
        public NotesApp.WindowSizeStatuses windowSizeStatus;

        // warning prompt
        public bool settingDeleteWithoutPrompt;
        public bool settingClearSaveWithoutPrompt;

        // colors
        public Color backgroundColor;
        public Color titleBarColor;
        public Color buttonColor;
        public Color textColor;
        public Color highlightColor;
        public Color textBoxColor;

        public FormData(NotesApp app)
        {
            table = app.table;
            packet = app.GetRawRows();
            titles = packet.titles;
            notes = packet.notes;
            originalDates = packet.originalDates;
            modifiedDates = packet.modifiedDates;
            folders = packet.folders;

            // window height
            windowHeight = app.windowHeight;
            windowSizeStatus = app.windowSizeStatus;

            // warning prompt
            settingDeleteWithoutPrompt = app.settingDeleteWithoutPrompt;
            settingClearSaveWithoutPrompt = app.settingClearSaveWithoutPrompt;

            // colors
            backgroundColor = app.backgroundColor;
            titleBarColor = app.titleBarColor;
            buttonColor = app.buttonColor;
            textColor = app.textColor;
            highlightColor = app.highlightColor;
            textBoxColor = app.textBoxColor;

            
        }
    }
}
