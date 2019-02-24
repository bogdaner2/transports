using System;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class TechPassport : ITechPassport
    {
        [DataMember]
        public Guid TechPassportId { get; set; }
        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public int YearOfManufacture { get; set; }

        public TechPassport(string brand, int year)
        {
            TechPassportId = Guid.NewGuid();
            Brand = brand;
            YearOfManufacture = year;
        }

        public TechPassport() { TechPassportId = Guid.NewGuid(); }

        public override string ToString()
        {
            return string.Format(
                $"Id : {TechPassportId.ToString().Substring(TechPassportId.ToString().Length - 6)} | Brand : {Brand}");
        }

        public object Clone() => MemberwiseClone();
    }
}