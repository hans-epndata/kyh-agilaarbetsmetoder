using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using WebApp.Models.Users;

namespace WebApp.Models.Entities;

public class UserEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public byte[] Password { get; private set; } = null!;
    public byte[] SecurityKey { get; private set; } = null!;

    public static implicit operator UserEntity(UserCreateRequest request)
    {
        try
        {
            var userEntity = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            userEntity.GenerateSecurePassword(request.Password);
            return userEntity;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public void GenerateSecurePassword(string password)
    {
        using var hmac = new HMACSHA512();
        SecurityKey = hmac.Key;
        Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public bool ValidatePassword(string password)
    {
        using var hmac = new HMACSHA512(SecurityKey);
        var _hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < _hash.Length; i++)
            if (_hash[i] != Password[i])
                return false;

        return true;
    }
}
