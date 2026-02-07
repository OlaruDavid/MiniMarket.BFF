using MiniMarket.DTOs;
using MiniMarket.Repositories;

namespace MiniMarket.Services;

public class CartService
{
    private readonly CartRepository _cartRepository;

    public CartService(CartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public async Task<IEnumerable<CartItemDto>> GetCartItems(int userId)
    {
        return await _cartRepository.GetCartItemsByUser(userId);
    }

    public async Task AddToCart(int userId, int productId, int quantity)
    {
        int cartId = await _cartRepository.GetOrCreateCartId(userId);

        await _cartRepository.AddToCart(cartId, productId, quantity);
    }

    public async Task RemoveCartItem(int cartItemId)
    {
        await _cartRepository.RemoveCartItem(cartItemId);
    }
}
