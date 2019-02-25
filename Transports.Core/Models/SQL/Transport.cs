using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Transports")]
    public class Transport : ITransport, IEntity
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

        public Guid TransportId { get; set; }
        public Guid TechPassportId { get; set; }
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
        }

        public Transport() { TransportID = Guid.NewGuid(); }

        public override string ToString() => string.Format($"{TypeOfTransport}");
        public object Clone() => MemberwiseClone();
    }
}