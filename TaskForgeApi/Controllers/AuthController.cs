using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskForgeApi.Entities;
using TaskForgeApi.Models;
using TaskForgeApi.Services;

namespace TaskForgeApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterDto request)
    {
        var emailExists = await authService.IsEmailExistsAsync(request);
        var usernameExists = await authService.IsUsernameExistsAsync(request);

        if (emailExists || usernameExists)
        {

        var errors = new Dictionary<string, string[]> {};
            if(emailExists)
            {
                errors["email"] = new[] { "Email is already in use" }; 
            }
            if(usernameExists)
            {
                errors["username"] = new[] { "Username is already in use" };
            }

        return Conflict(new ValidationProblemDetails(errors)
        {
            Status = StatusCodes.Status409Conflict,
            Title = "Registration conflict",
            Detail = "One or more validation errors occurred.",
            Instance = HttpContext.Request.Path
        });
      }
      var user = await authService.RegisterAsync(request);

      return Ok(user);
    }
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> LoginAsync(LoginDto request)
    {
      var result = await authService.LoginAsync(request);
      if (result is null)
      {
        return Unauthorized(new ProblemDetails
        {
            Title = "Unauthorized",
            Status = StatusCodes.Status401Unauthorized,
            Detail = "Invalid credentials"
        });
      }
      return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
    {
        var result = await authService.RefreshTokenAsync(request);
        if (result is null || result.AccessToken is null || result.RefreshToken is null)
            return Unauthorized(new ProblemDetails
            {
                Title = "Unauthorized",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "Invalid refresh token"
            });
        return Ok(result);
    }

    [Authorize]
    [HttpGet]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are authenticated!");
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("admin-only")]
    public IActionResult AdminOnlyEndpoint()
    {
        return Ok("You are an admin!");
    }

    [HttpGet("getUsers")]
    public async Task<List<User>> GetUsersAsync()
    {
        return await authService.GetUsersAsync();
    }
  }
}
