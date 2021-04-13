using System;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;
using InventorConfig.Gui;

namespace InventorConfig.Gui.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private String InitialDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private String FileFilter = "JSON files (*.json)|*.json";
        private ConfigHistoryHandler configHistory = new ConfigHistoryHandler();

        public MainWindow()
        {
            InitializeComponent();

            FileNameTextBox.Text = GetDefaultFileIfItExists();
            configHistory.AddToConfigHistoryFile(GetDefaultFileIfItExists());
            FileNameTextBox.ItemsSource = configHistory.Configs;
        }
        //TODO: is not currently updating the combobox in real time as things are added.

        private string GetDefaultFileIfItExists()
        {
            var defaultFileName = "default.json";
            var defaultFullFileName = System.IO.Path.Combine(InitialDirectory, defaultFileName);
            if (System.IO.File.Exists(defaultFullFileName))
            {
                StatusCyan();
                return defaultFullFileName;
            }

            return "";
        }

        private void ApplyConfigButton_Click(object sender, RoutedEventArgs e)
        {
            StatusCyan();
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

            StatusCyan();

            //modify the Inventor config
            try
            {
                ConfigLoader configLoader = new ConfigLoader(fileName);

                StatusGreen("Configuration Applied.");
            }
            catch (Exception ex)
            {
                StatusRed("Configuration failed to load.");
                throw new SystemException(ex.Message, ex);
            }
        }

        private void SaveConfigButton_Click(object sender, RoutedEventArgs e)
        {
            StatusCyan();
            var test = SaveConfigToNewFile();
            if (test)
            {
                StatusGreen("Configuration Created.");
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
                ConfigWriter configWriter = new ConfigWriter(saveFileDialog.FileName);
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
            {
                FileNameTextBox.Text = openFileDialog.FileName;
            }
            configHistory.AddToConfigHistoryFile(FileNameTextBox.Text);


            StatusCyan("");
        }

        private void StatusRed(string text = null)
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Red;
            if (text != null)
                StatusTextBox.Content = text;
        }

        private void StatusGreen(string text = null)
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Lime;
            if (text != null)
                StatusTextBox.Content = text;
        }

        private void StatusCyan(string text = null)
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.DarkSlateGray;
            if (text != null)
                StatusTextBox.Content = text;
        }

        private void StatusClear(string text = null)
        {
            StatusIcon.Fill = System.Windows.Media.Brushes.Transparent;
            if (text == null)
            {
                StatusTextBox.Content = "";
            }
            {
                StatusTextBox.Content = text;
            }
        }

        private void FileNameTextBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ComboBoxUpdate();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        private void FileNameTextBox_SourceUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
        {
        }

        private void FileNameTextBox_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxUpdate();
        }

        private void ComboBoxUpdate()
        {
            if (System.IO.File.Exists(FileNameTextBox.Text))
            {
                configHistory.AddToConfigHistoryFile(FileNameTextBox.Text);
                StatusCyan("");
            }
            else
            {
                StatusRed("");
            }
        }
    }
}
