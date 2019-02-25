using System.Windows;
using System.Windows.Controls;
using Transports.Desktop.ViewModels;

namespace Transports.Desktop.Views
{
    public partial class RoutesView : Page
    {
        private RoutesViewModel RoutesViewModel { get; set; }
        public RoutesView()
        {
            InitializeComponent();
            RoutesViewModel = new RoutesViewModel();
            DataContext = RoutesViewModel;
        }

        private void Add_Route_OnClick(object sender, RoutedEventArgs e)
        {
            RoutesViewModel.AddRoute();
        }

        private void Update_Route_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Remove_Route_OnClick(object sender, RoutedEventArgs e)
        {
            RoutesViewModel.RemoveRoute();
        }
    }
}
