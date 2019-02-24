using System;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Runtime.Serialization;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.TechPassports"), Serializable, DataContract]
    public class TechPassport : ICloneable, IEntity
    {
        private Guid _TechPassportID;

        [Column(IsPrimaryKey = true, Storage = "_TechPassportID"), DataMember]
        public Guid TechPassportID
        {
            get => _TechPassportID;
            set => _TechPassportID = value;
        }
        [Column, DataMember]
        public string Brand { get; set; }
        [Column, DataMember]
        public int YearOfManufacture { get; set; }
        public TechPassport(string brand, int year)
        {
            TechPassportID = Guid.NewGuid();
            Brand = brand;
            YearOfManufacture = year;
        }

        public TechPassport() { TechPassportID = Guid.NewGuid(); }

        public override string ToString()
        {
            return string.Format(
                $"Id : {TechPassportID.ToString().Substring(TechPassportID.ToString().Length - 6)} | Brand : {Brand}");
        }

        public object Clone() => MemberwiseClone();
    }
}