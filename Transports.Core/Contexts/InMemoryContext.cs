using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using Transports.Core.Interfaces;
using Transports.Core.Models;
using Transports.Core.SerializationProviders;

namespace Transports.Core.Contexts
{
    public class InMemoryContext : IContext
    {
        private static readonly Lazy<InMemoryContext> Lazy = new Lazy<InMemoryContext>(() => new InMemoryContext());
        public static InMemoryContext Instance => Lazy.Value;

        private InMemoryContext() { }

        public List<Driver> Drivers = new List<Driver>();
        public List<Shift> Shifts = new List<Shift>();
        public List<DriverShift> DriverShifts = new List<DriverShift>();
        public List<Transport> Transports = new List<Transport>();
        public List<Route> Routes = new List<Route>();

        public void LoadData()
        {
            // Serialize()
        }

        //private void Serialize<T>(XmlSerializer xml, IEnumerable<T> list)
        //{
        //    using (var fs = new FileStream(type.Name + ".xml", FileMode.Create))
        //    {
        //        xml.Serialize(fs, list);
        //    }
        //}

        public void SerializeJson(object type)
        {
            switch (type)
            {
                case List<Driver> list:
                    JsonSerializer.Serialize(new DataContractJsonSerializer(list.GetType()), type);
                    break;
                case List<Route> list:
                    JsonSerializer.Serialize(new DataContractJsonSerializer(list.GetType()), type);
                    break;
                case List<Transport> list:
                    JsonSerializer.Serialize(new DataContractJsonSerializer(list.GetType()), type);
                    break;
                default:
                    throw new ArgumentException(
                        "unknown type",
                        nameof(type));
            }
        }

        public void DeserializeJson(object type)
        {
            switch (type)
            {
                case List<Driver> list:
                    JsonSerializer.Deserialize(new DataContractJsonSerializer(typeof(List<Driver>)), ref Drivers);
                    break;
                case List<Route> list:
                    JsonSerializer.Deserialize(new DataContractJsonSerializer(list.GetType()), ref type);
                    break;
                case List<Transport> list:
                    JsonSerializer.Deserialize(new DataContractJsonSerializer(list.GetType()), ref type);
                    break;
                default:
                    throw new ArgumentException(
                        "unknown type",
                        nameof(type));
            }
        }

        //public static void Deserialize(XmlSerializer xml)
        //{
        //    using (var fileStream = new FileStream("Drivers.xml", FileMode.OpenOrCreate))
        //    {
        //        try
        //        {
        //            InMemoryContext.Instance.Drivers = (List<Driver>) xml.Deserialize(fileStream);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }
        //}

        public void SaveData()
        {
            SerializeJson(Drivers);
        }
    }
}
