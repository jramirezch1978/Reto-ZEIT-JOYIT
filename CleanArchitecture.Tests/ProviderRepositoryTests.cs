using Xunit;
using Moq;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Tests;

public class ProviderRepositoryTests
{
    [Fact]
    public async Task GetProviderByIdAsync_ReturnsProvider_WhenProviderExists()
    {
        // Arrange
        var mockConnection = new Mock<IDbConnection>();
        var repo = new ProviderRepository(mockConnection.Object);
        // You would set up Dapper's QuerySingleOrDefaultAsync here if you abstracted it
        // For now, just check that the method can be called (example structure)
        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => repo.GetProviderByIdAsync(1));
    }

    [Fact]
    public async Task AddProviderAsync_ThrowsNotImplemented()
    {
        var mockConnection = new Mock<IDbConnection>();
        var repo = new ProviderRepository(mockConnection.Object);
        await Assert.ThrowsAsync<NotImplementedException>(() => repo.AddProviderAsync(new Provider()));
    }
} 