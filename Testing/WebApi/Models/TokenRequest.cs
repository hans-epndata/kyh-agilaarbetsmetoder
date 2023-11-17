namespace WebApi.Models;

public class TokenRequest
{
    public string UserId { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
