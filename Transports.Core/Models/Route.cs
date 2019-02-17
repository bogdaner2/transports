﻿using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    [Table(Name = "dbo.Routes")]
    public class Route : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }
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
        private int _time;

        public Route(int length, bool isTrafficJam)
        {
            Id = Guid.NewGuid();
            Length = length;
            IsTrafficJam = isTrafficJam;
            EstimatedTime = new Random().Next(1, 120);
            InMemoryContext.Routes.Add(this);
        }

        public Route() { }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Routes.xml", FileMode.Create))
            {
                xml.Serialize(fs, InMemoryContext.Routes);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Routes.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    InMemoryContext.Routes = (List<Route>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format(
                $"{string.Format(Id.ToString().Substring(Id.ToString().Length - 5))}|Length : {Length} Time : {EstimatedTime}");
        }
    }
}