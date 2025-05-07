using Xunit;
using Moq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Tests;

public class ProviderServiceTests
{
    [Fact]
    public async Task CreateProviderAsync_ReturnsProviderId_WhenProviderIsValid()
    {
        var mockRepo = new Mock<IProviderRepository>();
        mockRepo.Setup(r => r.AddProviderAsync(It.IsAny<Provider>())).ReturnsAsync(1);
        var service = new ProviderService(mockRepo.Object);
        var provider = new Provider { Name = "Provider1", Email = "provider@test.com" };
        var result = await service.CreateProviderAsync(provider);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetProviderByIdAsync_ReturnsNull_WhenProviderNotFound()
    {
        var mockRepo = new Mock<IProviderRepository>();
        mockRepo.Setup(r => r.GetProviderByIdAsync(It.IsAny<int>())).ReturnsAsync((Provider)null);
        var service = new ProviderService(mockRepo.Object);
        var result = await service.GetProviderByIdAsync(999);
        Assert.Null(result);
    }
} 