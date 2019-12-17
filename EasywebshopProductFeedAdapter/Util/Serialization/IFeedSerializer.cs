using EasywebshopProductFeedAdapter.Domain.Feeds;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Util.Serialization
{
    public interface IFeedSerializer
    {
        void Serialize(IFeed src, Stream outputStream);
        XmlFeed Deserialize(string inputString);
    }
}
