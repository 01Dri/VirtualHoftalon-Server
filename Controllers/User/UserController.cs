using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Models.Dto.User;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller.User;

public class UserController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [Route("/user/register")]
    public IActionResult Register([FromBody] UserRegisterDTO registerDto)
    {
        return Ok(_userService.Register(registerDto));
    }
}