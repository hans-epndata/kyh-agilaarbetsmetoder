using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class UserEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string SecurityKey { get; set; } = null!;

    }
}
