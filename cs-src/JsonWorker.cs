using System.IO;
using System.Reflection;

namespace cs_src
{
    public class JsonWorker
    {
        private string _pathForThisApp; 
        private string _pathForLocalJsonFile; 
        
        public JsonWorker(Assembly executingAssembly)
        {
            _pathForThisApp = executingAssembly.Location;
            _pathForLocalJsonFile = Path.Combine(_pathForThisApp, Configuration.LocalJsonFilename);
        }

        public bool CheckForLocalJsonFile()
        {
            return File.Exists(_pathForLocalJsonFile);
        }

        public string GetLocalJsonFilePath()
        {
            return _pathForLocalJsonFile;
        }
        
        public void UpdateInventorFromJson(string path)
        {
            // Make json update inventor settings here.
            
        }
    }
}