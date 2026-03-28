using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.Dtos;
using UsersApi.Model;
using UsersApi.Services;

namespace UsersApi.Controllers;


[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{

    private UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }


    [HttpPost("cadastro")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto dto)
    {
        await _userService.Cadastrar(dto);
        return Ok("User created!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var token = await _userService.Login(dto);
        return Ok(token);
    }


    [HttpGet("all")]
    public async Task<IActionResult> All()
    {
        var users = _userService.GetUsers();
        return Ok(users);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete()
    {
        await _userService.Delete();
        return Ok("feito");
    }


}
