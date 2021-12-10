using NexTechChallengeBackEnd.Api.Entities;
using System.Text.Json;

namespace NexTechChallengeBackEnd.Api.Repositories
{
    // class to communicate with Hacker News Feed API
    public class CommunicationClient
    {
        private readonly HttpClient httpClient;
        public CommunicationClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
            this.httpClient = httpClient;
        }

        // retrieve newest stories from hacker news feed using /newstories API call
        public async Task<List<Story>> GetNewestStories(Dictionary<Int32, Story> storyList)
        {
            if (storyList is null)
            {
                return new List<Story>();
            }

            HttpResponseMessage response = await httpClient.GetAsync("newstories.json");
            
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();

                List<Int32> storyIds = JsonSerializer.Deserialize<List<Int32>>(data);

                if (storyIds != null)
                {
                    for (int i = 0; i < storyIds.Count; i++)
                    {
                        int storyId = storyIds[i];
                        await StoreStory(storyId, storyList);
                    }
                }

                List<Story> stories = new();
                stories.AddRange(storyList.Values);
                return stories;
            }
            else
            {
                string msg = await response.Content.ReadAsStringAsync();
                throw new Exception(msg);
            }
        }

        // retrieve story details for provided id if not already stored
        public async Task<Story> StoreStory(Int32 id, Dictionary<Int32, Story> storyList)
        {
            if (id <= 0)
            {
                return new Story();
            }
            else if (storyList.ContainsKey(id))
            {
                return storyList[id];
            }
            else
            {
                HttpResponseMessage response = await httpClient.GetAsync(String.Format("item/{0}.json", id));
                
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();

                    Story result = JsonSerializer.Deserialize<Story>(data);

                    if (result != null)
                    {
                        storyList[id] = result;
                    }

                    return result;
                }
                else
                {
                    string msg = await response.Content.ReadAsStringAsync();
                    throw new Exception(msg);
                }
            }
        }
    }
}