using System.IO;
using System.Reflection;

namespace InventorConfig
{
    public class ConfigFile
    {
        public string FilePath { get; set; }

        private readonly string _defaultConfigFileName = "default.json";
        private readonly string _currentDirectory = System.Environment.CurrentDirectory;
        private readonly string _executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public ConfigFile()
        {
        }

        public void GetLoadConfigFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //try the default file....
                var _defaultConfigFullPath = Path.Combine(_executableLocation, _defaultConfigFileName);

                SetFilePathIfExists(_defaultConfigFullPath);
                return;
            }

            //Is the specified file absolute?
            if (!Path.IsPathRooted(path))
                path = Path.Combine(_currentDirectory, path);

            SetFilePathIfExists(path);
            return;
        }

        public void GetWriteConfigFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new System.Exception("The output path is blank. Exiting.");
            }

            //Is the specified file absolute?
            if (!Path.IsPathRooted(path))
                path = Path.Combine(_currentDirectory, path);

            FilePath = path;
            return;
        }

        private void SetFilePathIfExists(string path)
        {
            if (File.Exists(path))
            {
                FilePath = path;
                return;
            }
        }
    }
}