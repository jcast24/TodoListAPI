using TodoApi.Models;

namespace TodoApi.Services
{
    public class AuthService : IAuthService
    {
        public Task<TokenResponseDto?> LoginAsync(UserDto request)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task<User?> RegisterAsync(UserDto request)
        {
            throw new NotImplementedException();
        }
    }
}
