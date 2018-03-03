using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class QnAPair
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
    }
}   
