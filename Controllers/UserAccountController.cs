using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Services;
using _Net_API_Web.Models;

namespace _Net_API_Web.Controllers;



[ApiController]
[Route("[controller]")]






public class UserAccountController : ControllerBase
{
    private readonly ILogger<UserAccountController> _logger;

    public UserAccountController(ILogger<UserAccountController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<UserAccount> GetAll()
    {
        return UserAccount_Services.GetAll()
        .ToArray();
    }

    [HttpGet("{account}")]
    public ActionResult<UserAccount> Get(string account)
    {
        var data = UserAccount_Services.Get(account);

        if (data == null)
            return NotFound();
        return data;
    }
    [HttpPost]
    public IActionResult Create(UserAccount data)
    {

        UserAccount_Services.Add(data);
        return CreatedAtAction(nameof(Create), new { user_account = data.Account }, data);
    }

    [HttpPut("{account}")]
    public IActionResult Update(string account, UserAccount data)
    {
        if (account != data.Account)
            return BadRequest();
        var existingPizza = UserAccount_Services.Get(account);
        if (existingPizza is null)
            return NotFound();
        UserAccount_Services.Update(data);
        return NoContent();
    }

    [HttpDelete("{account}")]
    public IActionResult Delete(string account)
    {
        var data = UserAccount_Services.Get(account);
        if (data is null)
            return NotFound();
        UserAccount_Services.Delete(account);
        return NoContent();
    }
}