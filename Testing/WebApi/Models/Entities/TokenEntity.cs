using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities
{
    public class TokenEntity
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; } = false;
    }
}
