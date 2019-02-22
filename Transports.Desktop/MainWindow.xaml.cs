using System;
using System.Collections.Generic;
using System.Windows;
using Transports.Core;
using Transports.Core.Contexts;
using Transports.Core.Models;
using Transports.Desktop.Views;

namespace Transports.Desktop
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly TransportsContext _db;
        public MainWindow()
        {
            InitializeComponent();

            var repo = new ContextRepository<Shift>();
            var shift = new Shift
            {
                End = DateTime.Now,
                Start = DateTime.Now
            };

            repo.Create(
            shift.AddRoutes(new List<Route>{
                new Route(10, false),
                new Route(10, false),
                new Route(10, false)
            }));
            var items = repo.GetAll();
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