using System;
using System.Reflection;

namespace InventorConfig.Gui.ViewModel
{
    public class AboutViewModel: BaseViewModel
    {
        private readonly static string _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public String VersionText { get => "InventorConfig Version: " + _version; }
        public string Link { get => "https://github.com/jordanrobot/InventorConfig"; }
        public string AboutText { get => "A simple tool that loads application options into Autodesk Inventor from a JSON file. Useful for (re)setting user's configs quickly.\n\n Find documentation and the latest version at github here:"; }
    }
}