using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Transports.Core.Models;

namespace Transports.Web.WCF
{
    public interface ITransportService
    {
        [OperationContract]
        [WebGet(
            UriTemplate = "Drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Driver> GetDrivers();
    }
}
