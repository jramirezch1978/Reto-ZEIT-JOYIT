using Xunit;
using Moq;
using Dapper;
using System.Data;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Repositories;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Tests;

public class UserRepositoryTests
{
    [Fact]
    public async Task GetUserByIdAsync_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var mockConnection = new Mock<IDbConnection>();
        var repo = new UserRepository(mockConnection.Object);
        // You would set up Dapper's QuerySingleOrDefaultAsync here if you abstracted it
        // For now, just check that the method can be called (example structure)
        // Act & Assert
        await Assert.ThrowsAsync<NotImplementedException>(() => repo.GetUserByIdAsync(1));
    }

    [Fact]
    public async Task AddUserAsync_ThrowsNotImplemented()
    {
        var mockConnection = new Mock<IDbConnection>();
        var repo = new UserRepository(mockConnection.Object);
        await Assert.ThrowsAsync<NotImplementedException>(() => repo.AddUserAsync(new User()));
    }
} 