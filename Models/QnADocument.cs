using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class QnADocument
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }
        [JsonProperty("source")]
        public string Source { get; set; }
        [JsonProperty("questions")]
        public string[] Questions { get; set; }
        [JsonProperty("metadata")]
        public Metadata[] Metadata { get; set; }
        [JsonProperty("alternateQuestions")]
        public string AlternateQuestions { get; set; }
        [JsonProperty("alternateQuestionClusters")]
        public object[] AlternateQuestionClusters { get; set; }
        [JsonProperty("changeStatus")]
        public string ChangeStatus { get; set; }
        [JsonProperty("kbId")]
        public string KbId { get; set; }
        [JsonProperty("context")]
        public Context Context { get; set; }
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
