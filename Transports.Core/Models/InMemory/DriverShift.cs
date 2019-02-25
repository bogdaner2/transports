using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [DataContract, Serializable]
    public class DriverShift : IDriverShift
    {
        [DataMember]
        public Guid DriverShiftId { get; set; }

        [DataMember]
        public Guid ShiftId { get; set; }

        [DataMember]
        public Guid DriverId { get; set; }

        public Driver Driver { get; set; }
        public Shift Shift { get; set; }

        public DriverShift() { }

        public object Clone() => MemberwiseClone();
    }
}