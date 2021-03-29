using System;
using System.IO;
using System.Reflection;

namespace InventorConfig
{
    public class ConfigFile
    {
        protected const string _defaultConfigFileName = "default.json";
        protected readonly string _currentDirectory = System.Environment.CurrentDirectory;
        protected readonly string _executableLocation = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected string _defaultConfigFullPath;

        private string _path;
        public string Path
        {
            get { return _path; }
            set { _path = ReturnAbsolutePath(value); }
        }

        public string Contents { get; set; }

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

        protected string ReturnAbsolutePath(string path)
        {
            GuardAgainstEmptyPath(path);

            if (System.IO.Path.IsPathRooted(path))
                return path;

            return System.IO.Path.Combine(_currentDirectory, path);
        }

        protected void GuardAgainstInvalidFile(string path)
        {
            if (File.Exists(path))
                return;

            throw new SystemException("The specified file could not be found; exiting.");
        }

        protected void GuardAgainstEmptyPath(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new Exception("The configuration path is empty or the specified file cannot be found; exiting.");
        }
    }
}