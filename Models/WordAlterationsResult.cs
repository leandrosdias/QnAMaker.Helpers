using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace QnAMaker.Helpers.Models
{
    public class WordAlterationsResult
    {
        [JsonProperty("wordAlterations")]
        public List<WordAlterations> WordAlterations { get; set; }

        [JsonProperty("error")]
        public ErrorResponse ErrorResponse { get; set; }

        public WordAlterationsResult(ErrorResponse errorResponse)
        {
            ErrorResponse = errorResponse;
        }
    }
}
