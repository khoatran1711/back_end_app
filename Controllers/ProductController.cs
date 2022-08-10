using Microsoft.AspNetCore.Mvc;
using _Net_API_Web.Services;
using _Net_API_Web.Models;

namespace _Net_API_Web.Controllers;



[ApiController]
[Route("[controller]")]






public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet("")]
    public IEnumerable<Product> GetAll()
    {
        return Product_Services.GetAll()
        .ToArray();
    }

    [HttpGet("{id_product}")]
    public ActionResult<Product> Get(string id_product)
    {
        var data = Product_Services.Get(id_product);

        if(data == null)
            return NotFound();
        return data;
    }

    [HttpGet("getbycate/{cate_name}")]
    public IEnumerable<Product> GetbyCategory(string cate_name)
    {
        var data = Product_Services.GetbyCategory(cate_name);

        return data;
    }


    [HttpPost]
    public IActionResult Create( Product data)
    {         
        Product_Services.Add(data);
        return CreatedAtAction(nameof(Create), new { id_product = data.id_product }, data);
    }

    [HttpPut("{id_product}")]
    public IActionResult Update(string id_product, Product data)
    {
        if (id_product != data.id_product)
            return BadRequest();
        var existingPizza = Product_Services.Get(id_product);
        if(existingPizza is null)
            return NotFound();
        Product_Services.Update(data);           
        return NoContent();
    }

    [HttpDelete("{id_product}")]
    public IActionResult Delete(string id_product)
    {
        var data = Product_Services.Get(id_product);
        if (data is null)
            return NotFound();      
        Product_Services.Delete(id_product);
        return NoContent();
    }
}