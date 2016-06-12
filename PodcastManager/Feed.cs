using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace PodcastManager
{
    [Serializable]
    public class Feed
    {
        public string URL;
        public DateTime? LastUpdate;
        public string PodcastName;

        public Feed()
        {
        }

        public Feed(string url, string name)
        {
            URL = url;
            PodcastName = name;
            LastUpdate = null;
        }

        public void CheckForUpdates()
        {
            Console.WriteLine("Updating " + this.PodcastName + "...");
            XmlDocument doc = new XmlDocument();
            doc.Load(URL);
            {
                var channel = doc.SelectSingleNode("rss").SelectSingleNode("channel");
                var items = channel.SelectNodes("item");
                foreach (XmlNode n in items)
                {
                    Console.WriteLine(n.SelectSingleNode("title").InnerText);
                    Console.WriteLine(n.SelectSingleNode("enclosure").Attributes.GetNamedItem("url").Value);
                    Console.WriteLine(n.SelectSingleNode("pubDate").InnerText);
                    break;
                }
            }
        }

        public void DownloadNewContent()
        {
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.PodcastName, this.URL);
        }
    }
}
