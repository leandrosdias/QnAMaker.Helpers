using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class KnowledgeBaseResult
    {
        [JsonProperty("operationState")]
        public OperationStateType OperationState { get; set; }
        [JsonProperty("createdTimestamp")]
        public DateTime CreatedTimestamp { get; set; }
        [JsonProperty("lastActionTimestamp")]
        public DateTime LastActionTimestamp { get; set; }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("operationId")]
        public string OperationId { get; set; }
        [JsonProperty("resourceLocation")]
        public string ResourceLocation { get; set; }
        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public KnowledgeBaseResult(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }

    public enum OperationStateType
    {
        Failed,
        NotStarted,
        Running,
        Succeeded
    }
}
