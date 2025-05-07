using System;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Interfaces;
using BCrypt.Net;

namespace CleanArchitecture.Infrastructure.Data;

public class DataLoader
{
    private readonly IUserRepository _userRepository;

    public DataLoader(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task SeedDataAsync()
    {
        // Verificar si ya existe el usuario administrador
        var adminUser = await _userRepository.GetByUsernameAsync("jhonny.ramirez");
        if (adminUser == null)
        {
            var user = new User
            {
                Username = "jhonny.ramirez",
                Email = "jramirez@npssac.com.pe",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                FirstName = "Jhonny Alexander",
                LastName = "Ramirez Chiroque",
                Role = "ADMIN",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            await _userRepository.CreateAsync(user);
        }
    }
} 