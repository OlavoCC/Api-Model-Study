using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Api.Application.Services;
public class TokenService
{
    private const string KEY = "4d8f9a1c2e7b4f6a9d3e8c1b7a5f2e9d";

    public string CreateToken(int userID, string name)
    {
        var claims = new[] //claim eh um dado do usuario, nesse caso name e userid
        {
            new Claim(ClaimTypes.NameIdentifier, userID.ToString()),
            new Claim(ClaimTypes.Name, name)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY)); //transforma a key em bytes
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //cria a token
        
        var token = new JwtSecurityToken( //cria a token a partir do claims e credenciais
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token); //converte o token em string
    }
}