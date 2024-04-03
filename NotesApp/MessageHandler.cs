using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesApp
{
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

        public static void DebugWrite(List<string> messages)
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
