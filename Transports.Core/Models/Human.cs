using System;
using System.Runtime.Serialization;

namespace Transports.Core.Driver
{
    [Serializable]
    [DataContract]
    public abstract class Human : Base
    {
        public Human(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Human()
        {
        }

        [DataMember] public string Name { get; set; }

        [DataMember] public int Age { get; set; }

        public override string ToString()
        {
            return string.Format($"Name : {Name} , Age : {Age}");
        }
    }
}