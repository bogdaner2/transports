using Transport.Data;

namespace Transports.Core.Transport
{
    public class Car : Transport
    {
        public Car(string type, TechPasport t) : base(type, t)
        {
        }

        public int PathLenth { get; set; }

        public string KindOfRoad { get; private set; }

        public int CountOfSeets
        {
            get => CountOfSeets;
            set => CountOfSeets = 9;
        }
    }
}