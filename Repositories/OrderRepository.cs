using Dapper;
using MiniMarket.Data;
using MiniMarket.DTOs;

namespace MiniMarket.Repositories;

public class OrderRepository
{
    public async Task<IEnumerable<CartItemForOrderDto>> GetCartItemsForOrder(int cartId)
    {
        using var connection=Database.GetConnection();
        var sql = @"
            SELECT 
                ci.product_id AS ProductId,
                ci.quantity AS Quantity,
                p.price AS Price
            FROM cart_items ci
            JOIN products p ON ci.product_id = p.id
            WHERE ci.cart_id = @CartId;
        ";

        return await connection.QueryAsync<CartItemForOrderDto>(sql, new { CartId = cartId });
    }

    public async Task<int> CreateOrder(int userId, decimal total)
    {
        using var connection=Database.GetConnection();
        var sql = @"
            INSERT INTO orders (user_id, total)
            VALUES (@UserId, @Total)
            RETURNING id;
        ";

        return await connection.ExecuteScalarAsync<int>(sql, new
        {
            UserId = userId,
            Total = total
        });
    }

    public async Task AddOrderItem(int orderId, int productId, int quantity, decimal price)
    {
        using var connection=Database.GetConnection();
        var sql = @"
            INSERT INTO order_items (order_id, product_id, quantity, price)
            VALUES (@OrderId, @ProductId, @Quantity, @Price);
        ";

        await connection.ExecuteAsync(sql, new
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            Price = price
        });
    }

    public async Task ClearCart(int cartId)
    {
        using var connection=Database.GetConnection();
        var sql = "DELETE FROM cart_items WHERE cart_id = @CartId";
        await connection.ExecuteAsync(sql, new { CartId = cartId });
    }
     public async Task<IEnumerable<OrderDto>> GetOrdersByUser(int userId)
        {
            using var connection = Database.GetConnection();

            var sql = @"
                SELECT 
                    o.id AS OrderId,
                    o.total,
                    o.status,
                    o.created_at AS CreatedAt
                FROM orders o
                WHERE o.user_id = @UserId
                ORDER BY o.created_at DESC
            ";

            return await connection.QueryAsync<OrderDto>(sql, new { UserId = userId });
        }

        public async Task<IEnumerable<OrderItemDto>> GetOrderItems(int orderId)
        {
            using var connection = Database.GetConnection();

            var sql = @"
                SELECT
                    oi.product_id AS ProductId,
                    p.title AS Title,
                    oi.quantity,
                    oi.price
                FROM order_items oi
                JOIN products p ON p.id = oi.product_id
                WHERE oi.order_id = @OrderId
            ";

            return await connection.QueryAsync<OrderItemDto>(sql, new { OrderId = orderId });
        }
}
