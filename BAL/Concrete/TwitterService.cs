using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using DAL;
using DAL.DAO;
using System.Text.RegularExpressions;
using Tweetinvi;
using System.Linq;
using RestSharp;
using EmojiData;


namespace BAL
{
    public class TwitterService : Abstract.ITwitterService
    {
        private readonly string bearerToken;
        private readonly string key;
        private readonly string secret;



        private const string url = "https://api.twitter.com/2/tweets/sample/stream";

        private Abstract.IStatusService StatusService;

        public TwitterService(Abstract.IStatusService statusService)
        {
            bearerToken = ConfigurationManager.AppSettings["bearerToken"];
            key = ConfigurationManager.AppSettings["key"];
            secret = ConfigurationManager.AppSettings["secret"];
            StatusService = statusService;
        }

        public async System.Threading.Tasks.Task ReadTweets(Status status)
        {
            try
            {
                
                // Application client
                var appClient = new TwitterClient(key, secret, bearerToken);

                appClient.Config.TweetMode = TweetMode.Extended;

                var sampleStreamV2 = appClient.StreamsV2.CreateSampleStream();


                sampleStreamV2.TweetReceived += (sender, args) =>
                {
                    if (args.Tweet != null)
                    {
                        
                        status.countTweets++;

                        // calculate totals.
                        //Console.WriteLine("Count: "  + data.Count);
                        // date



                        if (status.startDateTime == null || args.Tweet.CreatedAt < status.startDateTime)
                            status.startDateTime = args.Tweet.CreatedAt;
                        if (status.endDateTime == null || args.Tweet.CreatedAt > status.endDateTime)
                            status.endDateTime = args.Tweet.CreatedAt;


                        // emojis
                        var matches = Emoji.EmojiRegex.Matches(args.Tweet.Text);
                        var l = matches.Cast<Match>().Select(match => match.Value).ToList();

                        if (l.Count > 0)
                        {
                            status.countEmoji++;
                            foreach (var em in l)
                            {
                                if (status.emoji.ContainsKey(em))
                                    status.emoji[em]++;
                                else
                                    status.emoji.Add(em, 1);
                            }
                        }


                        if (args.Tweet.Entities != null)
                        {
                            if (args.Tweet.Entities.Hashtags != null)
                            {
                                foreach (var hs in args.Tweet.Entities.Hashtags)
                                {
                                    if (status.hashtags.ContainsKey(hs.Tag))
                                        status.hashtags[hs.Tag]++;
                                    else
                                        status.hashtags[hs.Tag] = 1;
                                }
                            }


                            if (args.Tweet.Entities.Urls != null)
                            {
                                status.countUrl++;
                                bool containsPicture = false;
                                foreach (var url in args.Tweet.Entities.Urls)
                                {
                                    Uri Uri = new Uri(url.ExpandedUrl);
                                    var host = Uri.Host;
                                    if (!containsPicture && (url.ExpandedUrl.Contains("instagram.com") || url.ExpandedUrl.Contains("pic.twitter.com")))
                                    {
                                        containsPicture = true;
                                        status.countPhotos++;
                                    }

                                    if (status.domains.ContainsKey(host))
                                        status.domains[host]++;
                                    else
                                        status.domains[host] = 1;
                                }
                            }
                        }
                        StatusService.SaveStatus(status);
                    }
                };

                await sampleStreamV2.StartAsync();

            }
            catch (Exception e)
            {
                // Manage Exception. Usually the end of the steam
            }

        }
    }
}
