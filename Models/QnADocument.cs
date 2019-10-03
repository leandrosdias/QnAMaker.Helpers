using Newtonsoft.Json;
using System.Collections.Generic;

namespace QnAMaker.Helpers.Models
{
    public class QnADocument
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int Id { get; set; }
        [JsonProperty("answer", NullValueHandling = NullValueHandling.Ignore)]
        public string Answer { get; set; }
        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public string Source { get; set; }
        [JsonProperty("questions", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Questions { get; set; }
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public List<Metadata> Metadata { get; set; }
        [JsonProperty("alternateQuestions", NullValueHandling = NullValueHandling.Ignore)]
        public string AlternateQuestions { get; set; }
        [JsonProperty("alternateQuestionClusters", NullValueHandling = NullValueHandling.Ignore)]
        public object[] AlternateQuestionClusters { get; set; }
        [JsonProperty("changeStatus", NullValueHandling = NullValueHandling.Ignore)]
        public string ChangeStatus { get; set; }
        [JsonProperty("kbId", NullValueHandling = NullValueHandling.Ignore)]
        public string KbId { get; set; }
        [JsonProperty("context", NullValueHandling = NullValueHandling.Ignore)]
        public Context Context { get; set; }
        public QnADocument()
        {
            Metadata = new List<Metadata>();
        }
    }

    public class Context
    {
        [JsonProperty("isContextOnly")]
        public bool IsContextOnly { get; set; }
        [JsonProperty("prompts")]
        public object[] Prompts { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("value")]
        public string Value { get; set; }
    }

}
