using Microsoft.AspNetCore.Mvc;
using MiniMarket.DTOs;
using MiniMarket.Services;

namespace MiniMarket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthDto dto)
    {
        var user = await authService.Register(dto);
        return Ok(user);
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn([FromBody] SignInDto dto)
    {
        var user = await authService.SignIn(dto);
        return Ok(user);
    }
}