using System;
using System.ComponentModel.DataAnnotations.Schema;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class Driver : IDriver, IEntity
    {
        public Guid DriverId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public RangEnum Rang { get; set; }
        [NotMapped]
        public int TotalShifts { get; set; }

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