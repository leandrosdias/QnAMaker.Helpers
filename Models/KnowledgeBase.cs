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
        [JsonProperty("qnaDocuments")]
        public QnADocument[] QnADocuments { get; set; }

    }
}
