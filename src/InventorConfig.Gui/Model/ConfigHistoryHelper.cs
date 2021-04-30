using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace InventorConfig.Gui.Model
{
    public static class ConfigHistoryHelper
    {
        private readonly static string userDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventorConfig");
        private const string userConfigFileName = "ConfigHistory.xml";

        public static bool DoesConfigHistoryExist()
        {
            if (!System.IO.Directory.Exists(userDirectory))
            {
                System.IO.Directory.CreateDirectory(userDirectory);
            }

            return File.Exists(Path.Combine(userDirectory, userConfigFileName));
        }

        public static ObservableCollection<string> GetHistoryFromUserStorage()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<string>));

            using (StreamReader streamReader = new StreamReader(Path.Combine(userDirectory, userConfigFileName)))
            {
                return (ObservableCollection<string>)serializer.Deserialize(streamReader);
            }
        }

        public static void WriteHistoryToUserStorage(ObservableCollection<string> value)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<string>));

            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(userDirectory, userConfigFileName)))
            {
                serializer.Serialize(streamWriter, value);
            }
        }
    }
}