using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserOrderApp.Interface;
using UserOrderApp.Service;


public class AuthServiceTests
{
    [Fact]
    public async Task IsEmailRegisteredAsync_ReturnsTrue()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.IsEmailRegisteredAsync("user@example.com")).ReturnsAsync(true);

        var service = new AuthService(mockRepo.Object);
        var result = await service.IsEmailRegisteredAsync("user@example.com");

        Assert.True(result);
    }

    [Fact]
    public async Task IsEmailRegisteredAsync_ReturnsFalse()
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.IsEmailRegisteredAsync("new@example.com")).ReturnsAsync(false);

        var service = new AuthService(mockRepo.Object);
        var result = await service.IsEmailRegisteredAsync("new@example.com");

        Assert.False(result);
    }

    [Theory]
    [InlineData("        ")]
    [InlineData("Password!")]
    [InlineData(null)]        
    public void IsValidPassword_ShouldReturnFalse_ForInvalidEdgeCases(string password)
    {
        var service = new AuthService(null);
        var result = service.IsValidPassword(password ?? "");
        Assert.False(result);
    }
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("not-an-email")]
    public async Task IsEmailRegisteredAsync_ShouldReturnFalse_ForInvalidInput(string email)
    {
        var mockRepo = new Mock<IUserRepository>();
        mockRepo.Setup(r => r.IsEmailRegisteredAsync(It.IsAny<string>())).ReturnsAsync(false);

        var service = new AuthService(mockRepo.Object);

        var result = await service.IsEmailRegisteredAsync(email);
        Assert.False(result);
    }


}


