using System;
using System.Data.Linq;
using System.IO;
using Transports.Core.Models;

namespace Transports.Core
{
    public class TransportsContext : DataContext
    {
        private static readonly Lazy<TransportsContext> Lazy = new Lazy<TransportsContext>(() => new TransportsContext());
        public static TransportsContext Instance => Lazy.Value;
        private TransportsContext(string connection) : base(connection)
        {
            //if (DatabaseExists())
            //{
            //    DeleteDatabase();
            //}
            //CreateDatabase();
        }

        private TransportsContext() : this(
            $@"Server=.\;AttachDbFilename={Directory.GetCurrentDirectory()}\Transports.mdf;Database=Transports;Trusted_Connection=True;")
        {

        }

        public Table<Driver> Drivers => GetTable<Driver>();
        public Table<Shift> Shifts => GetTable<Shift>();
        public Table<Route> Routes => GetTable<Route>();
        public Table<Transport> Transports => GetTable<Transport>();
        public Table<TechPassport> TechPassports => GetTable<TechPassport>();

    }
}
