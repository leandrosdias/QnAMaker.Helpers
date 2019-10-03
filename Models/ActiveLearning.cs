using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class ActiveLearning
    {
        [JsonProperty("enable")]
        public string Enable { get; set; }
    }
}
