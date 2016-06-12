using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
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

        //public EpisodeList Episodes = new EpisodeList();
        [XmlArray]
        public List<Episode> Episodes = new List<Episode>();

        public PodcastFeed()
        {
        }

        public PodcastFeed(string url, string name)
        {
            URL = url;
            PodcastName = name;
            LastUpdate = null;
            Directory.CreateDirectory(PodcastPath());
        }

        public void CheckForUpdates()
        {
            Console.WriteLine("Updating " + this.PodcastName + "...");
            XmlDocument doc = new XmlDocument();
            //try
            //{
                doc.Load(URL);
            //}
            //catch (exce
            
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
                if (Episodes.Contains(ep))
                    break;
                
                Episodes.Add(ep);

                //ep.Download();
                // If no episodes have been previously downloaded, stop after the most recent episode.
                // Otherwise download all until a previously downloaded episode is encountered.
                if (Episodes.Count == 1)
                    break;
            }
        }

        public  IEnumerable<Episode> DownloadQueue
        {
            get {return Episodes.Where(x => !x.IsDownloaded && !x.Played);}
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.PodcastName, this.URL);
        }

        public string PodcastPath()
        {
            return String.Format(podcastPathBase, this.PodcastName);
        }

        //public void SaveEpisodes() 
        //{
        //    Stream output = File.Create(PodcastPath() + "history.xml");
        //    try
        //    {
        //        XmlSerializer s = new XmlSerializerFactory().CreateSerializer(typeof(List<Episode>));
        //        s.Serialize(output, this.Episodes);

        //    }
        //    //catch (Exception e)
        //    //{
        //    //    throw e;
        //    //}
        //    finally
        //    {
        //        output.Close();
        //    }
        //}

        //public void LoadEpisodes() 
        //{
        //    if (File.Exists(PodcastPath() + "history.xml")) 
        //    {
        //        XmlReader input = XmlReader.Create(PodcastPath() + "history.xml");
        //        try
        //        {
        //            XmlSerializer s = new XmlSerializerFactory().CreateSerializer(typeof(List<Episode>));
        //            var d = s.Deserialize(input);
        //            Episodes = (List<Episode>)d;
        //        }
        //        catch
        //        {
        //            System.Diagnostics.Debug.WriteLine("Error during deserialization");
        //        }
        //        finally
        //        {
        //            input.Close();
        //        }
        //    }
        //}
    }
}
