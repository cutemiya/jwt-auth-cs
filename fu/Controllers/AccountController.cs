using fu.Models.Db;
using fu.Models.Dto;
using fu.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace fu.Controllers;

[ApiController]
[Route("account")]
public class AccountController: BaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    [HttpGet("me")]
    public IActionResult GetMe()
    {
        return Ok(new UserDataDto()
        {
            Id = Id,
            Email = Email,
            Username = Username,
            Role = Role
        });
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateMe([FromBody] UserDataDto user)
    {
        await _accountService.UpdateAccount(new DbAccount()
        {
            Id = Id,
            Email = user.Email,
            Username = user.Username
        });
        
        return Ok();
    }

    [HttpPut("{accountId}")]
    public async Task<IActionResult> UpdateOther([FromBody] UserDataDto user, [FromRoute] int accountId)
    {
        if (Role != "admin")
        {
            return BadRequest("u not a admin");
        }

        await _accountService.UpdateAccount(new DbAccount()
        {
            Id = accountId,
            Email = user.Email,
            Username = user.Username,
            Role = user.Role
        });
        
        return Ok();
    }


    [HttpDelete("{accountId}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int accountId)
    {
        if (Role != "admin")
        {
            return BadRequest("u not a admin");
        }

        await _accountService.DeleteAccount(accountId);
        
        return Ok();
    }
}