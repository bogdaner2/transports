using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.TechPassports")]
    public class TechPassport : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }
        [Column]
        public string Brand { get; set; }
        [Column]
        public int YearOfManufacture { get; set; }
        public TechPassport(string brand, int year)
        {
            Id = Guid.NewGuid();
            Brand = brand;
            YearOfManufacture = year;
        }

        public override string ToString()
        {
            return string.Format($"{Brand}");
        }
    }
}