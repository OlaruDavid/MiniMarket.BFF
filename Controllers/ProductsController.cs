using Microsoft.AspNetCore.Mvc;
using MiniMarket.Services;

namespace MiniMarket.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(ProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] string gender, [FromQuery] string category)
    => Ok(await productService.GetProducts(gender, category));
}