using NexTechChallengeBackEnd.Api.Entities;

namespace NexTechChallengeBackEnd.Api.Repositories
{
     // cache class storing newest stories from Hacker News Feed
   public class StoryRepository : IStoryRepository
    {
        // cache interval in minutes
        public const int CACHE_EXPIRATION_INTERVAL = 20;

        private DateTime lastRetrieval = DateTime.MinValue;

        private Dictionary<Int32, Story> newestStories = new();

        public Dictionary<Int32, Story> GetNewestStories()
        {
            // clear newest stories using the cache expiration interval
            if (this.isExpired())
            {
                lastRetrieval = DateTime.Now;
                this.newestStories = new();
            }

            return this.newestStories;
        }

        private Boolean isExpired()
        {
            return DateTime.Now.Subtract(lastRetrieval).TotalMinutes > CACHE_EXPIRATION_INTERVAL;
        }
    }
}