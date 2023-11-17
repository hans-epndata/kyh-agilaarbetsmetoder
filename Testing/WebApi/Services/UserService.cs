using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using System.Security.Claims;
using WebApi.Enums;
using WebApi.Models;
using WebApi.Models.Entities;
using WebApi.Repositories;

namespace WebApi.Services;

public interface IUserService
{
    Task<TokenResponse> SignInAsync(string email, string password);
    Task<ServiceStatusCodes> SignUpAsync(SignUpRequest request);
}

public class UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ITokenGenerator _tokenGenerator = tokenGenerator;

    public async Task<TokenResponse> SignInAsync(string email, string password)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(x => x.Email == email);
            if (userEntity != null)
            {
                var isValidPassword = ValidatePassword(userEntity, password);
                if (isValidPassword)
                {
                    var tokenResponse = new TokenResponse
                    {
                        AccessToken = _tokenGenerator.GenerateAccessToken([new Claim("UserId", userEntity.Id)]),
                        RefreshToken = await _tokenGenerator.GenerateRefreshToken(userEntity.Id)
                    };
                    return tokenResponse;
                }

            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<ServiceStatusCodes> SignUpAsync(SignUpRequest request)
    {
        try
        {
            var userEntity = await _userRepository.GetAsync(x => x.Email == request.Email);
            if (userEntity != null)
                return ServiceStatusCodes.Conflict;

            userEntity = new UserEntity
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = GeneratePasswordHash(request.Password),
                SecurityKey = Guid.NewGuid().ToString()
            };

            userEntity = await _userRepository.AddAsync(userEntity);
            return ServiceStatusCodes.Created;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ServiceStatusCodes.BadRequest!;
    }


    private bool ValidatePassword(UserEntity user, string password)
    {
        try
        {
            var passwordHasher = new PasswordHasher<UserEntity>();
            var result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
            return result == PasswordVerificationResult.Success;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    private string GeneratePasswordHash(string password)
    {
        try
        {
            var passwordHasher = new PasswordHasher<UserEntity>();
            var passwordHash = passwordHasher.HashPassword(null!, password);
            return passwordHash ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
