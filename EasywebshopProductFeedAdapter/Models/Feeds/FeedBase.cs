using EasywebshopProductFeedAdapter.Util.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace EasywebshopProductFeedAdapter.Domain.Feeds
{
    public abstract class FeedBase : IFeed
    {

        public void SerializeTo(IFeedSerializer serializer, Stream outputStream)
        {
            serializer.Serialize(this, outputStream);
        }

        public abstract void DeserializeFrom(IFeedSerializer serializer, string inputString);
    }
}
