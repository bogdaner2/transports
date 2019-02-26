using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.DriverShifts")]
    public class DriverShift : IDriverShift, IEntity
    {
        private EntityRef<Driver> _Driver;

        private EntityRef<Shift> _Shift;

        public DriverShift(Shift shift, Driver driver)
        {
            DriverShiftID = Guid.NewGuid();
            _Shift = new EntityRef<Shift>();
            _Driver = new EntityRef<Driver>();
            Driver = driver;
            Shift = shift;
        }

        public DriverShift()
        {
            DriverShiftID = Guid.NewGuid();
            _Shift = new EntityRef<Shift>();
            _Driver = new EntityRef<Driver>();
        }

        [Column(IsPrimaryKey = true, Storage = "_DriverShiftID")]
        public Guid DriverShiftID { get; set; }

        [Column(Storage = "_ShiftID")] public Guid ShiftID { get; set; }

        [Column(Storage = "_DriverID")] public Guid DriverID { get; set; }

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

        public Guid DriverId { get; set; }
        public Guid ShiftId { get; set; }
        public Guid DriverShiftId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format(
                $"{Driver.Name} | {Shift.Start.ToShortTimeString()} + {Shift.End.ToShortTimeString()}");
        }
    }
}