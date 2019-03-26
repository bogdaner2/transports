using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Transports.WCF.Service
{
    [ServiceContract]
    public interface ITransportsService
    {
        #region drivers
        [OperationContract(IsOneWay = true)]
        void CreateDriver(string driver);

        [OperationContract(IsOneWay = true)]
        void UpdateDriver(string updatedDriver);

        [OperationContract(IsOneWay = true)]
        void RemoveDriver(string driverId);
        #endregion

        #region shifts
        [OperationContract(IsOneWay = true)]
        void CreateShift(string shift);

        [OperationContract(IsOneWay = true)]
        void UpdateShift(string updatedShift);

        [OperationContract(IsOneWay = true)]
        void RemoveShift(string shiftId);
        #endregion

        #region routes
        [OperationContract(IsOneWay = true)]
        void CreateRoute(string route);

        [OperationContract(IsOneWay = true)]
        void UpdateRoute(string updatedRoute);

        [OperationContract(IsOneWay = true)]
        void RemoveRoute(string routeId);
        #endregion

    }
}
