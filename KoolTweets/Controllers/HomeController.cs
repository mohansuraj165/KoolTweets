using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using KoolTweets.Models;

namespace KoolTweets.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        
        public JsonResult GetAllTweets(DateTime start, DateTime end)
        {
            if(start>DateTime.Now || end>DateTime.Now)
                return Json(new { success = false, error = "Date is in the future" }, JsonRequestBehavior.AllowGet);
            if (start>=end)
                return Json(new { success = false, error = "Start date is greater or same as end date" }, JsonRequestBehavior.AllowGet);

            List<Tweet> tweetStr = new List<Tweet>();
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            tweetStr =GetTweets(start,end);
            watch.Stop();
            ViewBag.Time = watch.ElapsedMilliseconds;
            return Json(new { success = true, data = tweetStr }, JsonRequestBehavior.AllowGet);
        }

        public List<Tweet> GetTweets(DateTime start, DateTime end)
        {
            List<Tweet> tweets = new List<Tweet>();
            List<string> tweetStr = new List<string>();
            string URL = String.Format("https://badapi.iqvia.io/api/v1/Tweets?startDate={0}&endDate={1}",start,end);

            WebRequest request = WebRequest.Create(URL);
            request.Method = "GET";
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();

            string result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                tweets = JsonConvert.DeserializeObject<List<Tweet>>(result);
                sr.Close();
            }

            //foreach (Tweet t in tweets)
            //{
            //    tweetStr.Add(JsonConvert.SerializeObject(t));
            //}
            if (tweets.Count==100)
            {
                DateTime newStartDate = tweets[99].stamp;
                tweets.AddRange( GetTweets(newStartDate, end));
            }
            return tweets;
        }
    }
}