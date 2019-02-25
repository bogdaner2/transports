using System;

namespace Transports.Core.Interfaces.Models
{
    public interface ITransport : ICloneable
    {
        Guid TransportId { get; set; }
        Guid TechPassportId { get; set; }
        string TypeOfTransport { get; set; }
        int CountOfSeats { get; set; }
    }
}
