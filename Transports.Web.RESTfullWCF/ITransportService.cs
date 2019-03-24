using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Transports.Core.Models.SQL;

namespace Transports.Web.RESTfullWCF
{
    [ServiceContract]
    public interface ITransportService
    {
        #region Drivers

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "api/drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Driver> GetDrivers();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "api/drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Driver CreateDriver(Driver driver);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "api/drivers",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool UpdateDriver(Driver driver);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "api/drivers/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool DeleteDriver(string id);

        #endregion

        #region Shifts

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "api/shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Shift> GetShifts();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "api/shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Shift CreateShift(Shift shift);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "api/shifts",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool UpdateShift(Shift shift);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "api/shifts/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool DeleteShift(string id);

        #endregion

        #region Routes

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            UriTemplate = "api/routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        List<Route> GetRoutes();

        [OperationContract]
        [WebInvoke(
            Method = "POST",
            UriTemplate = "api/routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        Route CreateRoute(Route route);

        [OperationContract]
        [WebInvoke(
            Method = "PUT",
            UriTemplate = "api/routes",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool UpdateRoute(Route route);

        [OperationContract]
        [WebInvoke(
            Method = "DELETE",
            UriTemplate = "api/routes/{id}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        bool DeleteRoute(string id);

        #endregion
    }
}