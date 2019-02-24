using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Routes")]
    public class Route : IEntity
    {
        private int _time;
        private Guid _RouteID;
        private Guid _ShiftID;
        private EntityRef<Shift> _Shift;

        [Column(IsPrimaryKey = true, Storage = "_RouteID")]
        public Guid RouteID
        {
            get => _RouteID;
            set => _RouteID = value;
        }

        [Column]
        public int Length { get; set; }

        [Column]
        public bool IsTrafficJam { get; set; }

        [Column]
        public int EstimatedTime
        {
            get => _time;
            set => _time = value * (IsTrafficJam ? 2 : 1);
        }

        [Column(Storage = "_ShiftID")]
        public Guid ShiftID
        {
            get => _ShiftID;
            set => _ShiftID = value;
        }

        [Association(Storage = "_Shift", ThisKey = "ShiftID")]
        public Shift Shift
        {
            set => _Shift.Entity = value;
            get => _Shift.Entity;
        }


        public Route(int length, bool isTrafficJam)
        {
            _Shift = new EntityRef<Shift>();
            RouteID = Guid.NewGuid();
            Length = length;
            IsTrafficJam = isTrafficJam;
            EstimatedTime = new Random().Next(1, 120);
        }

        public Route() { }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(RouteID.ToString().Substring(RouteID.ToString().Length - 5))}|Length : {Length} Time : {EstimatedTime}");
        }
    }
}