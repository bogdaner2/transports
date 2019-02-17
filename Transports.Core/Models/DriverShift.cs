using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    public class DriverShift
    {
        public static List<DriverShift> ListOfDriverShifts = new List<DriverShift>();
        private Guid idDriver;
        private Guid idRoute;

        public DriverShift(Route route, Driver driver)
        {
            Driver = driver;
            Route = route;
            ListOfDriverShifts.Add(this);
        }

        public DriverShift()
        {
        }

        public Route Route
        {
            get { return Route.Routes.Find(x => x.Id == idRoute); }
            set => idRoute = value.Id;
        }

        public Driver Driver
        {
            get { return Driver.DriverList.Find(x => x.Id == idDriver); }
            set => idDriver = value.Id;
        }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("DriverShifts.xml", FileMode.Create))
            {
                xml.Serialize(fs, ListOfDriverShifts);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("DriverShifts.xml", FileMode.OpenOrCreate))
            {
                ListOfDriverShifts = (List<DriverShift>) xml.Deserialize(fileStream);
            }
        }

        public override string ToString()
        {
            return string.Format($"{Driver.Name} | Len{Route.Length}");
        }
    }
}