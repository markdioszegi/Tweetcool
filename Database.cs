using System;
using System.IO;
using System.Collections.Generic;
using Tweetcool.Models;
using System.Xml.Serialization;

namespace Tweetcool
{
    public sealed class Database
    {
        static readonly string FilePath = "tweets.xml";
        static Database instance = null;
        public List<TweetModel> Tweets { get; set; }

        private Database()
        {
            Tweets = new List<TweetModel>();
            LoadFromFile();
        }

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }

        public void LoadFromFile()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<TweetModel>), new XmlRootAttribute("Tweets"));
            try
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                {
                    Tweets = (List<TweetModel>)xml.Deserialize(fs);
                }
            }
            catch (FileNotFoundException fnfe)
            {
                System.Console.WriteLine(fnfe.Message);
            }
        }

        public void SaveToFile()
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<TweetModel>), new XmlRootAttribute("Tweets"));
            using (FileStream fs = new FileStream(FilePath, FileMode.Create))
            {
                xml.Serialize(fs, Tweets);
            }
        }

        internal List<TweetModel> FilterTweets(int limit, int offset, string poster, DateTime date)
        {
            List<TweetModel> filteredTweets = new List<TweetModel>();

            for (int i = offset; i < Tweets.Count; i++)
            {
                if (i >= limit)
                    break;
                if (Tweets[i].Poster.Equals(poster, StringComparison.CurrentCultureIgnoreCase) && Tweets[i].Timestamp >= date)
                {
                    filteredTweets.Add(Tweets[i]);
                }
            }

            return filteredTweets;
        }
    }
}