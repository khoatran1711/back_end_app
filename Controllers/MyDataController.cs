using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Services;

namespace _Net_API_Web.Controllers;



[ApiController]
[Route("[controller]")]






public class MyDataController : ControllerBase
{
    private readonly ILogger<MyDataController> _logger;

    public MyDataController(ILogger<MyDataController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetMydataall")]
    public IEnumerable<MyData> GetAll()
    {
        return MyData_Services.GetAll()
        .ToArray();
    }

    [HttpGet("getbyID/{id}")]
    public ActionResult<MyData> Get(int id)
    {
        var data = MyData_Services.Get(id);

        if(data == null)
            return NotFound();
        return data;
    }
    [HttpPost]
    public IActionResult Create(MyData data)
    {            
        MyData_Services.Add(data);
        return CreatedAtAction(nameof(Create), new { id = data.myid }, data);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, MyData data)
    {
        if (id != data.myid)
            return BadRequest();
        var existingPizza = MyData_Services.Get(id);
        if(existingPizza is null)
            return NotFound();
        MyData_Services.Update(data);           
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var data = MyData_Services.Get(id);
        if (data is null)
            return NotFound();      
        MyData_Services.Delete(id);
        return NoContent();
    }
}