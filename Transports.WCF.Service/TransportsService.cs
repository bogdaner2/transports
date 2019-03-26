using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Transports.Core.Models.SQL;
using Transports.Core.Repositories;

namespace Transports.WCF.Service
{
    public class TransportsService : ITransportsService
    {
        private readonly ContextRepository<Driver> _driversRepo = new ContextRepository<Driver>();

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void CreateDriver(string driver)
        {
            try
            {
                Console.WriteLine("Recieved driver: " + driver);
                _driversRepo.Create(JsonConvert.DeserializeObject<Driver>(driver));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
        }
    }
}
