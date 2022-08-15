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

    [HttpGet("{idProduct}")]
    public ActionResult<Product> Get(string idProduct)
    {
        var data = Product_Services.Get(idProduct);

        if (data == null)
            return NotFound();
        return data;
    }

    [HttpGet("getbycate/{cateName}")]
    public IEnumerable<Product> GetbyCategory(string cateName)
    {
        var data = Product_Services.GetbyCategory(cateName);

        return data;
    }


    [HttpPost]
    public IActionResult Create(Product data)
    {
        Product_Services.Add(data);
        return CreatedAtAction(nameof(Create), new { id_product = data.IdProduct }, data);
    }

    [HttpPut("{idProduct}")]
    public IActionResult Update(string idProduct, Product data)
    {
        if (idProduct != data.IdProduct)
            return BadRequest();
        var existingPizza = Product_Services.Get(idProduct);
        if (existingPizza is null)
            return NotFound();
        Product_Services.Update(data);
        return NoContent();
    }

    [HttpDelete("{idProduct}")]
    public IActionResult Delete(string idProduct)
    {
        var data = Product_Services.Get(idProduct);
        if (data is null)
            return NotFound();
        Product_Services.Delete(idProduct);
        return NoContent();
    }
}