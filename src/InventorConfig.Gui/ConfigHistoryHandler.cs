using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace InventorConfig.Gui
{
    class ConfigHistoryHandler
    {
        //public HashSet<string> configHistory;
        public ConfigHistoryFile Configs;
        private string userDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "InventorConfig");
        private string userConfigFileName = "ConfigHistory.xml";

        public ConfigHistoryHandler()
        {
            if (!System.IO.Directory.Exists(userDirectory))
            {
                System.IO.Directory.CreateDirectory(userDirectory);
            }

            if (File.Exists(Path.Combine(userDirectory, userConfigFileName)))
            {
                GetHistoryFromUserStorage();
            }
            else
            {
                //configHistory = new HashSet<string> { };
                Configs = new ConfigHistoryFile();
                WriteHistoryToUserStorage();
            }
        }

        public void SaveConfigHistory()
        {
            WriteHistoryToUserStorage();
        }

        private void GetHistoryFromUserStorage()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigHistoryFile));

            using (StreamReader streamReader = new StreamReader(Path.Combine(userDirectory, userConfigFileName)))
            {
                Configs = (ConfigHistoryFile)serializer.Deserialize(streamReader);
            }

        }

        private void WriteHistoryToUserStorage()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ConfigHistoryFile));

            using (StreamWriter streamWriter = new StreamWriter(Path.Combine(userDirectory, userConfigFileName)))
            {
                serializer.Serialize(streamWriter, Configs);
            }
        }

        public HashSet<string> GetConfigHistoryFile() => Configs.Configs;

        public void AddToConfigHistoryFile(string item)
        {
            Configs.Add(item);
            SaveConfigHistory();
        }

        public void RemoveToConfigHistoryFile(string item)
        {
            Configs.Remove(item);
        }
    }
}
