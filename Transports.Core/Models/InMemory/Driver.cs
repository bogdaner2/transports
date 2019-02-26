using System;
using System.Linq;
using System.Runtime.Serialization;
using Transports.Core.Contexts;
using Transports.Core.Interfaces;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class Driver : IDriver
    {
        [DataMember]
        public Guid DriverId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }

        [DataMember]
        public RangEnum Rang { get; set; }

        public int TotalShifts
        {
            get => InMemoryContext.Instance.DriverShifts.Count(x => x.DriverId == DriverId);
        }

        public Driver(string name, int age, RangEnum rang) : this()
        {
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

        //public List<InMemory.Shift> GetDriverShiftsList()
        //{
        //    return InMemoryContext.Instance.DriverShifts
        //        .Where(x => x.Driver == this)
        //        .Select(x => x.Shift)
        //        .ToList();
        //}

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