using TaskForgeApi.Entities;
using TaskForgeApi.Models;

namespace TaskForgeApi.Services
{
  public interface IAuthService
  {
    Task<User?> RegisterAsync(UserDto request);
    Task<TokenResponseDto?> LoginAsync(UserDto request);
    Task<TokenResponseDto?> RefreshTokenAsync(RefreshTokenRequestDto request);
    Task<List<User>> getUsers();
  }
}
