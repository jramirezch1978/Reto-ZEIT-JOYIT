using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;

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
} 