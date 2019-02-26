using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.Routes")]
    public class Route : IRoute, IEntity
    {
        private EntityRef<Shift> _Shift;
        private int _time;


        public Route(int length, bool isTrafficJam)
        {
            _Shift = new EntityRef<Shift>();
            RouteID = Guid.NewGuid();
            Length = length;
            IsTrafficJam = isTrafficJam;
            EstimatedTime = new Random().Next(1, 120);
        }

        public Route()
        {
        }

        [Column(IsPrimaryKey = true, Storage = "_RouteID")]
        public Guid RouteID { get; set; }

        [Column(Storage = "_ShiftID")] public Guid ShiftID { get; set; }

        [Association(Storage = "_Shift", ThisKey = "ShiftID")]
        public Shift Shift
        {
            set => _Shift.Entity = value;
            get => _Shift.Entity;
        }

        public Guid RouteId { get; set; }

        [Column] public int Length { get; set; }

        [Column] public bool IsTrafficJam { get; set; }

        [Column] public int EstimatedTime { get; set; }

        public Guid ShiftId { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(RouteID.ToString().Substring(RouteID.ToString().Length - 5))}|Length : {Length} Time : {EstimatedTime}");
        }
    }
}