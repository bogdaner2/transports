using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using Transports.Core.Interfaces;
using Transports.Core.Models;
using Transports.Core.SerializationProviders;
using Transports.Core.Services;

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
        public List<TechPassport> TechPassports = new List<TechPassport>();

        public void LoadData()
        {
            switch (StateService.SerializationProvider)
            {
                case SerializationProvider.JsonSerializer:
                    DeserializeJson();
                    break;
            }
        }
        public void SaveData()
        {
            switch (StateService.SerializationProvider)
            {
                case SerializationProvider.JsonSerializer:
                    SerializeJson();
                    break;
            }
        }

        public void SerializeJson()
        {
            JsonSerializer.Serialize(new DataContractJsonSerializer(Drivers.GetType()), Drivers);
            JsonSerializer.Serialize(new DataContractJsonSerializer(Routes.GetType()), Routes);
            JsonSerializer.Serialize(new DataContractJsonSerializer(Transports.GetType()), Transports);
            JsonSerializer.Serialize(new DataContractJsonSerializer(Shifts.GetType()), Shifts);
            JsonSerializer.Serialize(new DataContractJsonSerializer(DriverShifts.GetType()), DriverShifts);
            JsonSerializer.Serialize(new DataContractJsonSerializer(TechPassports.GetType()), TechPassports);
        }

        public void DeserializeJson()
        {
            JsonSerializer.Deserialize(new DataContractJsonSerializer(Drivers.GetType()), ref Drivers);
            JsonSerializer.Deserialize(new DataContractJsonSerializer(Routes.GetType()),ref Routes);
            JsonSerializer.Deserialize(new DataContractJsonSerializer(Transports.GetType()),ref Transports);
            JsonSerializer.Deserialize(new DataContractJsonSerializer(Shifts.GetType()),ref Shifts);
            JsonSerializer.Deserialize(new DataContractJsonSerializer(DriverShifts.GetType()),ref DriverShifts);
            JsonSerializer.Deserialize(new DataContractJsonSerializer(TechPassports.GetType()),ref TechPassports);
        }

        //private void Serialize<T>(XmlSerializer xml, IEnumerable<T> list)
        //{
        //    using (var fs = new FileStream(type.Name + ".xml", FileMode.Create))
        //    {
        //        xml.Serialize(fs, list);
        //    }
        //}


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
    }
}
