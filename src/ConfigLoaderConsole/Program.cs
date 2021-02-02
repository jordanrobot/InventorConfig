using System;
using ConfigLoader;

namespace ConfigLoaderConsole
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var argWorker = new ArgumentWorker(args);

            #region Argument Switches
            if (argWorker.HelpSwitchExists)
            {
                View.ShowHelp();
                return;
            }

            if (argWorker.VersionSwitchExists)
            {
                View.ShowVersion();
                return;
            }
            #endregion Argument Switches

            View.ShowBanner();

            ConfigFile configFile = new ConfigFile(argWorker);

            if (configFile.FilePath is null)
            {
                View.ShowError();
                Console.ReadLine();
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine(configFile.FilePath);
                Console.WriteLine("The options have been set!  Press enter to exit.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message, e);
            }
        }
    }
}