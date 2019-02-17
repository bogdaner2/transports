using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.Shifts")]
    public class Shift : IEntity
    {
        [Column]
        public DateTime Start { get; set; }
        [Column]
        public DateTime End { get; set; }

        private Guid _ShiftID;
        [Column(IsPrimaryKey = true, Storage = "_ShiftID")]
        public Guid ShiftID
        {
            get => _ShiftID;
            set => _ShiftID = value;
        }


        private EntitySet<Route> _Routes;
        [Association(Storage = "_Routes", OtherKey = "ShiftID")]
        public EntitySet<Route> Routes
        {
            get => _Routes; 
            set => _Routes.Assign(value); 
        }

        public Shift()
        {
            _Routes = new EntitySet<Route>();
            ShiftID = Guid.NewGuid();
        }

        public List<Driver> GetRouteDriversList()
        {
            return InMemoryContext.DriverShifts
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
