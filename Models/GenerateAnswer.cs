using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class GenerateAnswer
    {
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
    }
}
