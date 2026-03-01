using TaskForgeApi.Entities;
using TaskForgeApi.Models;

namespace TaskForgeApi.Services
{
  public interface IAuthService
  {
    Task<User?> RegisterAsync(RegisterDto request);
    Task<TokenResponseDto?> LoginAsync(LoginDto request);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<List<User>> GetUsersAsync();
    Task<bool> IsEmailExistsAsync(RegisterDto request);
    Task<bool> IsUsernameExistsAsync(RegisterDto request);
    }
}
