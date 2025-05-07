using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Services;

public class PartnerService
{
    private readonly IPartnerRepository _partnerRepository;

    public PartnerService(IPartnerRepository partnerRepository)
    {
        _partnerRepository = partnerRepository;
    }

    public Task<Partner> GetByIdAsync(int id) => _partnerRepository.GetByIdAsync(id);
    public Task<IEnumerable<Partner>> GetAllAsync() => _partnerRepository.GetAllAsync();
    public Task<int> CreateAsync(Partner partner) => _partnerRepository.CreateAsync(partner);
    public Task UpdateAsync(Partner partner) => _partnerRepository.UpdateAsync(partner);
    public Task DeleteAsync(int id) => _partnerRepository.DeleteAsync(id);
} 