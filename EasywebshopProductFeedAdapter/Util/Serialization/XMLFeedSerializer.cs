using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using EasywebshopProductFeedAdapter.Domain.Feeds;

namespace EasywebshopProductFeedAdapter.Util.Serialization
{
    public class XMLFeedSerializer : IFeedSerializer
    {
        public XmlFeed Deserialize(string inputString)
        {
            var xml = new XmlDocument();
            xml.LoadXml(inputString);
            return new XmlFeed(xml);
        }

        public void Serialize(IFeed src, Stream outputStream)
        {
            throw new NotImplementedException(); //TODO
        }
    }
}
