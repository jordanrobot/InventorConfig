using System.IO;
using System.Reflection;

namespace ConfigLoaderConsole
{
    public class ConfigFile
    {
        public string FilePath { get; set; }

        private readonly string _defaultConfigFileName = "config-loader.json";
        private readonly string _currentDirectory = System.Environment.CurrentDirectory;
        private readonly string _executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public ConfigFile(ArgumentWorker argWorker)
        {
            if (argWorker.PathSwitchExists)
            {
                var _filePath = argWorker.Path;

                if (!Path.IsPathRooted(_filePath))
                {
                    _filePath = Path.Combine(_currentDirectory, _filePath);
                }

                if (File.Exists(_filePath))
                {
                    FilePath = _filePath;
                    return;
                }

                FilePath = null;
                return;
            }

            var _defaultConfigFullPath = Path.Combine(_executableLocation, _defaultConfigFileName);

            if (File.Exists(_defaultConfigFullPath))
            {
                FilePath = _defaultConfigFullPath;
                return;
            }
        }
    }
}