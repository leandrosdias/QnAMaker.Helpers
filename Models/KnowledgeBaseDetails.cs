using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace QnAMaker.Helpers.Models
{
    public class KnowledgeBaseDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("hostName")]
        public string HostName { get; set; }
        [JsonProperty("lastAccessedTimestamp")]
        public DateTime LastAccessedTimestamp { get; set; }
        [JsonProperty("lastChangedTimestamp")]
        public DateTime LastChangedTimestamp { get; set; }
        [JsonProperty("lastPublishedTimestamp")]
        public DateTime LastPublishedTimestamp { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("urls")]
        public List<string> Urls { get; set; }
        [JsonProperty("sources")]
        public List<string> Sources { get; set; }
        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public KnowledgeBaseDetails(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }

}

