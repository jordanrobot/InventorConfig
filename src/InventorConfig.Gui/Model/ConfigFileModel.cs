using System;

namespace InventorConfig.Gui.Model
{
    public class ConfigFileModel
    {
        public string FilePath { get; set; }

        public ConfigFileModel()
        {
        }

        public bool ApplyConfig()
        {
            if (String.IsNullOrEmpty(FilePath))
            {
                return false;
            }

            //modify the Inventor config
            try
            {
                ConfigLoader configLoader = new ConfigLoader(FilePath);
                return true;
            }
            catch (Exception ex)
            {
                //StatusRed("Configuration failed to load.");
                throw new SystemException(ex.Message, ex);
            }
        }
    }
}