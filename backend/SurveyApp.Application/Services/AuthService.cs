using SurveyApp.Application.DTOs.Auth;
using SurveyApp.Application.Interfaces.Repositories;
using SurveyApp.Application.Interfaces.Services;

namespace SurveyApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
            => throw new NotImplementedException();

        public Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
            => throw new NotImplementedException();
    }
}
