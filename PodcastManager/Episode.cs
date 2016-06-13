using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using PodcastManager.Properties;
using System.Net;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace PodcastManager
{
    //[Serializable]
    //public class EpisodeList
    //{
    //    [XmlArray]
    //    public List<Episode> Episodes = new List<Episode>();
        
        

    //    public bool Add(Episode ep) {
    //        if (!Episodes.Contains(ep))
    //        {
    //            Episodes.Add(ep);
    //            return true;
    //        }
    //        return false;
    //    }
        
    //    public Episode indexer(int index) {
    //        return Episodes[index];
    //    }

    //    public int Count
    //    {
    //        get { return Episodes.Count; }
    //    }
    //}
    [Serializable]
    public class Episode
    {
        static string episodePathBase = Resources.DataPath + @"{0}\{1}";
        public String URL;
        public String EpisodeName;
        public String PodcastName;
        public DateTime PublicationDate;
        public bool Played = false;

        public bool IsDownloaded { get { return File.Exists(EpisodePath()); } }

        public Episode() { }

        public Episode(PodcastFeed p,string url, string name, DateTime date) {
            URL = url;
            EpisodeName = name;
            PublicationDate = date;
            PodcastName = p.PodcastName;
        }

        string EpisodePath()
        {
            return String.Format(episodePathBase, PodcastName, EpisodeName);
        }

        public void Download()
        {
            if (!IsDownloaded)
            {
                // Extract file extension from url and append to filename
                var splitUrl = URL.Split('.');
                var fileExtension = splitUrl[splitUrl.Length - 1];

                Console.WriteLine("Started downloading " + EpisodeName);
                WebClient downloader = new WebClient();
                downloader.DownloadFile(URL, EpisodePath() + '.' + fileExtension);
                Console.WriteLine("Finished downloading " + EpisodeName);
            }
            else
            {
                Console.WriteLine("Cancelled duplicate download of " + EpisodeName);
            }
        }

        public override string ToString()
        {
            return EpisodeName + " - " + PodcastName;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Episode))
                return false;
            else
            {
                Episode that = obj as Episode;
                return this.URL == that.URL;
            }
        }
    }
}
