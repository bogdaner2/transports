using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.Transportations")]
    public class Transportation
    {
        public static List<Transportation> ListOfTransportation = new List<Transportation>();

        public Transportation(Driver d, Route r, Transport t)
        {
            Route = r;
            Driver = d;
            Transport = t;
            ListOfTransportation.Add(this);
        }

        public Transportation() {}

        public Route Route { get; set; }
        public Driver Driver { get; set; }
        public Transport Transport { get; set; }


        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Transportation.xml", FileMode.Create))
            {
                xml.Serialize(fs, ListOfTransportation);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Transportation.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    ListOfTransportation = (List<Transportation>) xml.Deserialize(fs);
                }
                catch { }
            }
        }

        public override string ToString()
        {
            try
            {
                return string.Format($"{Route.Length} {Driver.Name} {Transport.TypeOfTransport}");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}