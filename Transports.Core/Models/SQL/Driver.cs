using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Drivers")]
    public class Driver : IDriver, IEntity
    {
        private EntitySet<DriverShift> _DriverShifts;
        private Guid _DriverID;

        [Column(IsPrimaryKey = true, Storage = "_DriverID")]
        public Guid DriverId
        {
            get => _DriverID;
            set => _DriverID = value;
        }

        [Column]
        public string Name { get; set; }

        [Column]
        public int Age { get; set; }

        [Column]
        public RangEnum Rang { get; set; }

        [Association(Storage = "_DriverShifts", OtherKey = "DriverID")]
        public EntitySet<DriverShift> DriverShifts
        {
            get => _DriverShifts;
            set => _DriverShifts.Assign(value);
        }

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
        public Driver(bool @default = false) : this("Ivan", 16, RangEnum.A) { }
        public override string ToString() => string.Format($"{Name} {Age} years.Rang {Rang}");
        public object Clone() => MemberwiseClone();
    }
}