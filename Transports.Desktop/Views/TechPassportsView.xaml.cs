using System.Windows;
using System.Windows.Controls;
using Transports.Desktop.ViewModels;

namespace Transports.Desktop.Views
{
    public partial class TechPassportsView : Page
    {
        private TechPassportsViewModel TechPassportsViewModel { get; set; }
        public TechPassportsView()
        {
            InitializeComponent();
            TechPassportsViewModel = new TechPassportsViewModel();
            DataContext = TechPassportsViewModel;
        }

        private void Add_Passport_OnClick(object sender, RoutedEventArgs e)
        {
            TechPassportsViewModel.AddTechPassport();
        }

        private void Update_Passport_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Remove_Passport_OnClick(object sender, RoutedEventArgs e)
        {
            TechPassportsViewModel.RemoveTechPassport();
        }
    }
}
