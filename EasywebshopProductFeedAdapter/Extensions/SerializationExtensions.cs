using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasywebshopProductFeedAdapter.Extensions
{
    public static class SerializationExtensions
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, toSerialize);
                return textWriter.ToString();
            }
        }
    }
}
