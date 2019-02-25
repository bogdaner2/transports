using System;
using System.Collections.Generic;
using System.Linq;
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
using Transports.Desktop.ViewModels;

namespace Transports.Desktop.Views
{
    /// <summary>
    /// Interaction logic for ShiftsView.xaml
    /// </summary>
    public partial class ShiftsView : Page
    {
        private ShiftsViewModel ShiftsViewModel { get; set; }
        public ShiftsView()
        {
            InitializeComponent();
            ShiftsViewModel = new ShiftsViewModel();
            DataContext = ShiftsViewModel;
        }

        private void Add_Shift_OnClick(object sender, RoutedEventArgs e)
        {
            ShiftsViewModel.AddShift();
        }

        private void Update_Shift_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Remove_Shift_OnClick(object sender, RoutedEventArgs e)
        {
            ShiftsViewModel.RemoveShift();
        }
    }
}
