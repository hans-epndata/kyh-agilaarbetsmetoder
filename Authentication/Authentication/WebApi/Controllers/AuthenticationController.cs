using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Helpers.Services;
using WebApi.Models.Users;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthenticationController(UserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(UserCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _userService.CreateUserAsync(request);

                return result.Status switch
                {
                    Helpers.Enums.ResponseStatusCode.CREATED => Created("", result.Result),
                    Helpers.Enums.ResponseStatusCode.EXISTS => Conflict(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return Unauthorized("Incorrect email or password");

                var result = await _userService.LoginUserAsync(request);

                return result.Status switch
                {
                    Helpers.Enums.ResponseStatusCode.OK => Ok(result.Result),
                    Helpers.Enums.ResponseStatusCode.UNAUTHORIZED => Unauthorized(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }
    }
}
