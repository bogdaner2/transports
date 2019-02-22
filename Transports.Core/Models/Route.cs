using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Transports.Core.Contexts;

namespace Transports.Core.Models
{
    [Table(Name = "dbo.Routes"), Serializable, DataContract]
    public class Route : IEntity
    {
        private int _time;
        private Guid _RouteID;
        private Guid _ShiftID;
        private EntityRef<Shift> _Shift;

        [Column(IsPrimaryKey = true, Storage = "_RouteID"), DataMember]
        public Guid RouteID
        {
            get => _RouteID;
            set => _RouteID = value;
        }

        [Column, DataMember]
        public int Length { get; set; }

        [Column, DataMember]
        public bool IsTrafficJam { get; set; }

        [Column, DataMember]
        public int EstimatedTime
        {
            get => _time;
            set => _time = value * (IsTrafficJam ? 2 : 1);
        }

        [Column(Storage = "_ShiftID"), DataMember]
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
            InMemoryContext.Instance.Routes.Add(this);
        }

        public Route() { }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Routes.xml", FileMode.Create))
            {
                xml.Serialize(fs, InMemoryContext.Instance.Routes);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Routes.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    InMemoryContext.Instance.Routes = (List<Route>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(RouteID.ToString().Substring(RouteID.ToString().Length - 5))}|Length : {Length} Time : {EstimatedTime}");
        }
    }
}