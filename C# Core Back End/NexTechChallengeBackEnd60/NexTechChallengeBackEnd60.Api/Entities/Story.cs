namespace NexTechChallengeBackEnd.Api.Entities
{
    // model class representing news story attributes
    public class Story
    {
        public Story()
        {
            this.by = String.Empty;
            this.kids = new();
            this.title = String.Empty;
            this.type = String.Empty;
            this.url = String.Empty;
        }

        public string by { get; init;}

        public Int32 descendants { get; init;}

        public Int32 id { get; set;}
        
        public List<Int32> kids { get; init; }

        public Int32 score { get; init;}
        
        public Int64 time { get; init;}

        public string title { get; init;}
        
        public string type { get; init;}

        public string url { get; init;}
    }
}