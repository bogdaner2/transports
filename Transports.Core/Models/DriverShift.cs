using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.DriverShifts"), DataContract, Serializable]
    public class DriverShift : IEntity
    {
        
        private Guid _DriverID;
        private Guid _ShiftID;
        private Guid _DriverShiftID;
        private EntityRef<Shift> _Shift;
        private EntityRef<Driver> _Driver;

        [Column(IsPrimaryKey = true, Storage = "_DriverShiftID")]
        public Guid DriverShiftID
        {
            get => _DriverShiftID;
            set => _DriverShiftID = value;
        }

        [Column(Storage = "_ShiftID")]
        public Guid ShiftID
        {
            get => _ShiftID;
            set => _ShiftID = value;
        }

        [Column(Storage = "_DriverID")]
        public Guid DriverID
        {
            get => _DriverID;
            set => _DriverID = value;
        }

        [Association(Storage = "_Driver", ThisKey = "DriverID")]
        public Driver Driver
        {
            set => _Driver.Entity = value;
            get => _Driver.Entity;
        }

        [Association(Storage = "_Shift", ThisKey = "ShiftID")]
        public Shift Shift
        {
            set => _Shift.Entity = value;
            get => _Shift.Entity;
        }

        public DriverShift(Shift shift, Driver driver)
        {
            DriverShiftID = Guid.NewGuid();
            _Shift = new EntityRef<Shift>();
            _Driver = new EntityRef<Driver>();
            Driver = driver;
            Shift = shift;
            InMemoryContext.DriverShifts.Add(this);
        }

        public DriverShift()
        {
            DriverShiftID = Guid.NewGuid();
            _Shift = new EntityRef<Shift>();
            _Driver = new EntityRef<Driver>();
            InMemoryContext.DriverShifts.Add(this);
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