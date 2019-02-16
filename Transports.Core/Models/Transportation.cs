﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    public class Transportation : Base
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
        public Models.Driver Driver { get; set; }
        public Models.Transport Transport { get; set; }


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
                return string.Format($"{Route.Lenth} {Driver.Name} {Transport.TypeOfTransport}");
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}