using System;

namespace Transports.Core.Interfaces.Models
{
    public interface IShift : ICloneable
    {
        DateTime Start { get; set; }
        DateTime End { get; set; }
        Guid ShiftId { get; set; }
        int TotalRoutes { get; }
        int TotalDrivers { get; }
    }
}
