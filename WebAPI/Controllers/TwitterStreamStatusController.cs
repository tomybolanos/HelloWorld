using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BAL.Abstract;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterStreamStatusController : ControllerBase
    {
        private readonly IStatusService statusService;

        public TwitterStreamStatusController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        [HttpGet]
        [Produces("application/json")]
        public IActionResult Get()
        {
            var status = statusService.GetStatus();
            var ret = new Models.StreamStatus();
            float seconds = (float) ((DateTimeOffset)status.endDateTime - (DateTimeOffset)status.startDateTime).TotalSeconds;
            float minutes = seconds / 60;
            float hours = minutes / 60;

            if (seconds != 0)
            {
                ret.TotalTweets = status.countTweets;
                ret.TweetsPerHour = (float)status.countTweets / hours;
                ret.TweetsPerMinute = (float)status.countTweets / minutes;
                ret.TweetsPerSecond = (float)status.countTweets / seconds;
                ret.PercentUrl = status.countUrl / status.countTweets * 100;
                ret.PercentPicture = status.countPhotos / status.countTweets * 100;
                ret.Percentemojis = status.countEmoji / status.countTweets * 100;
                ret.TopDomains = (from entry in status.domains
                                  orderby entry.Value
                                  descending
                                  select entry.Key).ToArray<string>
                          (
                          ).Take(5).ToList();
                ret.TopEmojis = (from entry in status.emoji
                                 orderby entry.Value
                                 descending
                                 select entry.Key).ToArray<string>
                          (
                          ).Take(5).ToList();


                ret.TopHashTags = (from entry in status.hashtags
                                 orderby entry.Value
                                 descending
                                 select entry.Key).ToArray<string>
                          (
                          ).Take(5).ToList();
            }

            return new OkObjectResult(ret);
        }
    }
}
