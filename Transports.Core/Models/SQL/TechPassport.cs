﻿using System;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using Transports.Core.Interfaces.Models;

namespace Transports.Core.Models.SQL
{
    [Table(Name = "dbo.TechPassports")] 
    public class TechPassport : ITechPassport, IEntity
    {
        private Guid _TechPassportID;

        [Column(IsPrimaryKey = true, Storage = "_TechPassportID")]
        public Guid TechPassportID
        {
            get => _TechPassportID;
            set => _TechPassportID = value;
        }

        public Guid TechPassportId { get; set; }

        [Column]
        public string Brand { get; set; }
        [Column]
        public int YearOfManufacture { get; set; }
        public TechPassport(string brand, int year)
        {
            TechPassportID = Guid.NewGuid();
            Brand = brand;
            YearOfManufacture = year;
        }

        public TechPassport() { TechPassportID = Guid.NewGuid(); }

        public override string ToString()
        {
            return string.Format(
                $"Id : {TechPassportID.ToString().Substring(TechPassportID.ToString().Length - 6)} | Brand : {Brand}");
        }

        public object Clone() => MemberwiseClone();
    }
}