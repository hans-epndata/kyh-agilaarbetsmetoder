namespace WebApi.Models.Users;

public class UserLoginResult
{
    public User User { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
