using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class Result
    {
        public bool Sucess { get; set; }
        [JsonProperty("error")]
        public Error Error { get; set; }
    }
}
