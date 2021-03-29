using System;
using Newtonsoft.Json;

namespace InventorConfig
{
    public class ConfigLoader : ConfigEngine
    {
        public ConfigLoader(string path)
        {
            ConfigFileReadOnly configFile = new ConfigFileReadOnly(path);
            Config = DeserializeConfiguration(configFile.Contents);
            DecideIfWeShouldCloseInventorAfterCompletion();
            App = GetInventorInstance();
            Config.LoadConfigurationIntoInventor(App);
            CloseInventorIfRequired();
        }

        private Configuration DeserializeConfiguration(string content)
        {
            try
            {
                return JsonConvert.DeserializeObject<Configuration>(content);
            }
            catch (Exception e)
            {
                throw new SystemException("The configuration was invalid, please verify json file syntax.  Process was aborted, press any key to continue...", e);
            }
        }
    }
}
