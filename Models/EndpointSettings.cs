using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class EndpointSettings
    {
        [JsonProperty("activeLearning")]
        public ActiveLearning ActiveLearning { get; set; }
        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public EndpointSettings(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
