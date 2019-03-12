using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{

    public class Transport : ITransport, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid TransportId { get; set; }
        public string TypeOfTransport { get; set; }
        public int CountOfSeats { get; set; }

        public TechPassport TechPassport { get; set; }

        [NotMapped]
        public Guid TechPassportId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}