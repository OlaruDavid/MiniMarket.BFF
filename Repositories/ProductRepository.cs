using Dapper;
using MiniMarket.Data;
using MiniMarket.Models;

namespace MiniMarket.Repositories;

public class ProductRepository
{
    public async Task<IEnumerable<Product>> GetProductsByGenderAndCategory(string gender, string category)
    {
        using var connection = Database.GetConnection();
        string sql = @"SELECT 
                            id, title, description, price, image_url, gender, category, created_at
                            FROM products
                            WHERE gender = @Gender AND category = @Category;
                            ";
        return await connection.QueryAsync<Product>(sql, new { Gender = gender, Category = category });
    }

}