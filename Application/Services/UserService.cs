
using Api.Domain.Models;
using Api.DTOs.User;
using Api.Application.Response;
using Api.Infrastructure.Data;
using Api.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Api.Application.Services;

namespace Api.Application;

public class UserService : IUserInterface
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    public UserService (AppDbContext context, TokenService tokenService )
    {
        _context = context;
        _tokenService = tokenService;
    }
    //Aqui eu injeto o banco de dados para poder ser usado

    public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO dto)
    {   
        var user = new User
        {
            Name = dto.Name,
            Password = dto.Password
        }; //Seta o Model como DTO

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); //Salva no banco

        return new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name,

        }; //Retorna o DTO, uma funcao retorna dados (sem writeline)
    }
    
    public async Task<ApiResponse<IEnumerable<UserResponseDTO>>> GetAllUsers()
    {
        var users = await _context.Users.ToListAsync(); //Pego os usuarios
        var total = await _context.Users.CountAsync(); //Pego o total de usuarios (somente na ApiResponse, usuario nao tem total por isso nao fica na DTO)  
        var DTO = users.Select(user => new UserResponseDTO{ //Converte para DTO
            Id = user.Id,
            Name = user.Name,

        });
        
        return new ApiResponse<IEnumerable<UserResponseDTO>>
        {
            Data = DTO,
            TotalUsers = total
        }; //Retorna a lista da DTO, porem com um objeto ApiResponse
    }

    public async Task<LoginResponseDTO> LoginAsync(CreateUserDTO dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == dto.Name);
        if (user != null && user.Name == dto.Name && user.Password == dto.Password) //Correto eh usar hash na senha, adicionar depois
        {
            return new LoginResponseDTO
            {
                Success = true,
                Token = _tokenService.CreateToken(user.Id, user.Name)   
            };
        }

        else
        {
            return new LoginResponseDTO
            {
                Success = false
            };
        }
    }

    public async Task<LoginResponseDTO> DeleteAsync(DeleteUserDTO dto)
    {

        var user = await _context.Users.FindAsync(dto.Id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new LoginResponseDTO
            {
                Success = true
            };
        }
        else
        {
            return new LoginResponseDTO
            {
                Success = false
            };
        }
    }
}
        