namespace MiniMarket.DTOs;

public class CartItemDto
{
    public int CartItemId { get; set; } 
    public int ProductId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
