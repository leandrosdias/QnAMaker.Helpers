using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using QnAMaker.Helpers.Models;
using Newtonsoft.Json;

namespace QnAMaker.Helpers
{
    public class QnAMakerClient
    {
        public string KnowledgeId { get; set; }
        public string SubscriptionKey { get; set; }
        public string EndPoint { get; set; }
        public string Enviroment { get; set; }

        public QnAMakerClient(string knowledgeId, string subscriptionKey, string env = "test", string endPoint = "")
        {
            if (string.IsNullOrWhiteSpace(endPoint))
                endPoint = "https://westus.api.cognitive.microsoft.com/qnamaker/";

            KnowledgeId = knowledgeId;
            SubscriptionKey = subscriptionKey;
            EndPoint = endPoint;
            Enviroment = env;
        }

        /// <summary>
        /// Create a new Knowledge Base. SubscriptionKey and Name is required
        /// </summary>
        /// <returns>If error return in ErrorResponse</returns>
        public async Task<KnowledgeBaseResult> CreateKnowledgeBase(KnowledgeBase knowledgeBase)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new KnowledgeBaseResult(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            if (string.IsNullOrWhiteSpace(knowledgeBase.Name))
                return new KnowledgeBaseResult(new ErrorResponse(ErrorCodeType.FieldRequired, "Field 'name' is required"));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/knowledgebases/create";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(knowledgeBase), Encoding.UTF8,
                    "application/json");

                var response = await client.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<KnowledgeBaseResult>(jsonResponse);
            }
        }

        /// <summary>
        /// Delete Knowledge Base by Id
        /// </summary>
        /// <returns>If sucess return true</returns>
        public async Task<object> DeleteKnowledgeBase()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return new ErrorResponse(ErrorCodeType.KbNotFound, "KnowledgeId is empty");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/knowledgebases/" + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), uri))
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return true;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ErrorResult>(jsonResponse);
            }
        }

        /// <summary>
        /// Downloads all word alterations (synonyms) that have been automatically mined or added by the user.
        /// </summary>
        /// <returns>If Error return null</returns>
        public async Task<WordAlterationsResult> DownloadAlterations()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new WordAlterationsResult(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/alterations";

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WordAlterationsResult>(jsonResponse);
            }
        }

        /// <summary>
        /// Gets endpoint keys for an endpoint.
        /// </summary>
        /// <returns>>If error return in ErrorResponse</returns>
        public async Task<EndpointKeys> DownloadEndpointKeys()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new EndpointKeys(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/endpointkeys";

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EndpointKeys>(jsonResponse);
            }
        }

        /// <summary>
        /// Gets endpoint keys for an endpoint.
        /// </summary>
        /// <returns>>If error return in ErrorResponse</returns>
        public async Task<EndpointSettings> DownloadEndpointSettings()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new EndpointSettings(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/endpointSettings";

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EndpointSettings>(jsonResponse);
            }
        }

        /// <summary>
        /// Gets Knowledgebase details.
        /// </summary>
        /// <returns>>If error return in ErrorResponse</returns>
        public async Task<KnowledgeBaseDetails> GetKnowledgebaseDetails()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new KnowledgeBaseDetails(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return new KnowledgeBaseDetails(new ErrorResponse(ErrorCodeType.KbNotFound, "KnowledgeId is empty"));

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/knowledgebases/" + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<KnowledgeBaseDetails>(jsonResponse);
            }
        }

        /// <summary>
        /// Downloads all the data associated with the specified knowledge base.
        /// </summary>
        /// <returns>If Error return null, if sucess return string with data</returns>
        public async Task<KnowledgeBase> DownloadKnowlegdeBase()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new KnowledgeBase(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return new KnowledgeBase(new ErrorResponse(ErrorCodeType.KbNotFound, "KnowledgeId is empty"));

            var uri = EndPoint + "/v4.0/knowledgebases/" + KnowledgeId + "/" + Enviroment + "/qna";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                var knowledgeBase = JsonConvert.DeserializeObject<KnowledgeBase>(jsonResponse);

                return knowledgeBase;
            }
        }

        /// <summary>
        /// Get all knowledgebases for user.
        /// </summary>
        /// <returns>If Error return in ErrorResponse</returns>
        public async Task<KnowledgeBases> GetKnowledgeBases()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new KnowledgeBases(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            var uri = EndPoint + "/v4.0/knowledgebases/";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<KnowledgeBases>(jsonResponse); ;
            }
        }

        /// <summary>
        /// Returns the list of answers for the given question sorted in descending order of ranking score. Top is 1 by default
        /// </summary>
        /// <returns>If Error return null</returns>
        public async Task<AnswerReturnList> GenerateAnswer(string question, int top = 1)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return null;

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId + "/generateAnswer";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
            {
                var generateAnswer = new GenerateAnswer { Question = question, Top = top };
                request.Content = new StringContent(JsonConvert.SerializeObject(generateAnswer), Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AnswerReturnList>(jsonResponse);
            }
        }

        /// <summary>
        /// Publish all unpublished in the knowledgebase to the prod endpoint
        /// </summary>
        /// <returns>If sucess Error.Code is empty</returns>
        public async Task<object> PublishKnowlegdeBase()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return new ErrorResponse(ErrorCodeType.KbNotFound, "KnowledgeId is empty");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/knowledgebases/" + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return true;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
            }
        }

        /// <summary>
        /// Refresh keys of endpoint
        /// </summary>
        /// <returns>If error return in ErrorResponse</returns>
        public async Task<EndpointKeys> RefreshEndpointsKeys(string keyType)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new EndpointKeys(new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty"));

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/endpointkeys/" + keyType;

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri))
            {
                var response = await client.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<EndpointKeys>(jsonResponse);
            }
        }

        /// <summary>
        /// Replaces word alterations (synonyms) for the KB with the give records.
        /// </summary>
        /// <returns>If sucess return true. If error return in ErrorResponse</returns>
        public async Task<object> ReplaceAlterations(UpdateAlterations updateAlterations)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/alterations";

            using (var request = new HttpRequestMessage(new HttpMethod("PUT"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(updateAlterations), Encoding.UTF8, "application/json");


                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                    return true;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
            }
        }

        /// <summary>
        /// Add or delete QnA Pairs and / or URLs to an existing knowledge base.
        /// </summary>
        /// <returns>If sucess true. If error return ErrorResponse</returns>
        public async Task<object> ReplaceKnowledgeBase(KnowledgeBase updateKnowledgeBase)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return new ErrorResponse(ErrorCodeType.SubscriptionKeyNotFound, "SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return new ErrorResponse(ErrorCodeType.KbNotFound, "KnowledgeId is empty");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "/v4.0/knowledgebases/" + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("PUT"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(updateKnowledgeBase),
                    Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return true;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
            }
        }

        /// <summary>
        /// Train knowledge base.
        /// </summary>
        /// <returns>If Error return null</returns>
        public async Task<object> TrainKnowledgeBase(FeedbackRecords feedbackRecords)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return null; //ResultHelper.GetGenericError("SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return null; //ResultHelper.GetGenericError("KnowledgeId is empty");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId + "/train";

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(feedbackRecords),
                    Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return null; //ResultHelper.GetSucess("Train completed successfully");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return null; //ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(jsonResponse));
            }
        }
    }
}
