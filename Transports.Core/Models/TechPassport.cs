using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.TechPassports"), Serializable, DataContract]
    public class TechPassport : IEntity
    {
        private Guid _TechPassportID;

        [Column(IsPrimaryKey = true, Storage = "_TechPassportID")]
        public Guid TechPassportID
        {
            get => _TechPassportID;
            set => _TechPassportID = value;
        }
        [Column]
        public string Brand { get; set; }
        [Column]
        public int YearOfManufacture { get; set; }
        public TechPassport(string brand, int year)
        {
            TechPassportID = Guid.NewGuid();
            Brand = brand;
            YearOfManufacture = year;
        }

        public TechPassport() { }

        public override string ToString() => string.Format($"{Brand}");
    }
}