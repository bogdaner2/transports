using System;
using System.Linq;
using System.Runtime.Serialization;
using Transports.Core.Contexts;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [DataContract, Serializable]
    public class Transport : ITransport
    {
        [DataMember]
        public Guid TransportId { get; set; }
        [DataMember]
        public Guid TechPassportId { get; set; }

        [DataMember]
        public string TypeOfTransport { get; set; }
        [DataMember]
        public int CountOfSeats { get; set; }

        public TechPassport Passport
        {
            get => InMemoryContext.Instance.TechPassports.FirstOrDefault(x => x.TechPassportId == TechPassportId);
            set => TechPassportId = value.TechPassportId;
        }

        public Transport(string typeOfTransport, TechPassport tp,int countOfSeats = 9)
        {
            TransportId = Guid.NewGuid();
            TypeOfTransport = typeOfTransport;
            Passport = tp;
            CountOfSeats = countOfSeats;
        }

        public Transport() { TransportId = Guid.NewGuid(); }

        public override string ToString() => string.Format($"{TypeOfTransport}");
        public object Clone() => MemberwiseClone();
    }
}