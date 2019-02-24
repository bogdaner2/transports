using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Transports.Core.Contexts;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.Drivers"), Serializable, DataContract]
    public class Driver : ICloneable, IEntity
    {
        private EntitySet<DriverShift> _DriverShifts;
        private Guid _DriverID;

        [Column(IsPrimaryKey = true, Storage = "_DriverID"), DataMember]
        public Guid DriverId
        {
            get => _DriverID;
            set => _DriverID = value;
        }

        [Column, DataMember]
        public string Name { get; set; }

        [Column, DataMember]
        public int Age { get; set; }

        [Column, DataMember]
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

        public bool IsAdult() => Age >= 18;

        public List<Shift> GetDriverShiftsList()
        {
            return InMemoryContext.Instance.DriverShifts
                .Where(x => x.Driver == this)
                .Select(x => x.Shift)
                .ToList();
        }

        public bool PassExam(bool isPrepared = false)
        {
            var random = new Random();
            var chance = isPrepared ? 7 : 3;
            if (Rang != RangEnum.D)
            {
                if (random.Next(0, 10) <= chance) return true;
            }

            return false;
        }

        public override string ToString() => string.Format($"{Name} {Age} years.Rang {Rang}");
        public object Clone() => MemberwiseClone();
    }
}