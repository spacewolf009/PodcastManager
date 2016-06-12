using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PodcastManager.Properties;
using System.Net;
using System.Xml.Serialization;

namespace PodcastManager
{
    [Serializable]
    public class EpisodeList
    {
        [XmlArray]
        public List<Episode> Episodes = new List<Episode>();
        public void Add(Episode ep) {
            Episodes.Add(ep);
        }
        
        public Episode indexer(int index) {
            return Episodes[index];
        }

        public int Count()
        {
            return Episodes.Count;
        }
    }
    [Serializable]
    public class Episode
    {
        static string episodePathBase = Resources.DataPath + @"{0}\{1}";
        String URL;
        String EpisodeName;
        PodcastFeed Podcast;
        DateTime PublicationDate;

        public bool IsDownloaded { get { return File.Exists(EpisodePath()); } }

        public Episode() { }

        public Episode(PodcastFeed p,string url, string name, DateTime date) {
            URL = url;
            EpisodeName = name;
            PublicationDate = date;
            Podcast = p;
        }

        string EpisodePath()
        {
            return String.Format(episodePathBase, Podcast.PodcastName, EpisodeName);
        }

        public void Download()
        {
            if (!IsDownloaded)
            {
                Console.WriteLine("Downloading " + EpisodeName);
                WebClient downloader = new WebClient();
                downloader.DownloadFile(URL, EpisodePath());
            }
            else
            {
                Console.WriteLine("Cancelled duplicate download of " + EpisodeName);
            }
        }
    }
}
