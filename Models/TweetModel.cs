using System;
using System.Xml.Serialization;

namespace Tweetcool.Models
{
    public class TweetModel
    {
        public string ID { get; set; }
        public string Poster { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public TweetModel() { }
    }
}