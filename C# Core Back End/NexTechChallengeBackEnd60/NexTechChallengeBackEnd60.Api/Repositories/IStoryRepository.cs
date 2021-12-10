using NexTechChallengeBackEnd.Api.Entities;

namespace NexTechChallengeBackEnd.Api.Repositories
{
    public interface IStoryRepository
    {
        Dictionary<Int32, Story> GetNewestStories();
    }
}