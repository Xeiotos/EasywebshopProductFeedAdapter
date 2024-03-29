﻿using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Extensions;
using EasywebshopProductFeedAdapter.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Xunit;

namespace EasywebshopProductFeedAdapter.Tests.Tests.Serialization
{
    public class DeserializerServiceTest
    {
        private readonly XmlDocument _xmlDocument;
        private readonly EasyWebshopFeedDeserializerService _deserializerService;

        public DeserializerServiceTest()
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load("Resources/productlist.xml");

            _deserializerService = new EasyWebshopFeedDeserializerService(ConfigurationHelper.GetIConfigurationRoot());
        }

        [Fact]
        public void DocumentElementIsProductList()
        {
            string name = _xmlDocument.DocumentElement.Name;
            Assert.Equal("productlist", name);
        }

        [Fact]
        public void DocumentElementFirstChildIsProduct()
        {
            string name = _xmlDocument.DocumentElement.FirstChild.Name;
            Assert.Equal("product", name);
        }

        [Fact]
        public void ExtensionMethodGetChildGetsCorrectChild()
        {
            string name = _xmlDocument.DocumentElement.FirstChild.GetChild("code").Name;
            Assert.Equal("code", name);
        }

        [Fact]
        public void ExtensionMethodGetChildGetsCorrectValue()
        {
            string value = _xmlDocument.DocumentElement.FirstChild.GetChild("code").InnerText;
            Assert.Equal("Gegraveerde box", value);
        }

        [Fact]
        public void AllProductsAreDeserialized()
        {
            Feed feed = _deserializerService.Deserialize(_xmlDocument.InnerXml);

            Assert.Equal(5, feed.Items.Count);
        }

        [Fact]
        public void ProductsAreDeserializedCorrectly()
        {
            Feed feed = _deserializerService.Deserialize(_xmlDocument.InnerXml);

            var firstItem = feed.Items[0];

            Assert.Equal("Gegraveerde box", firstItem.Id);
            Assert.Equal("https://www.ewimg.com/shops/ecocards/gegraveerde-box.jpg", firstItem.ImageLink);
            Assert.Equal("5.5 EUR", firstItem.Price);
            Assert.Equal("Gegraveerde gesloten box (hol), gegraveerd op 5 zijden. Andere afmetingen en opdruk mogelijk.&nbsp; Personaliseren kan op aanvraag, prijs: +€8 (per ontwerp)", firstItem.Description);
            Assert.Equal("in stock", firstItem.Availability);
            Assert.Equal("Gegraveerde box", firstItem.Title);
        }

        [Fact]
        public void ShippingIsAppendedCorrectly()
        {
            Feed feed = _deserializerService.Deserialize(_xmlDocument.InnerXml);

            var firstItemShipping = feed.Items[0].Shipping;
            var shippingBE = feed.Items[0].Shipping[0];

            Assert.Equal(2, firstItemShipping.Count);
            Assert.Equal("BE", shippingBE.Country);
            Assert.Equal("Standard", shippingBE.Service);
            Assert.Equal("6.00 EUR", shippingBE.Price);
        }
    }
}
