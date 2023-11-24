using Newtonsoft.Json;

namespace ProductStore.Models;

public class TokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; } = null!;
}
