using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class Driver : IDriver, IEntity
    {
        [Key]
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public RangEnum Rang { get; set; }
        public ICollection<DriverShift> DriverShifts { get; set; }

        [NotMapped]
        public int TotalShifts { get; set; }

        public Driver()
        {
            DriverId = Guid.NewGuid();
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format($"{Name} {Age} years.Rang {Rang}");
        }
    }
}