using Exercitiu.Models;
using MiniMarket.DTOs;
using MiniMarket.Repositories;

namespace MiniMarket.Services;

public class AuthService
{
    private readonly AuthRepository _authRepository;

    public AuthService(AuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<User> Register(AuthDto dto)
    {
        var user = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.Password
        };
        return await _authRepository.AddUser(user);
    }

    public async Task<User> SignIn(SignInDto dto)
    {
        var user = await _authRepository.GetByEmail(dto.Email);

        if (user == null)
            throw new Exception("Email sau parola gresita");

        if (user.PasswordHash != dto.Password)
            throw new Exception("Email sau parola gresita");

        return user;
    }

}