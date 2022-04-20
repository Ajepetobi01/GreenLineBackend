using GreenLineSystems.Core.Interfaces;
using GreenLineSystems.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GreenLineSystems.API.Controllers;

public class UserController : Controller
{
    public readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("CreateUser")]
    [Produces(typeof(MessageResult<bool>))]
    public async Task<IActionResult> NewUser(UserCreationRequest model)
    {
        var response = await _userService.AddUser(model);
        return StatusCode(response.Code, response);
    }
    
    [HttpPost("Login")]
    [Produces(typeof(MessageResult<LoginResponse>))]
    public async Task<IActionResult> LoginUser(LoginModel model)
    {
        var response = await _userService.LoginUser(model);
        return StatusCode(response.Code, response);
    }
    
    [HttpPost("ResetPassword")]
    [Produces(typeof(MessageResult<bool>))]
    public async Task<IActionResult> ChangePassword(UserCredentialsUpdate model)
    {
        var response = await _userService.ResetCredentials(model);
        return StatusCode(response.Code, response);
    }
}