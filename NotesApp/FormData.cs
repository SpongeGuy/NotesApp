using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using NotesApp;

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

        public int windowHeight;
        public NotesApp.WindowSizeStatuses windowSizeStatus;

        public FormData(NotesApp app)
        {
            table = app.table;
            packet = app.GetRawRows();
            titles = packet.titles;
            notes = packet.notes;
            originalDates = packet.originalDates;
            modifiedDates = packet.modifiedDates;
            folders = packet.folders;

            windowHeight = app.windowHeight;
            windowSizeStatus = app.windowSizeStatus;
            
            
        }
    }
}
