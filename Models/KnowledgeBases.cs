using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnAMaker.Helpers.Models
{
    public class KnowledgeBases
    {
        [JsonProperty("knowledgebases")]
        public KnowledgeBase[] KnowledgeBaseList { get; set; }
        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public KnowledgeBases(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
