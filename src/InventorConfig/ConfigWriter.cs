using System;
using System.IO;
using Newtonsoft.Json;

namespace InventorConfig
{
    public class ConfigWriter : ConfigEngine
    {
        public ConfigWriter(string path)
        {
            ConfigFileWriteOnly configFile = new ConfigFileWriteOnly(path);

            DecideIfWeShouldCloseInventorAfterCompletion();
            App = GetInventorInstance();

            Config = new Configuration();
            Config.GetConfigurationFromInventor(App);

            SerializeConfiguration(Config, configFile.Path);

            CloseInventorIfRequired();
        }

        private void SerializeConfiguration(Configuration config, string outputFile)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.NullValueHandling = NullValueHandling.Ignore;

            using (var sw = new StreamWriter(outputFile))
            {
                JsonWriter writer = new JsonTextWriter(sw);
                serializer.Serialize(writer, config);
                writer.Close();
            }
        }
    }
}
