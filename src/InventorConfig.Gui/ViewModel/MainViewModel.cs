using InventorConfig.Gui.Model;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace InventorConfig.Gui.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //default file and application location strings
        private static readonly string _initialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string _defaultFileName = "default.json";
        private const string _fileFilter = "JSON files (*.json)|*.json";
        private string _defaultFilePath = GetDefaultFileIfItExists();

        public MainViewModel()
        {
            //populate configList
            if (ConfigHistoryHelper.DoesConfigHistoryExist())
            {
                ConfigList = (ObservableCollection<string>)ConfigHistoryHelper.GetHistoryFromUserStorage();
            }

            AddToConfigList(_defaultFilePath);
            SelectedConfig = _defaultFilePath;

            //Status
            StatusReady("Ready");
        }

        #region ConfigFile

        private ConfigFileModel _selectedConfig = new ConfigFileModel();

        public string SelectedConfig
        {
            get => _selectedConfig.FilePath;
            set
            {
                if (System.IO.File.Exists(value))
                {
                    AddToConfigList(value);
                    _selectedConfig.FilePath = value;
                    NotifyPropertyChanged("SelectedConfig");
                } else
                {
                    RemoveFromConfigList(value);
                    _selectedConfig.FilePath = _defaultFilePath;
                    StatusRed("The selected configuration does not exist on disk.");
                }
            }
        }

        #endregion ConfigFile

        #region ConfigList

        private void AddToConfigList(string value)
        {
            if (!ConfigList.Contains(value))
            {
                ConfigList.Add(value);
                SaveConfigList();
            }
        }

        private void RemoveEmptiesFromConfigList() => ConfigList.Remove("");
        private void RemoveFromConfigList(string value) => ConfigList.Remove(value);
        private void SaveConfigList() => ConfigHistoryHelper.WriteHistoryToUserStorage(_configList);

        private ObservableCollection<string> _configList = new ObservableCollection<string>();

        public ObservableCollection<string> ConfigList
        {
            get => _configList;
            set
            {
                _configList = value;
                RemoveEmptiesFromConfigList();
                SaveConfigList();
            }
        }

        #endregion ConfigList

        #region Commands

        public async void ApplyConfigUIAsyncWrapper()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            StatusWorking("Accessing Inventor, please standby...");

            await Task.Run(() => ApplyConfig());

            Mouse.OverrideCursor = null;
        }

        private void ApplyConfig()
        {
            if (_selectedConfig.ApplyConfig())
            {
                StatusGreen("The Configuration was applied successfully.");
            }
            else
            {
                StatusRed("The Configuration failed to be applied.");
            }
        }

        public void SaveConfigToNewFileUIWrapper()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            StatusWorking("Accessing Inventor, please standby...");
            var temp = SaveConfigToNewFile();

            if (string.IsNullOrEmpty(temp))
            {
                StatusRed("The Configuration save failed.");
            }
            else
            {
                StatusGreen("The Configuration was saved to new file.");
                SelectedConfig = temp;
            }
                Mouse.OverrideCursor = null;
        }

        public string SaveConfigToNewFile()
        {
            var newFilePath = ShowSaveFileDialog();

            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            StatusWorking("Accessing Inventor, please standby...");

            if (string.IsNullOrEmpty(newFilePath))
            {

            } else
            {
                try
                {
                    ConfigWriter configWriter = new ConfigWriter(newFilePath);
                }
                catch (Exception ex)
                {
                    throw new SystemException(ex.Message, ex);
                }
            }

            Mouse.OverrideCursor = null;
            return System.IO.File.Exists(newFilePath) ? newFilePath : null;
        }

        public string ShowSaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = _initialDirectory,
                Filter = _fileFilter,
                DefaultExt = "json",
                Title = "Save the Inventor Configuration to a new file.",
                OverwritePrompt = true
            };

            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;

            return null;
        }

        public void BrowseForConfigFileUIWrapper()
        {
            StatusWorking("Browse for File...");
            string temp = BrowseForConfigFileDialog();

            if (!string.IsNullOrEmpty(temp))
            {
                this.SelectedConfig = temp;
                StatusReady("Ready...");
            }
        }

        public string BrowseForConfigFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = _initialDirectory,
                Filter = _fileFilter,
                CheckFileExists = true
            };

            return (openFileDialog.ShowDialog() == true) ? openFileDialog.FileName : null;
        }

        public static string GetDefaultFileIfItExists()
        {
            var defaultFullFileName = System.IO.Path.Combine(_initialDirectory, _defaultFileName);

            return System.IO.File.Exists(defaultFullFileName) ? defaultFullFileName : "";
        }

        #endregion Commands

        #region Status Controls

        private string _statusMessage;

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; NotifyPropertyChanged("StatusMessage"); }
        }

        private SolidColorBrush _statusColor = System.Windows.Media.Brushes.Transparent;

        public SolidColorBrush StatusColor
        {
            get => _statusColor;
            set { _statusColor = value; NotifyPropertyChanged("StatusColor"); }
        }

        public void StatusGreen(string temp = null)
        {
            StatusColor = Brushes.Lime;
            UpdateMessage(temp);
        }

        public void StatusRed(string temp = null)
        {
            StatusColor = Brushes.Red;
            UpdateMessage(temp);
        }

        public void StatusReady(string temp = null)
        {
            StatusColor = Brushes.DarkSlateGray;
            UpdateMessage(temp);
        }

        public void StatusWorking(string temp = null)
        {
            StatusColor = Brushes.Yellow;
            UpdateMessage(temp);
        }

        private void StatusClear(string temp = null)
        {
            StatusColor = Brushes.Transparent;
            UpdateMessage(temp);
        }

        private void UpdateMessage(string temp)
        {
            StatusMessage = temp ?? StatusMessage;
        }

        #endregion Status Controls
    }
}