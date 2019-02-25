using System.Windows;
using System.Windows.Controls;
using Transports.Desktop.ViewModels;

namespace Transports.Desktop.Views
{
    public partial class DriverShiftsView : Page
    {
        private DriverShiftsViewModel DriverShiftViewModel { get; set; }
        public DriverShiftsView()
        {
            InitializeComponent();
            DriverShiftViewModel = new DriverShiftsViewModel();
            DataContext = DriverShiftViewModel;
        }

        private void Add_DriverShift_OnClick(object sender, RoutedEventArgs e)
        {
            DriverShiftViewModel.AddDriverShift();
        }

        private void Update_DriverShift_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Remove_DriverShift_OnClick(object sender, RoutedEventArgs e)
        {
            DriverShiftViewModel.RemoveDriverShift();
        }
    }
}
