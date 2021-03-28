using System;
using System.IO;
using Newtonsoft.Json;

namespace InventorConfig
{
    public class ConfigEngine
    {
        private string _configRawText;
        private bool _closeInventorAfterCompletion;
        private Configuration Config { get; set; }
        private Inventor.Application App { get; set; }

        public void LoadConfigFromFile(string _configPath)
        {
            _configRawText = GetConfigFileContents(_configPath);
            Config = DeserializeConfiguration();
            DecideIfWeShouldCloseInventorAfterCompletion();
            App = GetInventorInstance();
            Config.LoadConfigurationIntoInventor(App);
            CloseInventorIfRequired();
        }

        public void WriteConfigToFile(string _configPath)
        {
            DecideIfWeShouldCloseInventorAfterCompletion();
            App = GetInventorInstance();

            Config = new Configuration();
            Config.GetConfigurationFromInventor(App);
            SerializeConfiguration(Config, _configPath);

            CloseInventorIfRequired();
        }

        public static string GetConfigFileContents(string _configPath)
        {
            try
            {
                using (var sr = new StreamReader(_configPath))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                throw new SystemException("There was an error reading the JSON configuration file from disk.  Process was aborted, press any key to continue...", ex);
            }
        }

        private Configuration DeserializeConfiguration()
        {
            try
            {
                return JsonConvert.DeserializeObject< Configuration>(_configRawText);
            }
            catch (Exception e)
            {
                throw new SystemException("The configuration was invalid, please verify json file syntax.  Process was aborted, press any key to continue...", e);
            }
        }

        private Inventor.Application GetInventorInstance()
        {
            try
            {
                var i = InventorInstance.GetInventorAppReference();
                return i;
            }
            catch (Exception e)
            {
                throw new SystemException("The Inventor application could not be started on this computer.  Is it installed?  Process aborted, press any key to continue...", e);
            }
        }

        private void DecideIfWeShouldCloseInventorAfterCompletion()
        {
            if (InventorInstance.NumberOfRunningInventorInstances() == 0)
            { _closeInventorAfterCompletion = true; }
        }

        private void CloseInventorIfRequired()
        {
            if (_closeInventorAfterCompletion)
            {
                App.Quit();
                GC.WaitForPendingFinalizers();
            }
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