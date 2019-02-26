using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Transports")]
    public class Transport : ITransport, IEntity
    {
        private EntityRef<TechPassport> _Passport;

        public Transport(string typeOfTransport, TechPassport tp, int countOfSeats = 9)
        {
            _Passport = new EntityRef<TechPassport>();
            TransportID = Guid.NewGuid();
            TypeOfTransport = typeOfTransport;
            Passport = tp;
            CountOfSeats = countOfSeats;
        }

        public Transport()
        {
            TransportID = Guid.NewGuid();
        }

        [Column(Storage = "_TransportID", IsPrimaryKey = true)]
        public Guid TransportID { get; set; }


        [Column(Storage = "_PassportID")] public Guid PassportID { get; set; }

        [Association(Storage = "_Passport", ThisKey = "PassportID")]
        public TechPassport Passport
        {
            set => _Passport.Entity = value;
            get => _Passport.Entity;
        }

        public Guid TransportId { get; set; }
        public Guid TechPassportId { get; set; }

        [Column] public string TypeOfTransport { get; set; }

        [Column] public int CountOfSeats { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format($"{TypeOfTransport}");
        }
    }
}