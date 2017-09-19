using Newtonsoft.Json;

namespace WEB.ViewModels
{
    public class ApiResponse
    {
        public int StatusCode { get; }
        
        public object Result { get;}

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ApiResponse(int statusCode, object result = null, string message = null)
        {
            Result = result;
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    return "Resource not found";
                case 422:
                    return "Validation error";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }
}