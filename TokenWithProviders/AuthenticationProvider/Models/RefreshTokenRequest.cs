namespace AuthenticationProvider.Models;

public class RefreshTokenRequest
{
    public required string AccessToken { get; set; }
}
