using Newtonsoft.Json;

namespace DamienVDK.Bookrentoo.OrganizationApp.Models.Firebase
{
    public class FirebaseAuthExceptionDetail
    {
        [JsonProperty("error")]
        public Error Error { get; set; } = new Error();

        public string GetErrorMessage()
        {
            if (string.IsNullOrWhiteSpace(Error?.Message)) return "Unknown error";
            if (Error.Message.Equals("INVALID_EMAIL", StringComparison.OrdinalIgnoreCase) ||
                Error.Message.Equals("INVALID_PASSWORD", StringComparison.OrdinalIgnoreCase)) return "Unknown email or password";

            return "Unknown error";
        }
    }

    public class Error
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; } = string.Empty;
    }
}
 