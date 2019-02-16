using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transports.Core.Transport;

namespace Transport.Data
{
    public class SportCar : Car
    {

        public int FuelStore { get; set; }

        public SportCar(string f , TechPasport t) : base (f,t)
        {

        }

    }
}
