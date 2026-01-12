using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.User;

public class CreateUserDTO
{
    [Required(ErrorMessage = "Name is required")] //faz com que nao possa ser null
    public string? Name { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
    public string? Password { get; set; }
}

//O que o controller vai receber