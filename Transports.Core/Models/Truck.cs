using Transports.Core.Transport;

namespace Transport.Data
{
    public class Truck : Car
    {
        public Truck(string f, TechPasport t) : base(f, t)
        {
        }

        public int CargoWeight { get; set; }

        public int MaxSpeed { get; private set; }
    }
}