using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class KnowledgeBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("qnaDocuments")]
        public QnADocument[] QnADocuments { get; set; }
        [JsonProperty("urls")] 
        public List<string> Urls { get; set; }
        [JsonProperty("files")]
        public List<File> Files { get; set; }
        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public KnowledgeBase(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
