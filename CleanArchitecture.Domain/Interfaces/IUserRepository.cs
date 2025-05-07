using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task<int> CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
} 