using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Transports.Core.Models.InMemory
{
    [DataContract, Serializable]
    public class DriverShift
    {

        [DataMember]
        public Guid DriverShiftId { get; set; }

        [DataMember]
        public Guid ShiftId { get; set; }

        [DataMember]
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }
        public Shift Shift { get; set; }

        public DriverShift()
        {
            
        }
        //public DriverShift(Models.Shift shift, Models.Driver driver)
        //{
        //    DriverShiftId = Guid.NewGuid();
        //    _Shift = new EntityRef<Models.Shift>();
        //    _Driver = new EntityRef<Models.Driver>();
        //    Driver = driver;
        //    Shift = shift;
        //}

        //public DriverShift()
        //{
        //    DriverShiftID = Guid.NewGuid();
        //    _Shift = new EntityRef<Models.Shift>();
        //    _Driver = new EntityRef<Models.Driver>();
        //}

        //public override string ToString()
        //{
        //    return string.Format($"{Driver.Name} | {Shift.Start.ToShortTimeString()} + {Shift.End.ToShortTimeString()}");
        //}
    }
}