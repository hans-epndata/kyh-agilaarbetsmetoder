using System.ComponentModel.DataAnnotations;
using WebApi.Models.Users;

namespace WebApi.Models.Keys;

public class KeyEntity
{
    [Key]
    public int Id { get; set; }

    public string UserId { get; set; } = null!;
    public UserEntity User { get; set; } = null!;

    public string Key { get; set; } = null!;
}
