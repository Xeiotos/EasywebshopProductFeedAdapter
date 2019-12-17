using EasywebshopProductFeedAdapter.Util.Serialization;
using Geta.GoogleProductFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasywebshopProductFeedAdapter.Domain.Feeds
{
    [XmlRoot("feed")]
    [Serializable]
    public class Feed : FeedBase
    {

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("updated")]
        public DateTime Updated { get; set; }

        [XmlElement("entry")]
        public List<Entry> Entries { get; set; }

        public override void DeserializeFrom(IFeedSerializer serializer, string inputString)
        {
            //TODO (But not critical)
            //It's probably beneficial to not re-generate the XML Feed every time, 
            //but to deserialize the existing one and make modifications.
        }
    }
}
