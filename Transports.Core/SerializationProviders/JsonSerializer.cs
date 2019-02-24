using System.IO;
using System.Runtime.Serialization.Json;

namespace Transports.Core.SerializationProviders
{
    public static class JsonSerializerProvider
    {
        public static void Serialize(DataContractJsonSerializer jsonFormatter, object items)
        {
            using (var fs = new FileStream($"{GenerateFileName(items)}.json", FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, items);
            }
        }

        public static void Deserialize<T>(DataContractJsonSerializer jsonFormatter, ref T items)
        {
            try
            {
                using (var fs = new FileStream($"{GenerateFileName(items)}.json", FileMode.OpenOrCreate))
                {
                    items = (T) jsonFormatter.ReadObject(fs);
                }
            }
            catch { }
        }

        private static string GenerateFileName<T>(T @object)
        {
            var fullname = @object.GetType().ToString();
            return fullname.Substring(fullname.LastIndexOf(".") + 1).TrimEnd(']');
        }
    }
}