using System;
using System.IO;
using Newtonsoft.Json;

namespace ConfigLoader
{
    public class ConfigEngine
    {
        private string _configRaw;
        private bool _closeApp;
        private Configuration Config { get; set; }
        private Inventor.Application App { get; set; }

        public ConfigEngine(string _configPath, bool test = false)
        {
            _configRaw = GetFileContents(_configPath);
            Config = DeserializeConfiguration();

            if (test)
                return;

            App = GetInventorInstance();
            Config.apply(App);
            CloseApp();
        }

        private string GetFileContents(string _configPath)
        {
            try
            {
                return File.ReadAllText(_configPath);
            }
            catch (Exception e)
            {
                throw new SystemException("There was an error reading the json configuration file from disk.  Process was aborted, press any key to continue...", e);
            }
        }

        private Configuration DeserializeConfiguration()
        {
            try
            {
                return JsonConvert.DeserializeObject< Configuration>(_configRaw);
            }
            catch (Exception e)
            {
                throw new SystemException("The configuration was invalid, please verify json file syntax.  Process was aborted, press any key to continue...", e);
            }
        }

        private Inventor.Application GetInventorInstance()
        {
            var instance = InventorInstance.GetInventorAppReference();
            if (instance != null)
                return instance;

            try
            {
                _closeApp = true;
                Type appType = Type.GetTypeFromProgID("Inventor.Application");
                return (Inventor.Application)Activator.CreateInstance(appType);
            }
            catch (Exception e)
            {
                throw new SystemException("The Inventor application could not be started on this computer.  Is it installed?  Process aborted, press any key to continue...", e);
            }
        }

        private void CloseApp()
        {
            if (_closeApp)
            {
                App.Quit();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}