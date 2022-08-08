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

        if(data == null)
            return NotFound();
        return data;
    }
    [HttpPost]
    public IActionResult Create( string useraccount,string userpassword)
    {         
        UserAccount data = new UserAccount(useraccount,userpassword)   ;
        data.user_account= useraccount;
        data.user_password=userpassword;
        UserAccount_Services.Add(data);
        Console.WriteLine(data.user_account);
        return CreatedAtAction(nameof(Create), new { useraccount = data.user_account }, data);
    }

    [HttpPut("{account}")]
    public IActionResult Update(string account, UserAccount data)
    {
        if (account != data.user_account)
            return BadRequest();
        var existingPizza = UserAccount_Services.Get(account);
        if(existingPizza is null)
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