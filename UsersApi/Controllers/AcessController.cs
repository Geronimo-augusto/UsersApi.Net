using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class AcessController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy ="IdadeMinima")]
    public IActionResult Get()
    {
        return Ok("Acesso permitido!");
    }
   
}
