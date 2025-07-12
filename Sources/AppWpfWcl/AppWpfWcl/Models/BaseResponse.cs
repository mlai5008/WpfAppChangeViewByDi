using System.Text.Json.Serialization;

namespace AppWpfWcl.Models
{
    public class BaseResponse
    {
        #region Properties       
        [JsonPropertyName("code")]
        public int Code { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("message")]
        public string Message { get; set; }
        #endregion
    }
}
