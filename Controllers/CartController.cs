using Microsoft.AspNetCore.Mvc;
using MiniMarket.DTOs;
using MiniMarket.Services;

namespace MiniMarket.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController(CartService cartService) : ControllerBase
{
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetCart(int userId)
    {
        var items = await cartService.GetCartItems(userId);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddCartItemDto dto)
    {
        if (dto.Quantity <= 0) dto.Quantity = 1;
        await cartService.AddToCart(dto.UserId, dto.ProductId, dto.Quantity);
        return Ok();
    }

    [HttpDelete("item/{cartItemId}")]
    public async Task<IActionResult>RemoveCartItem(int cartItemId)
    {
        await cartService.RemoveCartItem(cartItemId);
        return NoContent();
    }
}
