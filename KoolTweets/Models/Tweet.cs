﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KoolTweets.Models
{
    public class Tweet
    {
        public string id { get; set; }
        public string stamp { get; set; }
        public string text { get; set; }
    }
}