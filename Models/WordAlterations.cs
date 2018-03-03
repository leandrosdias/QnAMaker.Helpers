using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class WordAlterations
    {
        [JsonProperty("word")]
        public string Word { get; set; }
        [JsonProperty("alterations")]
        public List<string> Alterations { get; set; }
    }
}
