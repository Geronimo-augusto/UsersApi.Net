using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersApi.Data.Dtos;
using UsersApi.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersApi.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Cadastrar(CreateUsuarioDto dto)
    {
        User user = _mapper.Map<User>(dto);

        IdentityResult resultado = await _userManager.CreateAsync(user, dto.Password);


        var erros = resultado.Errors.Select(e => e.Description);

        if (!resultado.Succeeded) throw new ApplicationException($"Falha ao criar usuário, \nError: {erros}");

    }

    public async Task<string> Login(LoginUserDto dto)
    {
        var resultado = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);


        if (!resultado.Succeeded) throw new ApplicationException($"Falha ao autenticar usuário");

        var user = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(u => u.NormalizedUserName == dto.UserName.ToUpper());

        var token = _tokenService.GenerateToken(user);

        return token;
    }


    public List<User> GetUsers()
    {
        return _userManager.Users.ToList();
    }


    public async Task Delete()
    {
        var listaDeUsers = _userManager.Users.ToList();
        foreach (var user in listaDeUsers)
        {
            await _userManager.DeleteAsync(user);
        }
    }
}
