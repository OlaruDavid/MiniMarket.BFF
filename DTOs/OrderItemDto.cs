namespace MiniMarket.DTOs;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
