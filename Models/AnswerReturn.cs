using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class AnswerReturn
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }
        [JsonProperty("questions")]
        public List<string> Questions { get; set; }
        [JsonProperty("score")]
        public decimal Score { get; set; }
    }
}
