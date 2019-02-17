using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Transports.Core;
using Transports.Core.Models;

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

            repo.Create(shift.AddRoutes(new List<Route>{
                new Route(10, false),
                new Route(10, false),
                new Route(10, false)
            }));
            var items = repo.GetAll();
        }

    }
}