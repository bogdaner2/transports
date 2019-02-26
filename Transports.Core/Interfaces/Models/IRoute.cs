using System;

namespace Transports.Core.Interfaces.Models
{
    public interface IRoute : ICloneable
    {
        Guid RouteId { get; set; }
        int Length { get; set; }
        bool IsTrafficJam { get; set; }
        int EstimatedTime { get; set; }
        Guid ShiftId { get; set; }
    }
}
