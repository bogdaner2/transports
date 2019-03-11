using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class Shift : IShift, IEntity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        [Key]
        public Guid ShiftId { get; set; }

        [NotMapped]
        public int TotalRoutes { get; }
        [NotMapped]
        public int TotalDrivers { get; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}