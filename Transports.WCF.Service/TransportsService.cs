using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Transports.WCF.Service
{
    public class TransportsService : ITransportsService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void CreateDriver(string driver)
        {
            Console.WriteLine("Added: {0} ", driver);
        }
    }
}
