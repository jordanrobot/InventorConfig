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

        public ConfigFile(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //try the default file....
                var _defaultConfigFullPath = Path.Combine(_executableLocation, _defaultConfigFileName);

                if (File.Exists(_defaultConfigFullPath))
                {
                    FilePath = _defaultConfigFullPath;
                    return;
                }
            }

            //Is the specified file absolute?
            if (!Path.IsPathRooted(path))
                path = Path.Combine(_currentDirectory, path);

            if (File.Exists(path))
            {
                FilePath = path;
                return;
            }

            FilePath = null;
            return;
        }

    }
}