using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.Transports")]
    public class Transport : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }
        [Column]
        public string TypeOfTransport { get; set; }
        [Column]
        public int CountOfSeats { get; set; }

        private EntityRef<TechPassport> _passport;
        [Association(ThisKey = "AddressId", OtherKey = "AddressId")]
        public TechPassport Passport
        {
            set => _passport.Entity = value;
            get => _passport.Entity;
        }

        public Transport(string typeOfTransport, TechPassport tp,int countOfSeats = 9)
        {
            TypeOfTransport = typeOfTransport;
            Passport = tp;
            CountOfSeats = countOfSeats;
            InMemoryContext.Transports.Add(this);
        }

        private Transport()
        {
        }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Transport.xml", FileMode.Create))
            {
                xml.Serialize(fs, InMemoryContext.Transports);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Transport.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    InMemoryContext.Transports = (List<Transport>) xml.Deserialize(fileStream);
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