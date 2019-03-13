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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ShiftId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ICollection<DriverShift> DriverShifts { get; set; }

        [NotMapped]
        public int TotalRoutes { get; }
        [NotMapped]
        public int TotalDrivers { get; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return ShiftId.ToString();
        }
    }
}