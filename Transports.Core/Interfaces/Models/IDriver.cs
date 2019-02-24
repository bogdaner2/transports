using System;
using Transports.Core.Models;

namespace Transports.Core.Interfaces.Models
{
    public interface IDriver : ICloneable
    {
        Guid DriverId { get; set; }
        string Name { get; set; }
        int Age { get; set; }
        RangEnum Rang { get; set; }
        object Clone();
    }
}
