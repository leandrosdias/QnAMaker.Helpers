using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QnAMaker.Helpers.Models
{
    public class File
    {
        [JsonProperty("fileName")]
        public string FileName { get; set; }
        [JsonProperty("fileUri")]
        public string FileUri { get; set; }
    }
}
