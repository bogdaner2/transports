using System.IO;
using System.Runtime.Serialization.Json;

namespace Transports.Core.SerializationProviders
{
    public class JsonSerializer
    {
        public static void Serialize(DataContractJsonSerializer jsonFormatter, object items)
        {
            using (var fs = new FileStream(NameOfFile(items) + ".json", FileMode.Create))
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