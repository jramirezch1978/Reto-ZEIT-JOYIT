using Xunit;
using Moq;
using System.Data;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Data;

namespace CleanArchitecture.Tests;

public class PartnerRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenPartnerDoesNotExist()
    {
        // Arrange
        var mockDbConnection = new Mock<IDbConnection>();
        var mockDatabaseConnection = new Mock<DatabaseConnection>(null!);
        mockDatabaseConnection.Setup(db => db.CreateConnection()).Returns(mockDbConnection.Object);
        var repo = new PartnerRepository(mockDatabaseConnection.Object);
        // Act
        var result = await repo.GetByIdAsync(999);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ThrowsException_WhenPartnerIsNull()
    {
        var mockDbConnection = new Mock<IDbConnection>();
        var mockDatabaseConnection = new Mock<DatabaseConnection>(null!);
        mockDatabaseConnection.Setup(db => db.CreateConnection()).Returns(mockDbConnection.Object);
        var repo = new PartnerRepository(mockDatabaseConnection.Object);
        await Assert.ThrowsAsync<System.ArgumentNullException>(() => repo.CreateAsync(null!));
    }
} 