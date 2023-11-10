using System.Diagnostics;

namespace WebApi.Models.Users;

public class User
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public static implicit operator User(UserEntity entity)
    {
        try
        {
            return new User
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
