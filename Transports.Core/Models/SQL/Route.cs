using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class Route : IRoute, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RouteId { get; set; }
        public int Length { get; set; }
        public bool IsTrafficJam { get; set; }
        public int EstimatedTime { get; set; }
        public Shift Shift { get; set; }


        [NotMapped]
        public Guid ShiftId { get; set; }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}