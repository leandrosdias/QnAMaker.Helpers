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
        public CreateKbInput Add { get; set; }
        [JsonProperty("delete")]
        public DeleteKbContents Delete { get; set; }
        [JsonProperty("update")]
        public UpdateKbContents Update { get; set; }

        public UpdateKnowledgeBase()
        {
            Add = new CreateKbInput();
            Update = new UpdateKbContents();
            Delete = new DeleteKbContents();
        }
    }

    public class CreateKbInput
    {
        [JsonProperty("qnaDocuments", NullValueHandling = NullValueHandling.Ignore)]
        public List<QnADocument> QnADocuments { get; set; }
        [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore)]
        public List<File> Files { get; set; }
        [JsonProperty("urls", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Urls { get; set; }
        public CreateKbInput()
        {
            Urls = new List<string>();
        }
    }

    public class DeleteKbContents
    {
        [JsonProperty("ids", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> Ids { get; set; }
        [JsonProperty("sources", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Sources { get; set; }
    }

    public class UpdateKbContents
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        [JsonProperty("urls", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Urls { get; set; }
        [JsonProperty("qnaDocuments", NullValueHandling = NullValueHandling.Ignore)]
        public List<QnADocument> QnADocuments { get; set; }
    }
}
