using System;
using System.IO;
using System.Reflection;

namespace InventorConfig
{
    public class ConfigFile
    {
        public string Path { get; set; }
        public string Contents { get; set; }
        //TODO: make the ConfigFile get the filecontents!!!
        //TODO: constructor with the file path and write/read parameter options

        private readonly string _defaultConfigFileName = "default.json";
        private readonly string _currentDirectory = System.Environment.CurrentDirectory;
        private readonly string _executableLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private string _defaultConfigFullPath;

        public ConfigFile()
        {
            _defaultConfigFullPath = System.IO.Path.Combine(_executableLocation, _defaultConfigFileName);
        }

        public void SetApplyConfigFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //try the default file....
                SetFilePathIfFileExists(_defaultConfigFullPath);
                return;
            }

            //Is the specified file absolute?
            path = ReturnAbsolutePath(path);

            SetFilePathIfFileExists(path);
            return;
        }

        public void SetWriteConfigFilePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new System.Exception("The output path is blank. Exiting.");
            }

            //Is the specified file absolute?
            path = ReturnAbsolutePath(path);

            Path = path;
            return;
        }

        private void SetFilePathIfFileExists(string path)
        {
            Path = (File.Exists(path)) ? path : null;
        }

        public string ReturnAbsolutePath(string path)
        {
            if (!System.IO.Path.IsPathRooted(path))
                return System.IO.Path.Combine(_currentDirectory, path);

            return path;
        }
    }
}