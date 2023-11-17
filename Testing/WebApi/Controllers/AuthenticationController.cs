using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApi.Enums;
using WebApi.Models;
using WebApi.Models.Entities;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ITokenGenerator tokenGenerator, IUserService userService) : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator = tokenGenerator;
        private readonly IUserService _userService = userService;

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tokenResponse = await _userService.SignInAsync(request.Email, request.Password);
                if (tokenResponse != null)
                    return Ok(tokenResponse);

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Unauthorized();
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                ServiceStatusCodes result = await _userService.SignUpAsync(request);
                return result switch
                {
                    ServiceStatusCodes.Created => Created("", null!),
                    ServiceStatusCodes.Conflict => Conflict(),
                    _ => Unauthorized(),
                };
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Unauthorized();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenRequest request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tokenResponse = await _tokenGenerator.RefreshTokenAsync(request);
                    if (tokenResponse != null)
                        return Ok(tokenResponse);
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Unauthorized();
        }
    }  
}
