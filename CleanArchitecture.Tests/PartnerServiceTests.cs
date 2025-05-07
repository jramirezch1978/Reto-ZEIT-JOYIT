using Xunit;
using Moq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Tests;

public class PartnerServiceTests
{
    [Fact]
    public async Task CreateAsync_ReturnsPartnerId_WhenPartnerIsValid()
    {
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<Partner>())).ReturnsAsync(1);
        var service = new PartnerService(mockRepo.Object);
        var partner = new Partner { RazonSocial = "Empresa", TaxId = "123", Type = "Proveedor", ContactName = "Juan", ContactEmail = "juan@test.com", ContactPhone = "123456789", Address = "Calle 1", City = "Ciudad", State = "Estado", Country = "País", PostalCode = "00000", IsActive = true, CreatedAt = System.DateTime.UtcNow };
        var result = await service.CreateAsync(partner);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenPartnerNotFound()
    {
        var mockRepo = new Mock<IPartnerRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Partner)null);
        var service = new PartnerService(mockRepo.Object);
        var result = await service.GetByIdAsync(999);
        Assert.Null(result);
    }
} 