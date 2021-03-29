using System;
using System.Collections.Generic;
using System.Text;

namespace InventorConfig
{
    public class ConfigFileWriteOnly : ConfigFile
    {
        public ConfigFileWriteOnly(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new System.Exception("The output path is blank. Exiting.");
            }

            Path = path;
        }
    }
}
