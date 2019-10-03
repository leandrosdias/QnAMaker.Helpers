using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class EndpointKeys
    {
        [JsonProperty("primaryEndpointKey")]
        public string PrimaryEndpointKey { get; set; }
        [JsonProperty("secondaryEndpointKey")]
        public string SecondaryEndpointKey { get; set; }
        [JsonProperty("installedVersion")]
        public string InstalledVersion { get; set; }
        [JsonProperty("lastStableVersion")]
        public string LastStableVersion { get; set; }
        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public EndpointKeys(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
