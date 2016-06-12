﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PodcastManager.Properties;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace PodcastManager
{
    [Serializable]
    public class FeedList {
        [XmlArray]
        public List<PodcastFeed> Feeds = new List<PodcastFeed>();
    }
    static class FeedManager
    {

        static FeedList Feeds = new FeedList();

        public static IEnumerable<PodcastFeed> FeedList { get { return Feeds.Feeds.AsReadOnly(); } }

        /// <summary>
        /// Load rss feed information from file
        /// </summary>
        public static void LoadFeedDetails()
        {
            XmlReader input = XmlReader.Create(Resources.DataPath + "data.xml");
            try
            {
                XmlSerializer s = new XmlSerializerFactory().CreateSerializer(typeof(FeedList));
                var d = s.Deserialize(input);
                Feeds = (FeedList)d;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Error during deserialization");
            }
            finally
            {
                input.Close();
            }
        }

        /// <summary>
        /// Save rss feed information to file
        /// </summary>
        public static void SaveFeedDetails()
        {
            Stream output = File.Create(Resources.DataPath + "data.xml");
            try
            {
                XmlSerializer s = new XmlSerializerFactory().CreateSerializer(typeof(FeedList));
                s.Serialize(output, Feeds);
                
            }
            //catch (Exception e)
            //{
            //    throw e;
            //}
            finally
            {
                output.Close();
            }
        }

        public static void AddPodcast(string url, string name)
        {
            Feeds.Feeds.Add(new PodcastFeed(url, name));
        }
    }
}
