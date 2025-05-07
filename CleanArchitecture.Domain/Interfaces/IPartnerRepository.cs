using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Domain.Interfaces;

public interface IPartnerRepository
{
    Task<Partner> GetByIdAsync(int id);
    Task<IEnumerable<Partner>> GetAllAsync();
    Task<int> CreateAsync(Partner partner);
    Task UpdateAsync(Partner partner);
    Task DeleteAsync(int id);
} 