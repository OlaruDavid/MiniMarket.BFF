using MiniMarket.DTOs;
using MiniMarket.Repositories;

namespace MiniMarket.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepository;
    private readonly CartRepository _cartRepository;

    public OrderService(OrderRepository orderRepository, CartRepository cartRepository)
    {
        _orderRepository=orderRepository;
        _cartRepository=cartRepository;
    }

    public async Task PlaceOrder(int userId)
    {
        int? cartId = await _cartRepository.GetCartIdForUser(userId);
        if (!cartId.HasValue) 
            throw new Exception("Nu există coș pentru acest user.");

        var cartItems = await _orderRepository.GetCartItemsForOrder(cartId.Value);
        if (!cartItems.Any())
            throw new Exception("Coșul este gol. Nu se poate plasa comanda.");

        decimal total = cartItems.Sum(item => item.Price * item.Quantity);
        int orderId = await _orderRepository.CreateOrder(userId, total);

        foreach(var item in cartItems)
        {
            await _orderRepository.AddOrderItem(orderId, item.ProductId, item.Quantity, item.Price);
        }

        await _orderRepository.ClearCart(cartId.Value);

    }
    public async Task<List<OrderDto>> GetOrdersForUser(int userId)
    {
        var orders = (await _orderRepository.GetOrdersByUser(userId)).ToList();

        foreach (var order in orders)
        {
            var items = await _orderRepository.GetOrderItems(order.OrderId);
            order.Items = items.ToList();
        }

        return orders;
    }
}