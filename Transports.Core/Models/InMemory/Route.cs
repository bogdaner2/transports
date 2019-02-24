using System;
using System.Runtime.Serialization;

namespace Transports.Core.Models.InMemory
{
    [Serializable, DataContract]
    public class Route
    {
        private int _time;

        [DataMember]
        public Guid RouteId { get; set; }

        [DataMember]
        public int Length { get; set; }

        [DataMember]
        public bool IsTrafficJam { get; set; }

        [DataMember]
        public int EstimatedTime
        {
            get => _time;
            set => _time = value * (IsTrafficJam ? 2 : 1);
        }

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
    }
}