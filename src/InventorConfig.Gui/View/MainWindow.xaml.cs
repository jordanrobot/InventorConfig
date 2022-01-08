using InventorConfig.Gui.ViewModel;
using System;
using System.Windows;

namespace InventorConfig.Gui.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel mainViewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = mainViewModel;
        }

        private void ApplyConfigButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.ApplyConfigUIAsyncWrapper();
        }

        private void SaveConfigButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.SaveConfigToNewFileUIWrapper();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.BrowseForConfigFileUIWrapper();
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }
    }
}