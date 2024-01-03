using Microsoft.AspNetCore.Mvc;
namespace Blog.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    //health-check
    [HttpGet("health")]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "API funcionando"
        });
    }
    
}