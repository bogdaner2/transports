using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class DriverShift : IDriverShift, IEntity
    {
        [Key]
        public Guid DriverShiftId { get; set; }

        public Guid ShiftId { get; set; }

        public Guid DriverId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}