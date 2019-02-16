using System.Data.Linq;
using Transports.Core.Models;

namespace Transports.Core
{
    public class TransportsContext : DataContext
    {
        public TransportsContext(string connection) : base(connection)
        {
            
        }

        public Table<Driver> Drivers => this.GetTable<Driver>();
    }
}
