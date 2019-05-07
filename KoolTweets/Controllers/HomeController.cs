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
        const int MAX_RECORDS = 100;
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gett all tweets with time stamp falling in range start and end date
        /// Validates input parameters
        /// Eliminates duplicate entries
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Json object with data and/or appropriate message</returns>
        public JsonResult GetAllTweets(DateTime start, DateTime end)
        {
            if (start > DateTime.Now || end > DateTime.Now)
                return Json(new { success = false, error = "Date is in the future" }, JsonRequestBehavior.AllowGet);
            if (start >= end)
                return Json(new { success = false, error = "Start date is greater or same as end date" }, JsonRequestBehavior.AllowGet);

            List<Tweet> resultTweets = new List<Tweet>();
            Dictionary<string, bool> dict = new Dictionary<string, bool>();
            List<Tweet> tweets = new List<Tweet>();

            tweets = GetTweets(start, end);

            bool isMoreTweets = false;
            if (tweets.Count == MAX_RECORDS)
                isMoreTweets = true;

            if (tweets.Count == 0)
            {
                return Json(new { success = true, data = resultTweets, isMoreTweets = false }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime greatestTime = tweets[0].stamp;
                foreach (Tweet t in tweets)
                {
                    if (t.stamp > greatestTime)
                        greatestTime = t.stamp;
                    if (!dict.ContainsKey(t.id.Trim()))
                    {
                        dict[t.id.Trim()] = true;
                        if (t.stamp > start)
                            resultTweets.Add(t);
                    }
                }

                return Json(new { success = true, data = resultTweets, isMoreTweets, newStart = greatestTime.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Function responsible for making the actual API call to get tweets
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>List of Tweet objects</returns>
        public List<Tweet> GetTweets(DateTime start, DateTime end)
        {
            List<Tweet> tweets = new List<Tweet>();
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
            
            return tweets;
        }
    }
}