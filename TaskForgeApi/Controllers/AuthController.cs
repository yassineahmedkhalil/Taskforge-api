using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
    public async Task<ActionResult<User>> Register(UserDto request)
    {
      var user = await authService.RegisterAsync(request);
      if (user is null)
      {
            return BadRequest("Username already exists.");
      }
      return Ok(user);
    }
    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginAsync(UserDto request)
    {
      var token = await authService.LoginAsync(request);
      if (token is null)
      {
        return BadRequest("Invalid username or password.");
      }
      return Ok(token);
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
    public async Task<List<User>> getUsers()
    {
        return await authService.getUsers();
    }
  }
}
