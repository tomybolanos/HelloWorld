using EmojiData;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using Tweetinvi;
using Tweetinvi.Streaming;
using Tweetinvi.Streams;
using Microsoft.Extensions.DependencyInjection;
using BAL.Abstract;
using BAL;
using DAL.DAO;



namespace ConsoleApp
{

    class Status
    {
        public int countTweets;
        public DateTimeOffset? startDateTime;
        public DateTimeOffset? endDateTime;
        public int countEmoji;
        public int countUrl;
        public int countPhotos;

        public Status()
        {
            countTweets = 0;
            startDateTime = null;
            endDateTime = null;
            countEmoji = 0;
            countUrl = 0;
            countPhotos = 0;
        }
    }
    class Program
    {

        private static ServiceCollection collection;
        private static ServiceProvider serviceProvider;
        private static void RegisterServices()
        {
            collection = new ServiceCollection();
            collection.AddScoped<IStatusService, StatusService>();
            collection.AddScoped<ITwitterService, TwitterService>();

            serviceProvider = collection.BuildServiceProvider();
        }
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            RegisterServices();
            ITwitterService TwitterService = serviceProvider.GetService<ITwitterService>();
            DAL.DAO.Status status = new DAL.DAO.Status();
            while (true)
            {
                // service -  like process
                Console.WriteLine("Reading Sample Stream....");
                await TwitterService.ReadTweets(status);
                Console.WriteLine("Connection Closed / Reconnecting.....");
            }
        }
    }
}
