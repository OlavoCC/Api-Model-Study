using System.Reflection.Metadata;
using Api.Domain.Models;
using Api.DTOs.User;
using Api.Infrastructure.Response;
using Api.Infrastructure.Data;
using Api.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Application;

public class UserService : IUserInterface
{
    private readonly AppDbContext _context;
    public UserService (AppDbContext context)
    {
        _context = context;
    }
    //Aqui eu injeto o banco de dados para poder ser usado

    public async Task<UserResponseDTO> CreateUserAsync(CreateUserDTO dto)
    {
        var user = new User
        {
            Name = dto.Name
        }; //Seta o Model como DTO

        _context.Users.Add(user);
        await _context.SaveChangesAsync(); //Salva no banco

        return new UserResponseDTO
        {
            Id = user.Id,
            Name = user.Name
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
}
        