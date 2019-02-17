using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.DriverShifts")]
    public class DriverShift : IEntity
    {
        
        private Guid idDriver;
        private Guid idShift;

        public DriverShift(Shift shift, Driver driver)
        {
            DriverLocal = driver;
            Shift = shift;
            InMemoryContext.DriverShifts.Add(this);
        }

        public DriverShift()
        {
        }

        public Shift Shift { get; set; }
        public Driver Driver { get; set; }

        public Shift ShiftLocal
        {
            get { return InMemoryContext.Shifts.Find(x => x.Id == idShift); }
            set => idShift = value.Id;
        }

        public Driver DriverLocal
        {
            get { return InMemoryContext.Drivers.Find(x => x.Id == idDriver); }
            set => idDriver = value.Id;
        }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("DriverShifts.xml", FileMode.Create))
            {
                xml.Serialize(fs, InMemoryContext.DriverShifts);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("DriverShifts.xml", FileMode.OpenOrCreate))
            {
                InMemoryContext.DriverShifts = (List<DriverShift>) xml.Deserialize(fileStream);
            }
        }

        public override string ToString()
        {
            return string.Format($"{Driver.Name} | {Shift.Start.ToShortTimeString()} + {Shift.End.ToShortTimeString()}");
        }
    }
}