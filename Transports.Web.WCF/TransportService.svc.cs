using System.Collections.Generic;
using System.Linq;
using Transports.Core.Models;
using Transports.Core.Repositories;

namespace Transports.Web.WCF
{
    public class TransportService : ITransportService
    {
        private ContextRepository<Driver> _driverRepo = new ContextRepository<Driver>();
        public List<Driver> GetDrivers()
        {
            return _driverRepo.GetAll().ToList();
        }
    }
}
