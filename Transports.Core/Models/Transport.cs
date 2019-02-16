using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    public class Transport : Base
    {
        public static List<Transport> ListOfTransport = new List<Transport>();

        public Transport(string typeOfTransport, TechPassport tp)
        {
            TypeOfTransport = typeOfTransport;
            Passport = tp;
            ListOfTransport.Add(this);
        }

        private Transport()
        {
        }

        public TechPassport Passport { get; set; }

        public string TypeOfTransport { get; set; }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Transport.xml", FileMode.Create))
            {
                xml.Serialize(fs, ListOfTransport);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Transport.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    ListOfTransport = (List<Transport>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format($"{TypeOfTransport}");
        }
    }
}