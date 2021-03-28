using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventorConfig;
using Microsoft.Win32;

namespace InventorConfig.Gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private String FileFilter = "JSON files (*.json)|*.json";

        public MainWindow()
        {
            InitializeComponent();

            FileNameTextBox.Text = GetDefaultFileIfItExists();
        }

        private string GetDefaultFileIfItExists()
        {
            var defaultFileName = "default.json";
            var defaultFullFileName = System.IO.Path.Combine(InitialDirectory, defaultFileName);
            if (System.IO.File.Exists(defaultFullFileName))
                return defaultFullFileName;

            return "";
        }

        private void ApplyConfigButton_Click(object sender, RoutedEventArgs e)
        {
            StatusYellow();
            ApplyConfig();
        }

        private void ApplyConfig()
        {
            var fileName = FileNameTextBox.Text;
            if (String.IsNullOrEmpty(fileName))
            {
                MessageBox.Show("Please pick a configuration file to load");
            return;
            }

            StatusIcon.Fill = System.Windows.Media.Brushes.Yellow;
            ConfigFile configFile = new ConfigFile();
            configFile.SetApplyConfigFilePath(fileName);

            if (configFile.Path is null)
            {
                StatusRed();
                StatusTextBox.Content = "The selected file could not be found.";
                return;
            }

            //modify the Inventor config
            try
            {
                ConfigEngine configEngine = new ConfigEngine();
                configEngine.LoadConfigFromFile(configFile.Path);
                StatusIcon.Fill = System.Windows.Media.Brushes.Green;
                StatusGreen();
                StatusTextBox.Content = "Configuration Applied.";
                return;
            }
            catch (Exception ex)
            {
                StatusRed();
                StatusTextBox.Content = "Configuration failed to load.";
                throw new SystemException(ex.Message, ex);
            }
        }

        private void SaveConfigButton_Click(object sender, RoutedEventArgs e)
        {
            StatusYellow();
            var test = SaveConfigToNewFile();
            if (test)
            {
                StatusGreen();
                StatusTextBox.Content = "Configuration Created.";
            }
        }

        private bool SaveConfigToNewFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = InitialDirectory;
            saveFileDialog.Filter = FileFilter;
            saveFileDialog.DefaultExt = "json";
            saveFileDialog.Title = "Save the Inventor Configuration to a new file.";
            saveFileDialog.OverwritePrompt = true;

            if (saveFileDialog.ShowDialog() == true)
                FileNameTextBox.Text = saveFileDialog.FileName;

            try
            {
                ConfigEngine configEngine = new ConfigEngine();
                configEngine.WriteConfigToFile(saveFileDialog.FileName);
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message, ex);
            }

            if (System.IO.File.Exists(saveFileDialog.FileName))
                return true;

            return false;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = InitialDirectory;
            openFileDialog.Filter = FileFilter;
            openFileDialog.CheckFileExists = true;

            if (openFileDialog.ShowDialog() == true)
                FileNameTextBox.Text = openFileDialog.FileName;

            StatusClear();
            StatusTextBox.Content = "";
        }

        private void StatusRed()
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Red;
        }

        private void StatusGreen()
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Green;
        }

        private void StatusYellow()
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Yellow;
        }

        private void StatusClear()
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Transparent;
        }
    }
}
