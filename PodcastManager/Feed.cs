using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        void CheckForUpdates()
        {
        }

        void DownloadNewContent()
        {
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.PodcastName, this.URL);
        }
    }
}
