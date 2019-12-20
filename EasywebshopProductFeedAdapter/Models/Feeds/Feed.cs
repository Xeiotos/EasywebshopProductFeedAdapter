using Geta.GoogleProductFeed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasywebshopProductFeedAdapter.Domain.Feeds
{
    [XmlRoot("feed")]
    public class Feed
    {
        public Feed()
        {

        }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("link")]
        public string Link { get; set; }

        [XmlElement("updated")]
        public DateTime Updated { get; set; }

        [XmlElement("entry")]
        public List<Item> Items { get; set; }
    }
}
