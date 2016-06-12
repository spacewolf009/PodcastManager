using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using PodcastManager.Properties;

namespace PodcastManager
{
    [Serializable]
    public class PodcastFeed
    {
        static string podcastPathBase = Resources.DataPath + @"{0}\";
        public string URL;
        public DateTime? LastUpdate;
        public string PodcastName;

        public PodcastFeed()
        {
        }

        public PodcastFeed(string url, string name)
        {
            URL = url;
            PodcastName = name;
            LastUpdate = null;
            Directory.CreateDirectory(PodcastPath());
            File.Create(PodcastPath() + "history.xml");
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
                    string title = n.SelectSingleNode("title").InnerText;
                    string url = n.SelectSingleNode("enclosure").Attributes.GetNamedItem("url").Value;
                    string date = n.SelectSingleNode("pubDate").InnerText;
                    Console.WriteLine(title);
                    Console.WriteLine(url);
                    Console.WriteLine(date);
                    Episode ep = new Episode(this, url, title, DateTime.Parse(date));
                    //if (!ep.IsDownloaded)
                        ep.Download();
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

        public string PodcastPath()
        {
            return String.Format(podcastPathBase, this.PodcastName);
        }
    }
}
