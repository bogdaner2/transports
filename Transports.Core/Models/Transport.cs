using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.Transports"), DataContract, Serializable]
    public class Transport : IEntity
    {
        private Guid _TransportID;
        private Guid _PassportID;
        private EntityRef<TechPassport> _Passport;

        [Column(Storage = "_TransportID", IsPrimaryKey = true)]
        public Guid TransportID
        {
            get => _TransportID;
            set => _TransportID = value;
        }

        [Column]
        public string TypeOfTransport { get; set; }
        [Column]
        public int CountOfSeats { get; set; }


        [Column(Storage = "_PassportID")]
        public Guid PassportID
        {
            get => _PassportID;
            set => _PassportID = value;
        }

        [Association(Storage = "_Passport", ThisKey = "PassportID")]
        public TechPassport Passport
        {
            set => _Passport.Entity = value;
            get => _Passport.Entity;
        }

        public Transport(string typeOfTransport, TechPassport tp,int countOfSeats = 9)
        {
            _Passport = new EntityRef<TechPassport>();
            TransportID = Guid.NewGuid();
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