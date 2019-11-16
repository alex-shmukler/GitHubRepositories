using Newtonsoft.Json;

namespace GitHubRepositories.Models
{
    public class Repository
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }
    }
}