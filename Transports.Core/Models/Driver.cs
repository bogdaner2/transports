using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    public class Driver : Human
    {
        public bool IsAdult() => Age >= 18;

        public static List<Driver> DriverList = new List<Driver>();

        public Driver(string name, int age, int rang) : base(name, age)
        {
            Name = name;
            Age = age;
            Rang = rang.ToString();
            DriverList.Add(this);
        }
        public enum RangEnum
        {
            A,
            B,
            C,
            D
        }
        private string rang;

        public Driver() : this("Ivan", 16, 1)
        {
        }

        [DataMember]
        public string Rang
        {
            get => rang;
            set
            {
                if (Age >= 18)
                    try
                    {
                        rang = Enum.GetName(typeof(RangEnum), int.Parse(value));
                    }
                    catch (FormatException)
                    {
                        rang = value;
                    }
                else
                    rang = "A";
            }
        }

        public List<Route> GetDriverRoutesList()
        {
            var res = new List<Route>();
            foreach (var ds in DriverShift.ListOfDriverShifts)
                if (ds.Driver == this)
                    res.Add(ds.Route);
            return res;
        }

        public void PassExam()
        {
            //var random = new Random();
            //if (Rang != "D")
            //{
            //    if (random.Next(0, 10) <= 3) MessageBox.Show("Info", "Pass");
            //}
            //else
            //{
            //    MessageBox.Show("Info", "Max is D");
            //}
        }

        public void PassExam(bool IsPrepared)
        {
            //var random = new Random();
            //if (Rang != "D")
            //{
            //    if (random.Next(0, 10) <= 8) MessageBox.Show("Info", "Pass");
            //}
            //else
            //{
            //    MessageBox.Show("Info", "Max is D");
            //}
        }

        public static void Serialize(XmlSerializer xml)
        {
            using (var fs = new FileStream("Drivers.xml", FileMode.Create))
            {
                xml.Serialize(fs, DriverList);
            }
        }

        public static void Deserialize(XmlSerializer xml)
        {
            using (var fileStream = new FileStream("Drivers.xml", FileMode.OpenOrCreate))
            {
                try
                {
                    DriverList = (List<Driver>) xml.Deserialize(fileStream);
                }
                catch (Exception)
                {
                }
            }
        }

        public override string ToString()
        {
            return string.Format($"{Name} {Age} years.Rang {Rang}");
        }
    }
}