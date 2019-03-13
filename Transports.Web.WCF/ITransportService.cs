using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Transports.Core.Models;
using Transports.Core.Models.SQL;

namespace Transports.Web.WCF
{
    [ServiceContract]
    public interface ITransportService
    {
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Driver> GetDrivers();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Driver Create(Driver driver);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool  Update(Driver driver);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool Delete(Guid Id);
    }
}
