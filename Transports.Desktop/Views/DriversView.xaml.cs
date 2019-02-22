using System.Windows;
using System.Windows.Controls;
using Transports.Desktop.ViewModels;

namespace Transports.Desktop.Views
{
    public partial class DriversView : Page
    {
        private DriversViewModel DriverViewModel { get; set; }
        public DriversView()
        {
            InitializeComponent();
            DriverViewModel = new DriversViewModel();
            DataContext = DriverViewModel;
        }

        private void Add_Driver_OnClick(object sender, RoutedEventArgs e)
        {
            DriverViewModel.AddDriver();
        }
    }
}
