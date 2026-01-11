namespace Api.Infrastructure.Response;

public class ApiResponse<T>
{
    public T? Data {get;set;}
    public int TotalUsers { get; set; } //Adiciona o total de usuarios na resposta
}