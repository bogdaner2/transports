using System;

namespace Transports.Core.Interfaces.Models
{
    public interface ITechPassport : ICloneable
    {
        Guid TechPassportId { get; set; }
        string Brand { get; set; }
        int YearOfManufacture { get; set; }
        object Clone();
    }
}
