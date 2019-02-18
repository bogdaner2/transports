using System;
using System.Collections.Generic;
using System.Data.Linq;
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
        
        private Guid _DriverID;
        private Guid _ShiftID;
        private Guid _DriverShiftID;
        private EntityRef<Shift> _Shift;
        private EntityRef<Driver> _Driver;


        public DriverShift(Shift shift, Driver driver)
        {
            Driver = driver;
            Shift = shift;
            InMemoryContext.DriverShifts.Add(this);
        }

        public DriverShift()
        {
            _Shift = new EntityRef<Shift>();
            _Driver = new EntityRef<Driver>();
        }

        public Shift Shift { get; set; }
        public Driver Driver { get; set; }

        //public Shift ShiftLocal
        //{
        //    get { return InMemoryContext.Shifts.Find(x => x.ShiftID == idShift); }
        //    set => idShift = value.ShiftID;
        //}

        //public Driver DriverLocal
        //{
        //    get { return InMemoryContext.Drivers.Find(x => x.Id == idDriver); }
        //    set => idDriver = value.Id;
        //}

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