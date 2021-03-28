using CommandLine;
using CommandLine.Text;
using System;

namespace InventorConfig.Console
{
    public static class ConsoleUI
    {
        public static void ConsoleStart(Options options)
        {
            if (string.IsNullOrEmpty(options.Output))
            {
                LoadConfigIntoInventor(options.Path);
            }
            else
            {
                CreateNewConfigFromInventor(options.Output);
            }
        }

        private static void CreateNewConfigFromInventor(string output)
        {
            ShowReadBanner();

            ConfigFile configFile = new ConfigFile();
            configFile.SetWriteConfigFilePath(output);

            if (configFile.Path is null)
            {
                ShowNoValidConfigFileError();
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine();
                configEngine.WriteConfigToFile(configFile.Path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                //throw new SystemException(e.Message, e);
            }
        }

        private static void LoadConfigIntoInventor(string path)
        {
            ShowLoadBanner();

            ConfigFile configFile = new ConfigFile();
            configFile.SetApplyConfigFilePath(path);

            if (configFile.Path is null)
            {
                ShowNoValidConfigFileError();
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine();
                configEngine.LoadConfigFromFile(configFile.Path);
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                //throw new SystemException(e.Message, e);
            }
        }

        #region Screens
        private static void ShowComplete()
        {
            System.Console.WriteLine("Configuration update complete.");
        }

        private static void ShowNoValidConfigFileError()
        {
            // Prompt user here
            System.Console.WriteLine(HeadingInfo.Default);
            System.Console.WriteLine("Could not find a valid JSON config file.  Exiting.");
        }

        private static void ShowLoadBanner()
        {
            System.Console.WriteLine("Attempting to edit your Autodesk Inventor configuration.  Please stand by...");
        }

        private static void ShowReadBanner()
        {
            System.Console.WriteLine("Attempting to read your Autodesk Inventor configuration.  Please stand by...");
        }
        #endregion Screens
    }
}
