using Xunit;
using Moq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Domain.Interfaces;

namespace CleanArchitecture.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task CreateAsync_ReturnsUserId_WhenUserIsValid()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.CreateAsync(It.IsAny<User>())).ReturnsAsync(1);
        var service = new UserService(mockRepo.Object);
        var user = new User { Username = "test", Email = "test@test.com", PasswordHash = "hash", FirstName = "Test", LastName = "User", Role = "USER", IsActive = true, CreatedAt = System.DateTime.UtcNow };
        var result = await service.CreateAsync(user);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task GetByUsernameOrEmailAsync_ReturnsNull_WhenUserNotFound()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        mockRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        var service = new UserService(mockRepo.Object);
        var result = await service.GetByUsernameOrEmailAsync("notfound");
        Assert.Null(result);
    }
} 