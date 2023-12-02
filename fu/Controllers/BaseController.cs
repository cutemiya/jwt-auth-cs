using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fu.Controllers;

[Authorize]
[ApiController]
public class BaseController: ControllerBase
{
    private string AuthHeader => HttpContext.Request.Headers["Authorization"].ToString();

    protected int Id => Convert.ToInt32(Jwt.GetId(AuthHeader));
    protected string? Email => Jwt.GetEmail(AuthHeader);
    protected string? Username => Jwt.GetUsername(AuthHeader);
    protected string? Role => Jwt.GetRole(AuthHeader);
}