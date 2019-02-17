using System.Data.Linq;
using System.IO;
using Transports.Core.Models;

namespace Transports.Core
{
    public class TransportsContext : DataContext
    {
        public TransportsContext(string connection) : base(connection)
        {
            if (!DatabaseExists())
                CreateDatabase();
        }

        public TransportsContext() : this(
            $@"Server=.\;AttachDbFilename={Directory.GetCurrentDirectory()}\Transports.mdf;Database=Transports;Trusted_Connection=True;")
        {

        }

        public Table<Driver> Drivers => GetTable<Driver>();
        public Table<Transport> Transports => GetTable<Transport>();

    }
}
