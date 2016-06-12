using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PodcastManager.Properties;
using System.IO;
using System.Xml.Serialization;

namespace PodcastManager
{
    static class FeedManager
    { 
        static List<Feed> Feeds;

        public static IEnumerable<Feed> FeedList { get { return Feeds.AsReadOnly(); } }

        /// <summary>
        /// Load rss feed information from file
        /// </summary>
        public static void LoadFeedDetails()
        {
            Stream input = File.Create(Resources.DataPath + "data.xml");
            try
            {
                FeedManager.Feeds = (List<Feed>)new XmlSerializerFactory().CreateSerializer(typeof(List<Feed>)).Deserialize(input);
            }
            catch
            {
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
                new XmlSerializerFactory().CreateSerializer(typeof(List<Feed>)).Serialize(output, FeedManager.Feeds);
            }
            catch 
            {
            }
            finally
            {
                output.Close();
            }
        }

        public static void AddPodcast(string url, string name)
        {
            Feeds.Add(new Feed(url, name));
        }
    }
}
