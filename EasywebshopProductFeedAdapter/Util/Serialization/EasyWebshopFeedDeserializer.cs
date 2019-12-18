using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Extensions;
using Geta.GoogleProductFeed.Models;

namespace EasywebshopProductFeedAdapter.Util.Serialization
{
    public class EasyWebshopFeedDeserializer
    {
        private readonly string _feedUrl;
        private readonly string _fqdn;
        private readonly string _culture;


        public EasyWebshopFeedDeserializer(string feedurl, string fqdn, string culture)
        {
            _feedUrl = feedurl;
            _fqdn = fqdn;
            _culture = culture;
        }

        public Feed Deserialize(string inputString)
        {
            var xml = new XmlDocument();
            xml.LoadXml(inputString);

            var rootElement = xml.DocumentElement;

            var entries = new List<Entry>();

            var urlFormatter = new EasyWebshopUrlFormatter(_fqdn);

            foreach (XmlNode node in rootElement.ChildNodes)
            {
                entries.Add(new Entry()
                {
                    Id = node.GetChild("code").Value,
                    Title = node.GetChild("name").Value,
                    Description = node.GetChild("description").Value,
                    Link = urlFormatter.Format(node.GetChild("category").GetChild("name").Value, node.GetChild("code").Value),
                    ImageLink = node.GetChild("image").Value,
                    Availability = "in stock",
                    Price = node.GetChild("price").Value + new RegionInfo(_culture).ISOCurrencySymbol, //Google expects ISO 4217 formatted currency
                    Shipping = new List<Shipping>() {
                        new Shipping() {
                            Country = null,
                            Service = null,
                            Price = null
                        }
                    },
                    Brand = null
                });
            }

            var feed = new Feed()
            {
                Entries = entries,
                Link = _feedUrl,
                Title = "Feed for Google Merchant center",
                Updated = DateTime.Parse(rootElement.FirstChild.GetChild("last_update")?.Value, CultureInfo.InvariantCulture)
            };
            //A note on the Updated property
            //EasyWebshop uses Universal Sortable ("u"): yyyy-MM-dd HH:mm:ssZ, so InvariantCulture is used just to be safe
            //EasyWebshop does not provide a "last updated" value, so the last_update property of the first product in the list is used
            //As the list is provided in order with the most recently modified product first, this should work just as well

            return feed;
        }
    }
}
