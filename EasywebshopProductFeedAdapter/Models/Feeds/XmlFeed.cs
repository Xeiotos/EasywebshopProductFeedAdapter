using EasywebshopProductFeedAdapter.Util.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace EasywebshopProductFeedAdapter.Domain.Feeds
{
    public class XmlFeed : FeedBase
    {
        public XmlDocument FeedData { get; protected set; }

        public XmlFeed(XmlDocument feedData)
        {
            FeedData = feedData;
        }

        public override void DeserializeFrom(IFeedSerializer serializer, string inputString)
        {
            FeedData = serializer.Deserialize(inputString).FeedData;
        }
    }
}
