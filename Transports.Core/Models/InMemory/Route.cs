using System;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class Route : IRoute
    {
        [DataMember]
        public Guid RouteId { get; set; }

        [DataMember]
        public int Length { get; set; }

        [DataMember]
        public bool IsTrafficJam { get; set; }
       
        [DataMember]
        public int EstimatedTime { get; set; }
        [DataMember]
        public Guid ShiftId { get; set; }

        public Route(int length, bool isTrafficJam)
        {
            RouteId = Guid.NewGuid();
            Length = length;
            IsTrafficJam = isTrafficJam;
            EstimatedTime = new Random().Next(1, 120);
        }

        public Route() { }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(RouteId.ToString().Substring(RouteId.ToString().Length - 5))}|Length : {Length} Time : {EstimatedTime}");
        }

        public object Clone() => MemberwiseClone();
    }
}