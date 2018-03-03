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
        [JsonProperty("kbId")]
        public string KnowledgeBaseId { get; set; }
        [JsonProperty("dataExtractionResults")]
        public List<DataExtractionResults> DataExtractionResults { get; set; }
    }
}
