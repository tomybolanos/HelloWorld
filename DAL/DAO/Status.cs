using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DAO
{
    public class Status
    {
        public int countTweets { get; set; }
        public DateTimeOffset? startDateTime { get; set; }
        public DateTimeOffset? endDateTime { get; set; }
        public int countEmoji { get; set; }
        public int countUrl { get; set; }
        public int countPhotos { get; set; }

        public Dictionary<string, int> hashtags { get; set; }
        public Dictionary<string, int> domains { get; set; }
        public Dictionary<string, int> emoji { get; set; }



        public Status()
        {
            countTweets = 0;
            startDateTime = null;
            endDateTime = null;
            countEmoji = 0;
            countUrl = 0;
            countPhotos = 0;

            hashtags = new Dictionary<string, int>();
            domains = new Dictionary<string, int>();
            emoji = new Dictionary<string, int>();
        }
    }

    
}
