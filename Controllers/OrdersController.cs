using Microsoft.AspNetCore.Mvc;
using MiniMarket.DTOs;
using MiniMarket.Services;

namespace MiniMarket.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(OrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PlaceOrder([FromBody] CreateOrderDto dto)
    {
        try
        {
            await orderService.PlaceOrder(dto.UserId);
            return Ok(new { message = "Comanda a fost plasată cu succes!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetOrders(int userId)
    {
        var orders = await orderService.GetOrdersForUser(userId);
        return Ok(orders);
    }
}