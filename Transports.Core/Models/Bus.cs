namespace Transports.Core.Models
{
    public class Bus : Transport
    {
        public Bus(string type, TechPassport t) : base(type, t)
        {
        }

        public int PathLength { get; set; }

        public int CountOfSeats
        {
            get => CountOfSeats;
            set => CountOfSeats = 9;
        }
    }
}