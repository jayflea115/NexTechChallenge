using NexTechChallengeBackEnd.Api.Repositories;
using NexTechChallengeBackEnd.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace NexTechChallengeBackEnd.Api.Controllers
{
    // .NET Core RESTful API methods
    [ApiController]
    [Route("[controller]")]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryRepository repository;
        private readonly CommunicationClient httpClient;

        public StoriesController(IStoryRepository repository, CommunicationClient httpClient)
        {
            this.repository = repository;
            this.httpClient = httpClient;
        }

        // GET /Stories
        [HttpGet]
        public Task<List<Story>> GetNewestStories()
        {
            return this.httpClient.GetNewestStories(this.repository.GetNewestStories());
        }
    }
}