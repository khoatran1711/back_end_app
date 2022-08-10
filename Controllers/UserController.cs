using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Services;
using _Net_API_Web.Models;

namespace _Net_API_Web.Controllers;



[ApiController]
[Route("[controller]")]






public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IEnumerable<User> GetAll()
    {
        return UserInfo_Services.GetAll()
        .ToArray();
    }

    [HttpGet("{user_account}")]
    public ActionResult<User> Get(string user_account)
    {
        var data = UserInfo_Services.Get(user_account);

        if(data == null)
            return NotFound();
        return data;
    }


    [HttpPost]
    public IActionResult Create( User data)
    {         
        UserInfo_Services.Add(data);
        return CreatedAtAction(nameof(Create), new { user_account = data.user_account }, data);
    }

    [HttpPut("{user_account}")]
    public IActionResult Update(string user_account, User data)
    {
        if (user_account != data.user_account)
            return BadRequest();
        var existingPizza = UserInfo_Services.Get(user_account);
        if(existingPizza is null)
            return NotFound();
        UserInfo_Services.Update(data);           
        return NoContent();
    }

    [HttpDelete("{user_account}")]
    public IActionResult Delete(string user_account)
    {
        var data = UserInfo_Services.Get(user_account);
        if (data is null)
            return NotFound();      
        UserInfo_Services.Delete(user_account);
        return NoContent();
    }
}