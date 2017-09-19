using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WEB.ViewModels
{
    public class ErrorItem
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Field { get; set; }
        
        public string Message { get; set; }
        
    }
}