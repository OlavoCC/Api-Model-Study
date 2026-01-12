using Api.DTOs.User;
using Api.Infrastructure.Response;

namespace Api.Infrastructure.Interfaces;

public interface IUserInterface
{
    Task<UserResponseDTO> CreateUserAsync(CreateUserDTO dto); //Isso significa que ele vai devolver algo, porem antes ele vai criar o Usuario
    Task <ApiResponse<IEnumerable<UserResponseDTO>>> GetAllUsers();   //Vai devolver a lista de usuarios, porem antes ele vai exercutar o metodo GetAllUsers
    Task <LoginResponseDTO> LoginAsync(CreateUserDTO dto);
    Task <LoginResponseDTO> DeleteAsync(DeletUserDTO dto);
}