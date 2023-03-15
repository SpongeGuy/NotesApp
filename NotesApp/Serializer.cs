using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NotesApp
{
    public static class Serializer
    {
        public static void SaveData(NotesApp app)
        {
            // create formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // define path to save to
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "Sponge");

            // create directory if needed
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // set name of file
            path = Path.Combine(path, "data.spig");

            // create the file
            FileStream stream = new FileStream(path, FileMode.Create);

            // get the data
            FormData data = new FormData(app);

            // write the data to the file
            formatter.Serialize(stream, data);

            // close the file
            stream.Close();

            MessageHandler.DebugWrite("Wrote to file: " + path);
        }

        public static FormData LoadData()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            path = Path.Combine(path, "Sponge", "data.spig");

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();

                FileStream stream = new FileStream(path, FileMode.Open);

                FormData data = formatter.Deserialize(stream) as FormData;

                stream.Close();

                MessageHandler.DebugWrite("Data loaded successfully!");
                return data;
            }
            else
            {
                MessageHandler.DebugWrite("Save file is not found at: " + path);
                return null;
            }
        }

    }
}
