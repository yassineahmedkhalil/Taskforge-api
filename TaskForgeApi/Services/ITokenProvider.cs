using TaskForgeApi.Entities;
using TaskForgeApi.Models;

namespace TaskForgeApi.Services
{
    public interface ITokenProvider
    {
        public string CreateToken(User user);
        public Task<TokenResponseDto> CreateTokenResponse(User user);
        public Task<User?> ValidateRefreshTokenAsync(Guid userId, string refreshToken);
    }
}
