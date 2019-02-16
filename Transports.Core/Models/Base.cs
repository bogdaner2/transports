using System;
using System.Runtime.Serialization;

namespace Transports.Core
{
    [Serializable]
    [DataContract]
    public class Base
    {
        [DataMember]
        public Guid Id { get;  set; }

        public Base()
        {
            Id = Guid.NewGuid();
        }

    }
}
