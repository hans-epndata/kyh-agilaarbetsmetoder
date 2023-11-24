using AuthenticationProvider.Enums;

namespace AuthenticationProvider.Models
{
    public class AuthServiceResult
    {
        public ServiceStatusCode StatusCode { get; set; } = ServiceStatusCode.Success;
        public string? RefreshToken { get; set; }
        public string? AccessToken { get; set; }
    }
}
