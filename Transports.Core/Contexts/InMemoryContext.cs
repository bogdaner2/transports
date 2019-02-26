using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;
using Transports.Core.Interfaces;
using Transports.Core.Models.InMemory;
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
                case SerializationProvider.XmlSerializer:
                    DeserializeXml();
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
                case SerializationProvider.XmlSerializer:
                    SerializeXml();
                    break;
            }
        }

        public void SerializeJson()
        {
            JsonSerializerProvider.Serialize(new DataContractJsonSerializer(Drivers.GetType()), Drivers);
            JsonSerializerProvider.Serialize(new DataContractJsonSerializer(Routes.GetType()), Routes);
            JsonSerializerProvider.Serialize(new DataContractJsonSerializer(Transports.GetType()), Transports);
            JsonSerializerProvider.Serialize(new DataContractJsonSerializer(Shifts.GetType()), Shifts);
            JsonSerializerProvider.Serialize(new DataContractJsonSerializer(DriverShifts.GetType()), DriverShifts);
            JsonSerializerProvider.Serialize(new DataContractJsonSerializer(TechPassports.GetType()), TechPassports);
        }

        public void DeserializeJson()
        {
            JsonSerializerProvider.Deserialize(new DataContractJsonSerializer(Drivers.GetType()), ref Drivers);
            JsonSerializerProvider.Deserialize(new DataContractJsonSerializer(Routes.GetType()),ref Routes);
            JsonSerializerProvider.Deserialize(new DataContractJsonSerializer(Transports.GetType()),ref Transports);
            JsonSerializerProvider.Deserialize(new DataContractJsonSerializer(Shifts.GetType()),ref Shifts);
            JsonSerializerProvider.Deserialize(new DataContractJsonSerializer(DriverShifts.GetType()),ref DriverShifts);
            JsonSerializerProvider.Deserialize(new DataContractJsonSerializer(TechPassports.GetType()),ref TechPassports);
        }

        public void SerializeXml()
        {
            XmlSerializerProvider.Serialize(new XmlSerializer(Drivers.GetType()), Drivers);
            XmlSerializerProvider.Serialize(new XmlSerializer(Routes.GetType()), Routes);
            XmlSerializerProvider.Serialize(new XmlSerializer(Transports.GetType()), Transports);
            XmlSerializerProvider.Serialize(new XmlSerializer(Shifts.GetType()), Shifts);
            XmlSerializerProvider.Serialize(new XmlSerializer(DriverShifts.GetType()), DriverShifts);
            XmlSerializerProvider.Serialize(new XmlSerializer(TechPassports.GetType()), TechPassports);
        }

        public void DeserializeXml()
        {
            XmlSerializerProvider.Deserialize(new XmlSerializer(Drivers.GetType()),ref Drivers);
            XmlSerializerProvider.Deserialize(new XmlSerializer(Routes.GetType()),ref Routes);
            XmlSerializerProvider.Deserialize(new XmlSerializer(Transports.GetType()),ref Transports);
            XmlSerializerProvider.Deserialize(new XmlSerializer(Shifts.GetType()),ref Shifts);
            XmlSerializerProvider.Deserialize(new XmlSerializer(DriverShifts.GetType()),ref DriverShifts);
            XmlSerializerProvider.Deserialize(new XmlSerializer(TechPassports.GetType()),ref TechPassports);
        }

    }
}
