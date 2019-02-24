using System;
using System.Runtime.Serialization;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class Shift
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
    }
}
