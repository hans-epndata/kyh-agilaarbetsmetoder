using AuthenticationProvider.Enums;
using AuthenticationProvider.Models;
using AuthenticationProvider.Services;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(AuthService authService) : ControllerBase
    {
        private readonly AuthService _authService = authService;

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(SignUpForm form)
        {
            if (ModelState.IsValid)
            {
                ServiceStatusCode result = await _authService.SignUpAsync(form);
                return result switch
                {
                    ServiceStatusCode.Success => Ok("User was successfully created."),
                    ServiceStatusCode.AlreadyExists => Conflict("User with the same email address already exists."),
                    _ => BadRequest("Something went wrong when trying to create the user account."),
                };
            }

            return BadRequest();
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(SignInForm form)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.SignInAsync(form);

                
                HttpContext.Response.Cookies.Append("refreshToken", result.RefreshToken!, new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTime.Now.AddDays(60)
                });

                HttpContext.Response.Headers.Append("Authorization", $"Bearer {result.AccessToken}");

                return result.StatusCode switch
                {
                    ServiceStatusCode.Success => Ok(result.AccessToken),
                    ServiceStatusCode.AlreadyExists => Conflict("User with the same email address already exists."),
                    _ => BadRequest("Something went wrong when trying to create the user account."),
                };
            }

            return Unauthorized();
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RefreshTokenAsync(HttpContext);
                
                return result.StatusCode switch
                {
                    ServiceStatusCode.Success => Ok(result.AccessToken),
                    _ => Unauthorized(),
                };
            }

            return Unauthorized();
        }
    }
}
