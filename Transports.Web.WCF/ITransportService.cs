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
        // Drivers
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
        Driver CreateDriver(Driver driver);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool  UpdateDriver(Driver driver);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "drivers/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool DeleteDriver(string id);

        //Shifts
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Shift> GetShifts();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Shift CreateShift(Shift shift);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool UpdateShift(Shift shift);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool DeleteShift(Guid Id);

        //Routes
        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Route> GetRoutes();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Route CreateRoute(Route route);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool UpdateRoute(Route route);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool DeleteRoute(Guid Id);
    }
}
