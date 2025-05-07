using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using BCrypt.Net;

namespace CleanArchitecture.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> GetByIdAsync(int id) => _userRepository.GetByIdAsync(id);
    public Task<User> GetByEmailAsync(string email) => _userRepository.GetByEmailAsync(email);
    public Task<User> GetByUsernameAsync(string username) => _userRepository.GetByUsernameAsync(username);
    public Task<IEnumerable<User>> GetAllAsync() => _userRepository.GetAllAsync();
    public Task<int> CreateAsync(User user) => _userRepository.CreateAsync(user);
    public Task UpdateAsync(User user) => _userRepository.UpdateAsync(user);
    public Task DeleteAsync(int id) => _userRepository.DeleteAsync(id);

    public async Task<User> GetByUsernameOrEmailAsync(string usernameOrEmail)
    {
        var user = await _userRepository.GetByUsernameAsync(usernameOrEmail);
        if (user != null) return user;
        return await _userRepository.GetByEmailAsync(usernameOrEmail);
    }

    public async Task<bool> ExistsByUsernameOrEmailAsync(string username, string email)
    {
        var userByUsername = await _userRepository.GetByUsernameAsync(username);
        if (userByUsername != null) return true;
        var userByEmail = await _userRepository.GetByEmailAsync(email);
        return userByEmail != null;
    }

    public async Task<(int? id, int codigo, string mensaje)> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            if (await ExistsByUsernameOrEmailAsync(request.Username, request.Email))
            {
                return (null, 409, "El usuario o email ya existe");
            }
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };
            var id = await _userRepository.CreateAsync(user);
            return (id, 0, "ok");
        }
        catch (Exception ex)
        {
            return (null, 500, ex.Message);
        }
    }
} 