using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using QnAMaker.Helpers.Models;
using Newtonsoft.Json;
using QnAMaker.Helpers.Helpers;
using System.Net;
using System.IO;

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
        /// <returns>If sucess set KnowledgeBaseId and return Erro.Code empty</returns>
        public async Task<Result> CreateKnowledgeBase(KnowledgeBase knowledgeBase)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return ResultHelper.GetGenericError("SubscriptionKey is empty");

            //if (string.IsNullOrWhiteSpace(knowledgeBase.Name))
            //    return ResultHelper.GetGenericError("Field 'name' is required");

            //if (knowledgeBase.QnAPairs != null && knowledgeBase.QnAPairs.Count > 1000)
            //    return ResultHelper.GetGenericError("Max 1000 Q-A pair allowed per request");

            //if (knowledgeBase.Urls != null && knowledgeBase.Urls.Count > 5)
            //    return ResultHelper.GetGenericError("Max 5 urls allowed per request");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + "create";

            using (var request = new HttpRequestMessage(new HttpMethod("POST"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(knowledgeBase), Encoding.UTF8,
                    "application/json");

                var response = await client.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(jsonResponse));

                var knowledgeResult = JsonConvert.DeserializeObject<KnowledgeBaseResult>(jsonResponse);
                KnowledgeId = knowledgeResult.KnowledgeBaseId;
                return ResultHelper.GetSucess("Knowledge base created successfully");
            }
        }

        /// <summary>
        /// Delete Knowledge Base by Id
        /// </summary>
        /// <returns>If sucess return Erro.Code empty</returns>
        public async Task<Result> DeleteKnowledgeBase()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return ResultHelper.GetGenericError("SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return ResultHelper.GetGenericError("KnowledgeId is empty");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("DELETE"), uri))
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return ResultHelper.GetSucess("Knowledge base deleted successfully");

                var responseContent = await response.Content.ReadAsStringAsync();
                return ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(responseContent));
            }
        }

        /// <summary>
        /// Downloads all word alterations (synonyms) that have been automatically mined or added by the user.
        /// </summary>
        /// <returns>If Error return null</returns>
        public async Task<WordAlterationsResult> DownloadAlterations()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return null;

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return null;

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId + "/downloadAlterations";

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WordAlterationsResult>(jsonResponse);
            }
        }

        /// <summary>
        /// Downloads all the data associated with the specified knowledge base.
        /// </summary>
        /// <returns>If Error return null, if sucess return string with data</returns>
        public async Task<KnowledgeBase> DownloadKnowlegdeBase()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return null;

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return null;

            var uri = EndPoint + "/v4.0/knowledgebases/" + KnowledgeId + "/" + Enviroment + "/qna";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), uri))
            {
                var response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                    return null;


                var jsonResponse = await response.Content.ReadAsStringAsync();
                var knowledgeBase = JsonConvert.DeserializeObject<KnowledgeBase>(jsonResponse);

                return knowledgeBase;
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
        public async Task<Result> PublishKnowlegdeBase()
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return ResultHelper.GetGenericError("SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return ResultHelper.GetGenericError("KnowledgeId is empty");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("PUT"), uri))
            {
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return ResultHelper.GetSucess("Knowledge Base published successfully");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(jsonResponse));
            }
        }

        /// <summary>
        /// Replaces word alterations (synonyms) for the KB with the give records.
        /// </summary>
        /// <returns>If sucess Error.Code is empty</returns>
        public async Task<Result> UpdateAlterations(UpdateAlterations updateAlterations)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return ResultHelper.GetGenericError("SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return ResultHelper.GetGenericError("KnowledgeId is empty");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId + "/updateAlterations";

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(updateAlterations), Encoding.UTF8, "application/json");


                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return ResultHelper.GetSucess("Process completed successfully");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(jsonResponse));
            }
        }

        /// <summary>
        /// Add or delete QnA Pairs and / or URLs to an existing knowledge base.
        /// </summary>
        /// <returns>If sucess Error.Code is empty</returns>
        public async Task<Result> UpdateKnowledgeBase(UpdateKnowledgeBase updateKnowledgeBase)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return ResultHelper.GetGenericError("SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return ResultHelper.GetGenericError("KnowledgeId is empty");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId;

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(updateKnowledgeBase),
                    Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return ResultHelper.GetSucess("Update completed successfully");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(jsonResponse));
            }
        }

        /// <summary>
        /// Train knowledge base.
        /// </summary>
        /// <returns>If Error return null</returns>
        public async Task<Result> TrainKnowledgeBase(FeedbackRecords feedbackRecords)
        {
            if (string.IsNullOrWhiteSpace(SubscriptionKey))
                return ResultHelper.GetGenericError("SubscriptionKey is empty");

            if (string.IsNullOrWhiteSpace(KnowledgeId))
                return ResultHelper.GetGenericError("KnowledgeId is empty");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            var uri = EndPoint + KnowledgeId + "/train";

            using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), uri))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(feedbackRecords),
                    Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return ResultHelper.GetSucess("Train completed successfully");

                var jsonResponse = await response.Content.ReadAsStringAsync();
                return ResultHelper.GetError(JsonConvert.DeserializeObject<Result>(jsonResponse));
            }
        }
    }
}
