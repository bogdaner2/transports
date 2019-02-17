﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.Drivers")]
    public class Driver : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        [DataMember]
        public string Name { get; set; }

        [Column]
        [DataMember]
        public int Age { get; set; }

        [Column]
        [DataMember]
        public string Rang
        {
            get => rang;
            set
            {
                if (Age >= 18)
                    try
                    {
                        rang = Enum.GetName(typeof(RangEnum), int.Parse(value));
                    }
                    catch (FormatException)
                    {
                        rang = value;
                    }
                else
                    rang = "A";
            }
        }

        private string rang;

        public Driver(string name, int age, int rang)
        {
            Id = Guid.NewGuid();
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