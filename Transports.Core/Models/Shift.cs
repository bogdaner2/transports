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
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }
        [Column]
        public DateTime Start { get; set; }
        [Column]
        public DateTime End { get; set; }

        //[Association(
        //    Storage = "_CustomerAddresses",
        //    ThisKey = "CustomerId",
        //    OtherKey = "CustomerId")]
        //public EntitySet<Route> Router { get; set; }

        public Shift() {Id = Guid.NewGuid();}

        public List<Driver> GetRouteDriversList()
        {
            return InMemoryContext.DriverShifts
                .Where(x => x.Shift == this)
                .Select(x => x.Driver)
                .ToList();
        }

    }
}
