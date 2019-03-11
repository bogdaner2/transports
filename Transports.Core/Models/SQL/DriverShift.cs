using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    public class DriverShift : IDriverShift, IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid DriverShiftId { get; set; }

        [NotMapped]
        public Guid ShiftId { get; set; }
        [NotMapped]
        public Guid DriverId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}