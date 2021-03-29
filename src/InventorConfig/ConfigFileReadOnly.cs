using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace InventorConfig
{
    public class ConfigFileReadOnly : ConfigFile
    {
        public ConfigFileReadOnly(string path)
        {
            _defaultConfigFullPath = System.IO.Path.Combine(_executableLocation, _defaultConfigFileName);

            //If the input string is empty, try the default file...
            if (string.IsNullOrEmpty(path))
                path = _defaultConfigFullPath;

            SetFilePathIfFileExists(path);

            //This also throws an error if the file cannot be read...
            GetConfigFileContents();
        }
    }
}
