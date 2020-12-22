using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class StreamStatus
    {
        public int TotalTweets { get; set; }
        public float TweetsPerSecond { get; set; }
        public float TweetsPerMinute { get; set; }
        public float TweetsPerHour { get; set; }
        public List<string> TopEmojis { get; set; }
        public float Percentemojis { get; set; }
        public List<string> TopHashTags { get; set; }
        public float PercentUrl { get; set; }
        public float PercentPicture { get; set; }
        public List<string> TopDomains { get; set; }




    }
}
