using WebApi.Helpers.Services;

namespace WebApi.Models.Users;

public class UserCreateResult
{
    public User User { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
}
