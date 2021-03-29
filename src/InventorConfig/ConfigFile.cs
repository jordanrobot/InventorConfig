using System;
using System.IO;
using System.Reflection;

namespace InventorConfig
{
    public class ConfigFile
    {
        private string _path;

        public string Path
        {
            get { return _path; }
            set { _path = ReturnAbsolutePath(value); }
        }

        public string Contents { get; set; }

        protected const string _defaultConfigFileName = "default.json";
        protected readonly string _currentDirectory = System.Environment.CurrentDirectory;
        protected readonly string _executableLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected string _defaultConfigFullPath;

        public ConfigFile()
        {
            _defaultConfigFullPath = System.IO.Path.Combine(_executableLocation, _defaultConfigFileName);
        }

        protected void GetConfigFileContents()
        {
            try
            {
                using (var sr = new StreamReader(Path))
                {
                    Contents = sr.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                throw new SystemException("There was an error reading the JSON configuration file from disk.  Process was aborted, press any key to continue...", ex);
            }
        }

        protected void SetFilePathIfFileExists(string path)
        {
            Path = (File.Exists(path)) ? path : null;
        }

        protected string ReturnAbsolutePath(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new Exception("The configuration path is empty; exiting.");

            if (System.IO.Path.IsPathRooted(path))
                return path;

            return System.IO.Path.Combine(_currentDirectory, path);
        }
    }
}