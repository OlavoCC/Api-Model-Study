using System.ComponentModel.DataAnnotations;

namespace Api.DTOs.User;

public class CreateUserDTO
{
    [Required(ErrorMessage = "Name is required")] //faz com que nao possa ser null
    public string? Name { get; set; }
}

//O que o controller vai receber