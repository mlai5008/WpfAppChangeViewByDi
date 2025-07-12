using System.Text.Json.Serialization;

namespace AppWpfWcl.Models
{
    public class User
    {
        #region Properties        
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonPropertyName("userStatus")]
        public int UserStatus { get; set; }
        public string Message { get; set; }
        #endregion
    }
}
