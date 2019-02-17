using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [Table(Name = "dbo.Routes")]
    public class Route
    {
        public static List<Route> Routes = new List<Route>();

        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        private readonly int _km;
        private int Time;

        public Route(int length, bool isTrafficJam)
        {
            Id = Guid.NewGuid();
            Length = length;
            IsTrafficJam = isTrafficJam;
            EstimatedTime = new Random().Next(1, 120);
            Routes.Add(this);
        }

        public Route() { }

        public int Length { get; set; }

        public int EstimatedTime
        {
            get => Time;
            set => Time = value * (IsTrafficJam ? 2 : 1);
        }

        public bool IsTrafficJam { get; set; }

        public static List<Route> GetListByLen(int length)
        {
            return (from x in Routes
                where x.Length > length
                select x).ToList();
        }

        public List<Driver> GetRouteDriversList()
        {
            var res = new List<Driver>();
            foreach (var driverShift in DriverShift.ListOfDriverShifts)
                if (driverShift.Route == this)
                    res.Add(driverShift.Driver);
            return res;
        }
        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Routes.xml", FileMode.Create))
            {
                xml.Serialize(fs, Routes);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Routes.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    Routes = (List<Route>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(Id.ToString().Substring(Id.ToString().Length - 5))}|Length : {Length} Time : {EstimatedTime}");
        }
    }
}