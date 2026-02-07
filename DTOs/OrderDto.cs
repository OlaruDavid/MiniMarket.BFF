namespace MiniMarket.DTOs;

public class OrderDto
{
    public int OrderId { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<OrderItemDto> Items { get; set; } = new();
}
