using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QnAMaker.Helpers.Models
{
    public class ErrorResponse
    {
        [JsonProperty("code")]
        public ErrorCodeType Code { get; set; }
        [JsonProperty("details")]
        public List<ErrorResponse> Details { get; set; }
        [JsonProperty("innerError")]
        public InnerErrorModel InnerError { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("target")]
        public string Target { get; set; }

        public ErrorResponse(ErrorCodeType code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public enum ErrorCodeType
    {
        BadArgument,
        EndpointKeysError,
        ExtractionFailure,
        Forbidden,
        KbNotFound,
        NotFound,
        OperationNotFound,
        QnaRuntimeError,
        QuotaExceeded,
        SKULimitExceeded,
        ServiceError,
        Unauthorized,
        Unspecified,
        ValidationFailure,
        SubscriptionKeyNotFound,
        FieldRequired
    }

    public class InnerErrorModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("innerError")]
        public InnerErrorModel InnerError { get; set; }
    }
}
