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
        public List<DateTime> dates;
        public RawRowsPacket packet;

        public FormData(NotesApp app)
        {
            table = app.table;
            packet = app.GetRawRows();
            titles = packet.titles;
            notes = packet.notes;
            dates = packet.dates;
            
        }
    }
}
