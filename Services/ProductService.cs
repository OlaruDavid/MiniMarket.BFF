using MiniMarket.Models;
using MiniMarket.Repositories;

namespace MiniMarket.Services;

public class ProductService(ProductRepository productRepository)
{
    public async Task<IEnumerable<Product>> GetProducts(string gender, string category)
    => await productRepository.GetProductsByGenderAndCategory(gender, category);
}