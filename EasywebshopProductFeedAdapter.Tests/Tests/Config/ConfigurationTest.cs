using Geta.GoogleProductFeed.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit;

namespace EasywebshopProductFeedAdapter.Tests
{
    public class ConfigurationTest
    {
        private readonly IConfigurationRoot _config;


        public ConfigurationTest()
        {
            _config = ConfigurationHelper.GetIConfigurationRoot();
        }

        [Fact]
        public void ShippingSerializesToListCorrectly()
        {
            var shippingList = new List<Shipping>();

            shippingList = _config.GetSection("Easywebshop:Shipping").Get<List<Shipping>>();

            Assert.Equal(2, shippingList.Count);

            var shippingLocationBE = shippingList[0];
            var shippingLocationNL = shippingList[1];

            Assert.Equal("BE", shippingLocationBE.Country);
            Assert.Equal("6.00 EUR", shippingLocationBE.Price);
            Assert.Equal("Standard", shippingLocationBE.Service);
            
            Assert.Equal("NL", shippingLocationNL.Country);
            Assert.Equal("7.00 EUR", shippingLocationNL.Price);
            Assert.Equal("Standard", shippingLocationNL.Service);
        }
    }
}
