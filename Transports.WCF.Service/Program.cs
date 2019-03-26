using System;
using System.Configuration;
using System.Messaging;
using System.ServiceModel;

namespace Transports.WCF.Service
{
    class Program
    {
        public static void Main()
        {
            if (!MessageQueue.Exists(ConfigurationManager.AppSettings["queueName"]))
                MessageQueue.Create(ConfigurationManager.AppSettings["queueName"], true);

            using (ServiceHost serviceHost = new ServiceHost(typeof(TransportsService)))
            {
                serviceHost.Open();

                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press any key to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                serviceHost.Close();
            }
        }
    }
}
