using System.Data.Linq;
using Transports.Core.Models;

namespace Transports.Core
{
    public class Repository<T> where  T: class
    {
        private Table<T> _table;
        public Repository()
        {
            TransportsContext db = new TransportsContext();
           // _table = new Table<Driver>();
        }
    }
}
