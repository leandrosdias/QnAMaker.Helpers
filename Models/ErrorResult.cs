using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    class ErrorResult
    {
        [JsonProperty("error")]
        public ErrorResponse Error { get; set; }
    }
}
