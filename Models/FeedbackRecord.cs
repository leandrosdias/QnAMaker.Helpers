using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class FeedbackRecord
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("userQuestion")]
        public string UserQuestion { get; set; }
        [JsonProperty("kbQuestion")]
        public string KnowledgeBaseQuestion { get; set; }
        [JsonProperty("kbAnswer")]
        public string KnowledgeBaseAnswer { get; set; }
    }
}
