using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Drivers")]
    public class Driver : IDriver, IEntity
    {
        private readonly EntitySet<DriverShift> _DriverShifts;

        public Driver(string name, int age, RangEnum rang)
        {
            DriverId = Guid.NewGuid();
            _DriverShifts = new EntitySet<DriverShift>();
            Name = name;
            Age = age;
            Rang = rang;
        }

        public Driver()
        {
            DriverId = Guid.NewGuid();
        }

        public Driver(bool @default = false) : this("Ivan", 16, RangEnum.A)
        {
        }

        [Association(Storage = "_DriverShifts", OtherKey = "DriverID")]
        public EntitySet<DriverShift> DriverShifts
        {
            get => _DriverShifts;
            set => _DriverShifts.Assign(value);
        }

        [Column(IsPrimaryKey = true, Storage = "_DriverID")]
        public Guid DriverId { get; set; }

        [Column] public string Name { get; set; }

        [Column] public int Age { get; set; }

        [Column] public RangEnum Rang { get; set; }

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