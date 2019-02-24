﻿using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Transports.Core.Contexts;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.Transports"), DataContract, Serializable]
    public class Transport : ICloneable, IEntity
    {
        private Guid _TransportID;
        private Guid _PassportID;
        private EntityRef<TechPassport> _Passport;

        [Column(Storage = "_TransportID", IsPrimaryKey = true), DataMember]
        public Guid TransportID
        {
            get => _TransportID;
            set => _TransportID = value;
        }

        [Column, DataMember]
        public string TypeOfTransport { get; set; }
        [Column, DataMember]
        public int CountOfSeats { get; set; }


        [Column(Storage = "_PassportID"), DataMember]
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
        }

        public Transport() { TransportID = Guid.NewGuid(); }

        public override string ToString() => string.Format($"{TypeOfTransport}");
        public object Clone() => MemberwiseClone();
    }
}