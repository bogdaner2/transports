using System;
using System.Data.Linq;
using System.IO;
using Transports.Core.Interfaces;
using Transports.Core.Models;

namespace Transports.Core.Contexts
{
    public class TransportsContext : DataContext, IContext
    {
        private static readonly Lazy<TransportsContext> Lazy = new Lazy<TransportsContext>(() => new TransportsContext());
        public static TransportsContext Instance => Lazy.Value;
        private TransportsContext(string connection) : base(connection)
        {
            if (!DatabaseExists())
            {
                CreateDatabase();
            }
        }

        private TransportsContext() : this(
            $@"Server=.\;Database=Transports;Trusted_Connection=True;")
        {

        }

        private void DeleteDB() => DeleteDatabase();

        public Table<Driver> Drivers => GetTable<Driver>();
        public Table<Shift> Shifts => GetTable<Shift>();
        public Table<Route> Routes => GetTable<Route>();
        public Table<DriverShift> DriverShifts => GetTable<DriverShift>();
        public Table<TechPassport> TechPassports => GetTable<TechPassport>();
        public Table<Transport> Transports => GetTable<Transport>();

    }
}
