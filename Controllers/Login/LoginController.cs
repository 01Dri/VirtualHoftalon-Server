using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Models.Security.Dto;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller.User;

public class LoginController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    [Route("/login/register")]
    public IActionResult Register([FromBody] RegisterLoginDTO registerDto)
    {
        if (!ModelState.IsValid)
        {
                return BadRequest(ModelState);
        }
        return Ok(_loginService.Register(registerDto));
    }
    
    [HttpPost]
    [Route("/login/login")]
    public IActionResult Login([FromBody] LoginDTO loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(_loginService.Login(loginDto));
    }
    
    [HttpPost]
    [Route("/login/refreshtoken")]
    public IActionResult RefreshToken([FromBody] RefreshTokenRequestDTO refreshTokenRequestDto)
    {
        if (string.IsNullOrEmpty(refreshTokenRequestDto.refreshToken) || refreshTokenRequestDto.loginId == null)
        {
            throw new InvalidRefreshToken("Refresh token or LoginId don't be null");
        }
        return Ok(_loginService.RefreshToken(refreshTokenRequestDto));
    }
}