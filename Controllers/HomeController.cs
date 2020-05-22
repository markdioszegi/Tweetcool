using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tweetcool.Models;

namespace Tweetcool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tweets()
        {
            return View(Database.Instance.Tweets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult FilterTweets(int limit, int offset, string poster, DateTime date)
        {
            /* System.Console.WriteLine("LIMIT IS: " + limit);
            System.Console.WriteLine("OFFSET IS: " + offset);
            System.Console.WriteLine("POSTER IS: " + poster);
            System.Console.WriteLine("DATE IS: " + date.ToShortDateString()); */

            return View("Tweets", Database.Instance.FilterTweets(limit, offset, poster, date));
        }

        [HttpPost]
        public IActionResult PostTweet(TweetModel tweet)
        {
            if (ModelState.IsValid)
            {
                bool isUnique = false;
                if (Database.Instance.Tweets.Count == 0)
                {
                    tweet.ID = Guid.NewGuid().ToString();
                    isUnique = true;
                }
                while (!isUnique)
                {
                    tweet.ID = Guid.NewGuid().ToString();
                    //Check if the generated ID already exists
                    foreach (var _tweet in Database.Instance.Tweets)
                    {
                        if (_tweet.ID != tweet.ID)
                        {
                            isUnique = true;
                        }
                    }
                }
                tweet.Timestamp = DateTime.Now;
            }
            //Debug
            System.Console.WriteLine("ID: " + tweet.ID);
            System.Console.WriteLine("Poster: " + tweet.Poster);
            System.Console.WriteLine("Content: " + tweet.Content);
            System.Console.WriteLine("Date: " + tweet.Timestamp);
            Database.Instance.Tweets.Add(tweet);
            Database.Instance.SaveToFile();

            Response.Cookies.Append("poster", tweet.Poster);
            return View("Index");
        }
    }
}
