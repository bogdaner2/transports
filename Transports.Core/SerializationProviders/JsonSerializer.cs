using System.IO;
using System.Runtime.Serialization.Json;

namespace Transports.Core.SerializationProviders
{
    public class JsonSerializer
    {
        //public void SerializeJson(object type)
        //{
        //    switch (type)
        //    {
        //        case List<Driver> list:
        //            Serialize(new DataContractJsonSerializer(list.GetType()), type);
        //            break;
        //        case List<Route> list:
        //            Serialize(new DataContractJsonSerializer(list.GetType()), type);
        //            break;
        //        case List<Transport> list:
        //            Serialize(new DataContractJsonSerializer(list.GetType()), type);
        //            break;
        //        default:
        //            throw new ArgumentException(
        //                "unknown type",
        //                nameof(type));
        //    }
        //}

        //public void DeserializeJson(object type)
        //{
        //    switch (type)
        //    {
        //        case List<Driver> list:
        //            Deserialize(new DataContractJsonSerializer(typeof(List<Driver>)), ref InMemoryContext.Instance.Drivers);
        //            break;
        //        case List<Route> list:
        //            Deserialize(new DataContractJsonSerializer(list.GetType()), ref type);
        //            break;
        //        case List<Transport> list:
        //             Deserialize(new DataContractJsonSerializer(list.GetType()),ref  type);
        //            break;
        //        default:
        //            throw new ArgumentException(
        //                "unknown type",
        //                nameof(type));
        //    }
        //}

        public static void Serialize(DataContractJsonSerializer jsonFormatter, object items)
        {
            using (var fs = new FileStream(NameOfFile(items) + ".json", FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, items);
            }
        }

        public static void Deserialize<T>(DataContractJsonSerializer jsonFormatter, ref T items)
        {
            using (var fs = new FileStream(NameOfFile(items) + ".json", FileMode.OpenOrCreate))
            {
                items = (T) jsonFormatter.ReadObject(fs);
            }
        }

        private static string NameOfFile<T>(T @object)
        {
            var fullname = @object.GetType().ToString();
            return fullname.Substring(fullname.LastIndexOf(".") + 1).TrimEnd(']');
        }
    }
}