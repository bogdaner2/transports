using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class TechPassport : ITechPassport, IEntity
    {
        [Key]
        public Guid TechPassportId { get; set; }
        public string Brand { get; set; }
        public int YearOfManufacture { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}