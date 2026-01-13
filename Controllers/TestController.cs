using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    [Authorize]
    [HttpGet("private")]
    public IActionResult Private()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = User.FindFirstValue(ClaimTypes.Name);

        return Ok(new
        {
            message = "Acesso autorizado 😎",
            userId,
            email
        });
    }
}
