﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using EasywebshopProductFeedAdapter.Domain.Feeds;
using EasywebshopProductFeedAdapter.Extensions;
using Geta.GoogleProductFeed.Models;

namespace EasywebshopProductFeedAdapter.Services
{
    public class FeedWriterService : IFeedWriterService
    {
        public void Write(Feed feed)
        {
            var syndicationFeed = MakeSyndicationFeed(feed);
            WriteFeed(syndicationFeed);
        }

        private SyndicationFeed MakeSyndicationFeed(Feed feed)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<Item>));
            var xmlns = new XmlSerializerNamespaces();
            xmlns.Add("g", "http://base.google.com/ns/1.0");

            SyndicationFeed syndicationFeed = new SyndicationFeed();

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    xmlSerializer.Serialize(writer, feed.Items, xmlns);
                } catch (InvalidOperationException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                XmlDocument xmlDocument = new XmlDocument();

                try
                {
                    xmlDocument.LoadXml(writer.ToString());

                } catch (XmlException e)
                {
                    Debug.WriteLine(e.ToString());
                }

                using (XmlReader xmlReader = new XmlNodeReader(xmlDocument.DocumentElement))
                {
                    bool canRead = xmlReader.Read();
                    while (canRead)
                    {
                        if ((xmlReader.Name == "item") && xmlReader.IsStartElement())
                        {
                            string outerxml = xmlReader.ReadOuterXml();
                            canRead = (outerxml != string.Empty);

                            syndicationFeed.ElementExtensions.Add(xmlReader);
                        }
                        else
                        {
                            canRead = xmlReader.Read();
                        }
                    }
                }
            }

            //Add basic feed information
            syndicationFeed.Title = new TextSyndicationContent(feed.Title);
            syndicationFeed.Description = new TextSyndicationContent("Google Merchant Centre Feed, Generated by EasywebshopProductFeedAdapter");
            syndicationFeed.Generator = "EasywebshopProductFeedAdapter";
            syndicationFeed.LastUpdatedTime = feed.Updated;

            return syndicationFeed;
        }

        private void WriteFeed(SyndicationFeed syndicationFeed)
        {
            using (var xmlWriter = XmlWriter.Create(@"wwwroot\RSS\rss.xml"))
            {
                syndicationFeed.SaveAsRss20(xmlWriter);
            }
        }
    }
}
