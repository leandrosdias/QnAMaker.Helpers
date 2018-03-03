using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class UpdateKnowledgeBase
    {
        [JsonProperty("add")]
        public UpdatePairs Add { get; set; }    
        [JsonProperty("delete")]
        public UpdatePairs Delete { get; set; }
        
    }
}
