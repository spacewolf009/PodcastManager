using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace PodcastManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            FeedManager.LoadFeedDetails();
            int userChoice;
            while ((userChoice = Menu()) != 4)
            {
                switch (userChoice)
                {
                    case 1: AddPocast();
                        break;
                    case 2: Update();
                        break;
                    case 3: ListPodcasts();
                        break;
                    default: break;
                }
            }
            FeedManager.SaveFeedDetails();
        }

        static void AddPocast()
        {
            Console.WriteLine("URL:");
            string url = Console.ReadLine();
            Console.WriteLine("Name:");
            string name = Console.ReadLine();
            try
            {
                FeedManager.AddPodcast(url, name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred, podcast not added\n" + ex.Message + "\n" + ex.StackTrace);
            }
        }
        static int Menu()
        {
            int userChoice = 0;
            while (userChoice <= 0)
            {
                Console.WriteLine("1. Add new podcast\n2. Check for new content\n3. List Podcasts\n4. Quit");

                int.TryParse(Console.ReadLine(), out userChoice);
                if (userChoice > 4) userChoice = 0;
            }
            return userChoice;
        }

        public static void ListPodcasts()
        {
            foreach (PodcastFeed f in FeedManager.FeedList)
            {
                Console.WriteLine(f.ToString());
            }
        }
        public static void Update()
        {
            foreach (PodcastFeed f in FeedManager.FeedList)
            {
                f.CheckForUpdates();
            }
        }
    }
}
