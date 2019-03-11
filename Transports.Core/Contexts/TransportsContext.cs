using System;
using System.Data.Entity;
using Transports.Core.Interfaces;
using Transports.Core.Models.SQL;

namespace Transports.Core.Contexts
{
    public class TransportsContext : DbContext, IContext
    {
        private static readonly Lazy<TransportsContext> Lazy = new Lazy<TransportsContext>(() => new TransportsContext());
        public static TransportsContext Instance => Lazy.Value;

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<DriverShift> DriverShifts { get; set; }
        public DbSet<TechPassport> TechPassports { get; set; }
        public DbSet<Transport> Transports { get; set; }


        private TransportsContext(string connection) : base(connection)
        {
        }

        private TransportsContext() : this(
            $@"Server=.\;Database=Transports;Trusted_Connection=True;")
        {

        }
    }
}
