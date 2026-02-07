using Dapper;
using Exercitiu.Models;
using MiniMarket.Data;

namespace MiniMarket.Repositories;

public class AuthRepository
{
    public async Task<User> AddUser(User user)
    {
        using var connection = Database.GetConnection();

        string sql = @"INSERT INTO users (name,email,passwordhash)
                        VALUES (@Name,@Email,@PasswordHash)
                        RETURNING Id";

        var id = await connection.QuerySingleAsync<int>(sql, new
        {
            Name = user.Name,
            Email = user.Email,
            PasswordHash = user.PasswordHash
        });

        user.Id = id;

        return user;
    }

    public async Task<User?> GetByEmail(string email)
    {
        using var connection = Database.GetConnection();

        string sql = @"SELECT * FROM users WHERE email = @Email";

        return await connection.QuerySingleOrDefaultAsync<User>(sql, new
        {
            Email = email
        });
    }

}