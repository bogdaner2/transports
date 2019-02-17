using System;

namespace Transports.Core.Models
{
    [Serializable]
    public class TechPassport
    {
        public TechPassport(string b, int year)
        {
            Brand = b;
            YearOfManufacture = year;
        }

        public string Brand { get; set; }
        public int YearOfManufacture { get; set; }

        public override string ToString()
        {
            return string.Format($"{Brand}");
        }
    }
}