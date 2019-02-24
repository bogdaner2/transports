using System.Windows;
using System.Windows.Controls;
using Transports.Desktop.ViewModels;

namespace Transports.Desktop.Views
{
    public partial class TransportsView : Page
    {
        private TransportsViewModel TransportsViewModel { get; set; }
        public TransportsView()
        {
            InitializeComponent();
            TransportsViewModel = new TransportsViewModel();
            DataContext = TransportsViewModel;
        }

        private void Add_Transport_OnClick(object sender, RoutedEventArgs e)
        {
            TransportsViewModel.AddTransport();
        }

        private void Update_Transport_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Remove_Transport_OnClick(object sender, RoutedEventArgs e)
        {
            TransportsViewModel.RemoveTransport();
        }
    }
}
