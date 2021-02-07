using System;
using ConfigLoader;
using CommandLine;
using CommandLine.Text;

namespace ConfigLoaderConsole
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var results = Parser.Default.ParseArguments<Options>(args);
            results.WithParsed(options => ConsoleStart(options));
        }

        private static void ConsoleStart(Options options)
        {
            if (string.IsNullOrEmpty(options.output))
            {
                LoadConfig(options);
            }
            else
            {
                WriteConfig(options);
            }
        }

        private static void WriteConfig(Options options)
        {
            ShowReadBanner();

            ConfigFile configFile = new ConfigFile();
            configFile.GetWriteConfigFile(options.output);

            if (configFile.FilePath is null)
            {
                ShowNoValidConfigFileError();
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine();
                configEngine.WriteConfig(configFile.FilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw new SystemException(e.Message, e);
            }
        }

        private static void LoadConfig(Options options)
        {
            ShowLoadBanner();

            ConfigFile configFile = new ConfigFile();
            configFile.GetLoadConfigFile(options.path);

            if (configFile.FilePath is null)
            {
                ShowNoValidConfigFileError();
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine();
                configEngine.LoadConfig(configFile.FilePath, options.test);

                if (options.test)
                {
                    ShowTest();
                }
                else
                {
                    ShowComplete();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //throw new SystemException(e.Message, e);
            }
        }

        #region Screens
        private static void ShowComplete()
        {
            Console.WriteLine("Configuration update complete.");
        }

        private static void ShowTest()
        {
            Console.WriteLine("The json configuration file was valid; nothing was changed in Inventor's configuration.");
        }

        private static void ShowNoValidConfigFileError()
        {
            // Prompt user here
            Console.WriteLine(HeadingInfo.Default);
            Console.WriteLine("Could not find a valid json config file.  Exiting.");
        }

        private static void ShowLoadBanner()
        {
            Console.WriteLine("Attempting to edit your Autodesk Inventor configuration.  Please stand by...");
        }

        private static void ShowReadBanner()
        {
            Console.WriteLine("Attempting to read your Autodesk Inventor configuration.  Please stand by...");
        }
        #endregion Screens

    }
}