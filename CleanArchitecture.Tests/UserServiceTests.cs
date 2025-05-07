using Xunit;
using Moq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Tests;

public class UserServiceTests
{
    [Fact]
    public async Task CreateUserAsync_ReturnsUserId_WhenUserIsValid()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.AddUserAsync(It.IsAny<User>())).ReturnsAsync(1);
        var service = new UserService(mockRepo.Object);
        var user = new User { Username = "test", Email = "test@test.com" };
        var result = await service.CreateUserAsync(user);
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task AuthenticateUserAsync_ReturnsNull_WhenUserNotFound()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.GetUserByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
        var service = new UserService(mockRepo.Object);
        var result = await service.AuthenticateUserAsync("notfound", "password");
        Assert.Null(result);
    }
} 