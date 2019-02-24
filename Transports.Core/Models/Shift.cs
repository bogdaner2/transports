using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;
using Transports.Core.Contexts;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.Shifts"), Serializable, DataContract]
    public class Shift : IEntity
    {
        private Guid _ShiftID;
        private EntitySet<Route> _Routes;
        private EntitySet<DriverShift> _DriverShift;

        [Column, DataMember]
        public DateTime Start { get; set; }

        [Column, DataMember]
        public DateTime End { get; set; }

        [Column(IsPrimaryKey = true, Storage = "_ShiftID"), DataMember]
        public Guid ShiftID
        {
            get => _ShiftID;
            set => _ShiftID = value;
        }

        [Association(Storage = "_Routes", OtherKey = "ShiftID")]
        public EntitySet<Route> Routes
        {
            get => _Routes; 
            set => _Routes.Assign(value); 
        }

        [Association(Storage = "_DriverShift", OtherKey = "ShiftID")]
        public EntitySet<DriverShift> DriverShift
        {
            get => _DriverShift;
            set => _DriverShift.Assign(value);
        }

        public Shift()
        {
            _Routes = new EntitySet<Route>();
            _DriverShift = new EntitySet<DriverShift>();
            ShiftID = Guid.NewGuid();
        }

        public List<Driver> GetDriverShiftsList()
        {
            return InMemoryContext.Instance.DriverShifts
                .Where(x => x.Shift == this)
                .Select(x => x.Driver)
                .ToList();
        }

        public Shift AddRoutes(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                route.ShiftID = ShiftID;
                Routes.Add(route);
            }
            return this;
        }

    }
}
