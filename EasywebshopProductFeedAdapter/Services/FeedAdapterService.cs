﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using EasywebshopProductFeedAdapter.Domain.Feeds;

namespace EasywebshopProductFeedAdapter.Services
{
    public class FeedAdapterService
    {
        public Feed AdaptEasyWebshopToGoogleMerchant(Feed feed)
        {
            var syndicationFeed = new SyndicationFeed();

            // Add basic information
            syndicationFeed.Title = new TextSyndicationContent("Google Merchant Centre Feed");
            syndicationFeed.Description = new TextSyndicationContent("Google Merchant Centre Feed, Generated by EasywebshopProductFeedAdapter");
            syndicationFeed.Generator = "EasywebshopProductFeedAdapter";
            syndicationFeed.LastUpdatedTime = DateTime.UtcNow;

            //return feed;
            throw new NotImplementedException();
            //TODO
        }
    }
}
