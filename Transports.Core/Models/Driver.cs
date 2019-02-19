using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.Drivers"), Serializable, DataContract]
    public class Driver : IEntity
    {
        private string _rang;
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
        public string Rang
        {
            get => _rang;
            set
            {
                if (Age >= 18)
                    try
                    {
                        _rang = Enum.GetName(typeof(RangEnum), int.Parse(value));
                    }
                    catch (FormatException)
                    {
                        _rang = value;
                    }
                else
                    _rang = "A";
            }
        }

        [Association(Storage = "_DriverShifts", OtherKey = "DriverID")]
        public EntitySet<DriverShift> DriverShifts
        {
            get => _DriverShifts;
            set => _DriverShifts.Assign(value);
        }


        public Driver(string name, int age, int rang)
        {
            DriverId = Guid.NewGuid();
            DriverShifts = new EntitySet<DriverShift>();
            Name = name;
            Age = age;
            Rang = rang.ToString();
            InMemoryContext.Drivers.Add(this);
        }

        public Driver() : this("Ivan", 16, 1) { }

        public bool IsAdult() => Age >= 18;

        public List<Shift> GetDriverShiftsList()
        {
            return InMemoryContext.DriverShifts
                .Where(x => x.Driver == this)
                .Select(x => x.Shift)
                .ToList();
        }

        public bool PassExam(bool isPrepared)
        {
            var random = new Random();
            var chance = isPrepared ? 7 : 3;
            if (Rang != "D")
            {
                if (random.Next(0, 10) <= chance) return true;
            }

            return false;
        }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Drivers.xml", FileMode.Create))
            {
                xml.Serialize(fs, InMemoryContext.Drivers);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Drivers.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    InMemoryContext.Drivers = (List<Driver>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format($"{Name} {Age} years.Rang {Rang}");
        }
    }
}