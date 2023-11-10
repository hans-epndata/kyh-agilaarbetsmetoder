using System.Diagnostics;
using WebApp.Helpers.Repositories;
using WebApp.Models;
using WebApp.Models.Entities;
using WebApp.Models.Users;

namespace WebApp.Helpers.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly ApiKeyRepository _apiKeyRepository;

        public UserService(UserRepository userRepository, ApiKeyRepository apiKeyRepository)
        {
            _userRepository = userRepository;
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task<ServiceResponse> CreateUserAsync(UserCreateRequest request)
        {
            try
            {
                if (await _userRepository.ExistsAsync(x => x.Email == request.Email))
                    return new ServiceResponse
                    {
                        Status = Enums.ResponseStatusCode.EXISTS,
                        Message = "User with the same email address already exists",
                        Result = null!
                    };

                User user = await _userRepository.CreateAsync(request);
                if (user != null)
                {
                    var apiKey = await _apiKeyRepository.CreateAsync(new KeyEntity
                    {
                        UserId = user.Id,
                        Key = Guid.NewGuid().ToString(),
                    });

                    return new ServiceResponse
                    {
                        Status = Enums.ResponseStatusCode.CREATED,
                        Message = "User was created successfully",
                        Result = new UserCreateResult
                        {
                            User = user,
                            ApiKey = apiKey.Key
                        }
                    };
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return new ServiceResponse
            {
                Status = Enums.ResponseStatusCode.ERROR,
                Message = "Something went wrong while creating the user."
            };
        }

        public async Task<ServiceResponse> LoginUserAsync(UserLoginRequest request)
        {
            try
            {
                var user = await _userRepository.GetAsync(x => x.Email == request.Email);
                if (user != null)
                {
                    if (user.ValidatePassword(request.Password))
                    {
                        var apiKey = await _apiKeyRepository.GetAsync(x => x.UserId == user.Id);

                        return new ServiceResponse
                        {
                            Status = Enums.ResponseStatusCode.OK,
                            Message = "User is authorized",
                            Result = new UserLoginResult
                            {
                                User = user,
                                ApiKey = apiKey.Key
                            }
                        };
                    }

                }
            }
            catch (Exception ex) { Debug.WriteLine($"{ex.Message}"); }

            return new ServiceResponse
            {
                Status = Enums.ResponseStatusCode.UNAUTHORIZED,
                Message = "Incorrect email or password"
            };
        }
    }
}
