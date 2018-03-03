using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class UpdateAlterations
    {
        [JsonProperty("add")]
        public List<WordAlterations> Add { get; set; }
        [JsonProperty("delete")]
        public List<WordAlterations> Delete { get; set; }
    }
}
