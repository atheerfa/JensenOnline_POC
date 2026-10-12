using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProducts() => Ok(new[] { "Laptop", "Mus" });

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public IActionResult CreateProduct() => Ok("Produkt skapad av Admin!");
}