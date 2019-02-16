using System;
using System.Runtime.Serialization;

namespace Transports.Core.Models
{
    [Serializable]
    [DataContract]
    public abstract class Human : Base
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Age { get; set; }
        public Human(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Human() { }

        public override string ToString()
        {
            return string.Format($"Name : {Name} , Age : {Age}");
        }
    }
}