using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class TechPassport : ITechPassport, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TechPassportId { get; set; }
        public string Brand { get; set; }
        public int YearOfManufacture { get; set; }

        public virtual Transport Transport { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}