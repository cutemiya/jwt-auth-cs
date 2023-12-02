using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fu.Controllers;

public class PingController : BaseController
{
    [HttpGet("ping-with-auth")]
    public Task<IActionResult> PingAuth()
    {
        return Task.FromResult<IActionResult>(Ok("pong with auth"));
    }
    
    [AllowAnonymous]
    [HttpGet("ping-with-out-auth")]
    public Task<IActionResult> Ping()
    {
        return Task.FromResult<IActionResult>(Ok("pong w/o auth"));
    }
}