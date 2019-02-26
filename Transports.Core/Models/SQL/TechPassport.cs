using System;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.TechPassports")]
    public class TechPassport : ITechPassport, IEntity
    {
        public TechPassport(string brand, int year)
        {
            TechPassportID = Guid.NewGuid();
            Brand = brand;
            YearOfManufacture = year;
        }

        public TechPassport()
        {
            TechPassportID = Guid.NewGuid();
        }

        [Column(IsPrimaryKey = true)]
        public Guid TechPassportID { get; set; }

        public Guid TechPassportId { get; set; }

        [Column] public string Brand { get; set; }

        [Column] public int YearOfManufacture { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format(
                $"Id : {TechPassportID.ToString().Substring(TechPassportID.ToString().Length - 6)} | Brand : {Brand}");
        }
    }
}