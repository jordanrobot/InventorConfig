using System.Windows;
using System.Diagnostics;
using InventorConfig.Gui.ViewModel;
using System.ComponentModel;

namespace InventorConfig.Gui.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutViewModel About = new AboutViewModel();

        public AboutWindow()
        {
            InitializeComponent();
            this.DataContext = About;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            // see https://docs.microsoft.com/dotnet/api/system.diagnostics.processstartinfo.useshellexecute#property-value
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void AboutCloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

