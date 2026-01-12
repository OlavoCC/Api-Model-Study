using Microsoft.AspNetCore.Mvc;
using Api.Domain.Models;
using System.Runtime.CompilerServices;
using Api.Application.Interfaces;
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

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDTO>> CreateUserAsync([FromBody] CreateUserDTO dto)
    {
        try
        {
            var createdUser = await _userService.CreateUserAsync(dto); //Uso o metodo para mandar a dto
         return Ok(createdUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
         
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<UserResponseDTO>>> GetAllUsers()
    {
        var users = await _userService.GetAllUsers(); //pego os dados
        return Ok(users);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> LoginAsync([FromBody] CreateUserDTO dto)
    {
        var loginResponse = await _userService.LoginAsync(dto);
        return Ok(loginResponse);
    }

    [HttpDelete("delete")]
    public async Task<ActionResult<LoginResponseDTO>> DeleteAsync([FromBody] DeleteUserDTO dto)
    {
        var DeleteResult = await _userService.DeleteAsync(dto);
        return Ok(DeleteResult);
    }
    
}