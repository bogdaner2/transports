using System;

namespace Transports.Core.Transport
{
    [Serializable]
    public class TechPasport : Base
    {
        // public int Pass { private get; set; }

        public TechPasport(string b, int year)
        {
            Brand = b;
            YearOfManufacture = year;
        }

        //public int GetPass()
        //{
        //    return Pass;
        //}

        public TechPasport()
        {
        }

        public string Brand { get; set; }
        public int YearOfManufacture { get; set; }

        public override string ToString()
        {
            return string.Format($"{Brand}");
        }
    }
}