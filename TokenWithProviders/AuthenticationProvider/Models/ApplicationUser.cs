using Microsoft.AspNetCore.Identity;

namespace AuthenticationProvider.Models;

public class ApplicationUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDate { get; set; }
}
