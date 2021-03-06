﻿using System;
using System.Linq;
using System.Runtime.Serialization;
using Transports.Core.Contexts;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class Shift : IShift
    {
        [DataMember]
        public Guid ShiftId { get; set; }
        public int TotalRoutes
        {
            get => InMemoryContext.Instance.Routes.Count(x => x.ShiftId == ShiftId);
        }

        public int TotalDrivers
        {
            get => InMemoryContext.Instance.DriverShifts.Count(x => x.ShiftId == ShiftId);
        }

        [DataMember(EmitDefaultValue = false)]
        public DateTime Start { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime End { get; set; }

        public Shift()
        {
            ShiftId = Guid.NewGuid();
        }

        public object Clone() => MemberwiseClone();
    }
}
