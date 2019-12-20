using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Extensions;
using EasywebshopProductFeedAdapter.Util;
using Geta.GoogleProductFeed.Models;
using Microsoft.Extensions.Configuration;

namespace EasywebshopProductFeedAdapter.Services
{
    public class EasyWebshopFeedDeserializerService
    {
        private readonly string _feedUrl;
        private readonly string _fqdn;
        private readonly string _countryCode;
        private readonly string _brand;
        private readonly List<Shipping> _shipping;


        public EasyWebshopFeedDeserializerService(IConfiguration config)
        {
            _feedUrl = config.GetValue<string>("Feed_Url");
            _fqdn = config.GetValue<string>("Easywebshop:Url");
            _countryCode = config.GetValue<string>("Easywebshop:CountryCode");
            _brand = config.GetValue<string>("Easywebshop:Brand");
            _shipping = config.GetSection("Easywebshop:Shipping").Get<List<Shipping>>();

        }

        public Feed Deserialize(string inputString)
        {
            var xml = new XmlDocument();
            xml.LoadXml(inputString);

            var rootElement = xml.DocumentElement;

            var items = new List<Item>();

            var urlFormatter = new EasyWebshopUrlFormatter(_fqdn);

            foreach (XmlNode node in rootElement.ChildNodes)
            {
                var productName = node.GetChild("name") == null ? node.GetChild("code").InnerText : node.GetChild("name").InnerText;
                items.Add(new Item()
                {
                    Id = node.GetChild("code").InnerText,
                    Title = productName,
                    Description = node.GetChild("description") == null ?  "" : node.GetChild("description").InnerText,
                    Link = urlFormatter.Format(node.GetChild("category").GetChild("name").InnerText, productName),
                    ImageLink = node.GetChild("image")?.InnerText,
                    Availability = "in stock",
                    Price = ParsePrice(node.GetChild("price").InnerText),
                    Shipping = _shipping,
                    Brand = _brand
                });
            }

            //Discard entries with invalid price
            var validItems = items.Where(e => e.Price != "NaN").ToList();

            var feed = new Feed()
            {
                Items = validItems,
                Link = _feedUrl,
                Title = "Feed for Google Merchant center",
                Updated = DateTime.Parse(rootElement.FirstChild.GetChild("last_update")?.InnerText, CultureInfo.InvariantCulture)
            };
            //A note on the Updated property
            //EasyWebshop uses Universal Sortable ("u"): yyyy-MM-dd HH:mm:ssZ, so InvariantCulture is used just to be safe
            //EasyWebshop does not provide a "last updated" value, so the last_update property of the first product in the list is used
            //As the list is provided in order with the most recently modified product first, this should work just as well

            return feed;
        }

        private string ParsePrice(string price)
        {
            if (double.TryParse(price, NumberStyles.Number, CultureInfo.InvariantCulture, out _))
            {
                return price + " " + new RegionInfo(_countryCode).ISOCurrencySymbol; //Google expects ISO 4217 formatted currency
            } else {
                return "NaN";
            }
        }
    }
}
