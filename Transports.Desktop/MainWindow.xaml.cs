using System;
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
            //_db = new TransportsContext();
            //_db.Drivers.InsertOnSubmit(new Driver());
            //_db.SubmitChanges();
            var repo = new ContextRepository<Driver>();
            repo.Create(new Driver());
        }

    }
}