using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Transport.Data;
using Transports.Core.Driver;

namespace Transports.Core
{
    [Serializable]
    public class Route : Base
    {
        public static List<Route> RouteList = new List<Route>();

        private readonly int Km;
        private int Time;

        public Route(int lenth, bool IsTrafficJam)
        {
            Lenth = lenth;
            this.IsTrafficJam = IsTrafficJam;
            EstimattedTime = new Random().Next(1, 120);
            RouteList.Add(this);
        }

        public Route(string start, string finish)
        {
            Start = start;
            Finish = finish;
            RouteList.Add(this);
        }

        public Route()
        {
        }

        public string Start { get; }
        public string Finish { get; set; }

        public int Lenth { get; set; }

        public int EstimattedTime
        {
            get => Time;
            set => Time = value * (IsTrafficJam ? 2 : 1);
        }

        public bool IsTrafficJam { get; set; }

        public static List<Route> GetListByLen(int length)
        {
            return (from x in RouteList
                where x.Lenth > length
                select x).ToList();
        }

        public List<Driver.Driver> GetRouteDriversList()
        {
            var res = new List<Driver.Driver>();
            foreach (var driverShift in DriverShift.ListOfDriverShifts)
                if (driverShift.Route == this)
                    res.Add(driverShift.Driver);
            return res;
        }

        public int GetTime()
        {
            return 0;
        }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Routes.xml", FileMode.Create))
            {
                xml.Serialize(fs, RouteList);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Routes.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    RouteList = (List<Route>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(Id.ToString().Substring(Id.ToString().Length - 5))}|Lenth : {Lenth} Time : {EstimattedTime}");
        }
    }
}