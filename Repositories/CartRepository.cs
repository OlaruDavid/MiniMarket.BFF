using Dapper;
using MiniMarket.Data;
using MiniMarket.DTOs;

namespace MiniMarket.Repositories;

public class CartRepository
{
    public async Task<IEnumerable<CartItemDto>> GetCartItemsByUser(int userId)
    {
        using var connection = Database.GetConnection();
        string sql = @"
                SELECT 
                    ci.id AS CartItemId,
                    p.id AS ProductId,
                    p.title AS Title,
                    p.price AS Price,
                    p.image_url AS ImageUrl,
                    ci.quantity AS Quantity
                FROM cart_items ci
                JOIN carts c ON ci.cart_id = c.id
                JOIN products p ON ci.product_id = p.id
                WHERE c.user_id = @UserId;
            ";
        return await connection.QueryAsync<CartItemDto>(sql, new { UserId = userId });
    }

    public async Task<int> GetOrCreateCartId(int userId)
    {
        using var connection = Database.GetConnection();
        var cartId = await connection.QueryFirstOrDefaultAsync<int?>(
            "SELECT id FROM carts WHERE user_id = @UserId", new { UserId = userId });
        if (cartId.HasValue) return cartId.Value;

        string insertSql = "INSERT INTO carts (user_id) VALUES (@UserId) RETURNING id";
        return await connection.QuerySingleAsync<int>(insertSql, new { UserId = userId });
    }

    public async Task AddToCart(int cartId, int productId, int quantity)
    {
        using var connection = Database.GetConnection();
        string sql = @"
                INSERT INTO cart_items (cart_id, product_id, quantity) 
                VALUES (@CartId, @ProductId, @Quantity)
            ";
        await connection.ExecuteAsync(sql, new { CartId = cartId, ProductId = productId, Quantity = quantity });
    }

    public async Task RemoveCartItem(int cartItemId)
    {
        using var connection = Database.GetConnection();
        string sql = @"DELETE FROM cart_items WHERE id=@Id";

        await connection.ExecuteAsync(sql, new { Id = cartItemId });
    }
    public async Task<int?> GetCartIdForUser(int userId)
    {
        using var connection = Database.GetConnection();
        return await connection.QueryFirstOrDefaultAsync<int?>(
            "SELECT id FROM carts WHERE user_id = @UserId", new { UserId = userId });
    }

}
