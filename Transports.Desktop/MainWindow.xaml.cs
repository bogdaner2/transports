using System.Windows;
using Transports.Core.Contexts;
using Transports.Core.Services;
using Transports.Desktop.ModalWindows;
using Transports.Desktop.Views;

namespace Transports.Desktop
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Main.Content = new DriversView();

            if (StateService.StoreType == StoreType.InMemory)
            {
                Closing += (sender, args) => new SaveDataWindow().Show();
                InMemoryContext.Instance.LoadData();
            }
        }

        private void Button_On_Drivers_Page(object sender, RoutedEventArgs e)
        {
            Main.Content = new DriversView();
        }

        private void Button_On_Shifts_Page(object sender, RoutedEventArgs e)
        {
            Main.Content = new ShiftsView();
        }

        private void Button_On_DriverShifts_Page(object sender, RoutedEventArgs e)
        {
            Main.Content = new DriverShiftsView();
        }

        private void Button_On_Routes_Page(object sender, RoutedEventArgs e)
        {
            Main.Content = new RoutesView();
        }

        private void Button_On_Transports_Page(object sender, RoutedEventArgs e)
        {
            Main.Content = new TransportsView();
        }

        private void Button_On_Passports_Page(object sender, RoutedEventArgs e)
        {
            Main.Content = new TechPassportsView();
        }
    }
}