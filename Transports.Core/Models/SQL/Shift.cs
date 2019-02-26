using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Shifts")]
    public class Shift : IShift, IEntity
    {
        private Guid _ShiftID;
        private EntitySet<Route> _Routes;
        private EntitySet<DriverShift> _DriverShift;

        [Column]
        public DateTime Start { get; set; }

        [Column]
        public DateTime End { get; set; }

        public Guid ShiftId { get; set; }
        public int TotalRoutes { get; set; }
        public int TotalDrivers { get; set; }

        [Column(IsPrimaryKey = true, Storage = "_ShiftID")]
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

        public Shift AddRoutes(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                route.ShiftID = ShiftID;
                Routes.Add(route);
            }
            return this;
        }

        public object Clone() => MemberwiseClone();
    }
}
