using System;
using Xunit;
using Moq;
using UserOrderApp.Interface;
using UserOrderApp.Service;
using UserOrderApp.Model;

namespace UserOrderApp.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public void GetUserById_ReturnsCorrectUser()
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(1)).Returns(new User
            {
                Id = 1,
                Email = "test@example.com",
                Password = "Test1234"
            });

            var userService = new UserService(mockRepo.Object);

            var result = userService.GetUserById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public void GetUserById_ReturnsNull_WhenNotFound()
        {

            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(repo => repo.GetUserById(999)).Returns((User)null);

            var userService = new UserService(mockRepo.Object);

            var result = userService.GetUserById(999);

            Assert.Null(result);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MaxValue)]
        public void GetUserById_ShouldReturnNull_WhenIdIsEdgeCase(int id)
        {
            var mockRepo = new Mock<IUserRepository>();
            mockRepo.Setup(r => r.GetUserById(id)).Returns((User)null);

            var service = new UserService(mockRepo.Object);

            var result = service.GetUserById(id);

            Assert.Null(result);
        }

    }
}
