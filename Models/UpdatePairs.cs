using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class UpdatePairs
    {
        [JsonProperty("qnaPairs")]
        public List<QnAPair> Pairs { get; set; }
        [JsonProperty("urls")]
        public List<string> Urls { get; set; }
    }
}
