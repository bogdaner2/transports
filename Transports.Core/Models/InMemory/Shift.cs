using System;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class Shift : IShift
    {
        [DataMember]
        public Guid ShiftId { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }

        public Shift()
        {
            ShiftId = Guid.NewGuid();
        }

        public object Clone() => MemberwiseClone();
    }
}
