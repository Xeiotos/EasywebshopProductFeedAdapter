using EasywebshopProductFeedAdapter.Util.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace EasywebshopProductFeedAdapter.Domain.Feeds
{
    public interface IFeed
    {
        void SerializeTo(IFeedSerializer serializer, Stream outputStream);
        void DeserializeFrom(IFeedSerializer serializer, String inputString);
    }
}
