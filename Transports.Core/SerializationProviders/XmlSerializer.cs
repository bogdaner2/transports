using System.IO;
using System.Xml.Serialization;

namespace Transports.Core.SerializationProviders
{
    public static class XmlSerializerProvider
    {
        public static void Serialize(XmlSerializer xml, object items)
        {
            using (var fs = new FileStream($"{GenerateFileName(items)}.xml", FileMode.Create))
            {
                xml.Serialize(fs, items);
            }
        }


        public static void Deserialize<T>(XmlSerializer xml, ref T items)
        {
            try
            {
                using (var fileStream = new FileStream($"{GenerateFileName(items)}.xml", FileMode.OpenOrCreate))
                {
                    items = (T) xml.Deserialize(fileStream);
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
