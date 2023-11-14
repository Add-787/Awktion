using Awktion.Application.Dtos.Login;
using Microsoft.AspNetCore.Mvc;

namespace Awktion.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ApiController
{
    private readonly ILogger<UserController> _logger;
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "Login")]
    public async Task<IActionResult> LoginAsync(LoginRequest request)
    {
        return Ok();
    }


}
