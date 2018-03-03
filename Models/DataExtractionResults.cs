using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class DataExtractionResults
    {
        [JsonProperty("sourceType")]
        public string SourceType { get; set; }
        [JsonProperty("extractionStatusCode")]
        public string ExtractionStatusCode { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }  
    }
}
