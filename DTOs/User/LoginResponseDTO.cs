namespace Api.DTOs.User;

public class LoginResponseDTO
{
    public bool Success { get; set; }
    public string? Token { get; set; }
}