using System;

namespace Transports.Core.Interfaces.Models
{
    public interface IDriverShift : ICloneable
    {
        Guid DriverId { get;set; }
        Guid ShiftId { get; set; }
        Guid DriverShiftId { get; set; }
        object Clone();
    }
}
