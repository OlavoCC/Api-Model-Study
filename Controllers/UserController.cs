using Microsoft.AspNetCore.Mvc;
using Api.Domain.Models;
using System.Runtime.CompilerServices;
using Api.Infrastructure.Interfaces;
using Api.Application;
using Api.DTOs.User;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService; //intjeto o service para mandar os itens/receber resposta
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<UserResponseDTO>> CreateUserAsync([FromBody] CreateUserDTO dto)
    {
         var createdUser = await _userService.CreateUserAsync(dto); //Uso o metodo para mandar a dto
         return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers(); //pego os dados
        return Ok(users);
    }
}