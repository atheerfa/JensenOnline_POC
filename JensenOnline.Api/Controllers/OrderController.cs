using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly AppDbContext _db;
    public OrderController(AppDbContext db) { _db = db; }

    [HttpPost]
    public IActionResult CreateOrder(string product)
    {
        var userId = int.Parse(User.FindFirst("UserId")!.Value);
        _db.Orders.Add(new Order { UserId = userId, Product = product });
        _db.SaveChanges();
        return Ok("Order skapad!");
    }

    [HttpGet("{id}")]
    public IActionResult GetMyOrder(int id)
    {
        var order = _db.Orders.Find(id);

        // kolla om ordern finns
        if (order == null)
        {
            return NotFound("Ordern kunde inte hittas.");
        }

        var userId = User.FindFirst("UserId")?.Value;

        // BOLA skydd, bara admin och kund som beställt
        if (order.UserId.ToString() != userId && !User.IsInRole("Admin"))
        {
            return Forbid();
        }

        return Ok(order);
    }
}