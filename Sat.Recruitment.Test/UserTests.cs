using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Entities;
using Sat.Recruitment.Api.Intefases;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Utils;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UserTests
    {
        //TODO change the testing strategy to inmemory 
        [Fact]
        public void CreateUserHappyPath()
        {
            //Arrange
            var user = new User()
            {
                Name = "Mike",
                Email= "mike@gmail.com",
                Address= "Av. Juan G",
                Phone= "+349 1122354215",
                UserType= "Normal",
                Money = decimal.Parse("124")
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            //Act
            var result = userService.CreateUserAsync(user).Result;

            //Assert
            Assert.True(result.IsSuccess);
        }
        
        [Fact]
        public void CreateUserDuplicated()
        {
            //Arrange
            var user = new User()
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };
            var userRepositoryMock = new Mock<IUserRepository>();

            var methodResult = new MethodResult();
            userRepositoryMock.Setup(x => x.IsDuplicatedAsync(user)).ReturnsAsync(true);

            var userService = new UserService(userRepositoryMock.Object);

            //Act
            var result = userService.CreateUserAsync(user).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The user is duplicated", result.Errors.ToString());
        }

        [Fact]
        public void UserIsNameEmpty()
        {
            //Arrange
            var user = new User()
            {
                Name = "",
                Email = "New@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            //Act
            var result = userService.CreateUserAsync(user).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The name is required", result.Errors.ToString());
        }

        [Fact]
        public void UserEmailEmpty()
        {
            //Arrange
            var user = new User()
            {
                Name = "new0",
                Email = "",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            //Act
            var result = userService.CreateUserAsync(user).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The email is required", result.Errors.ToString());
        }

        [Fact]
        public void UserAddressEmpty()
        {
            //Arrange
            var user = new User()
            {
                Name = "new1",
                Email = "new1@gmail.com",
                Address = "",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            //Act
            var result = userService.CreateUserAsync(user).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(("The address is required"), result.Errors.ToString());
        }
        [Fact]
        public void UserPhoneEmpty()
        {
            //Arrange
            var user = new User()
            {
                Name = "new2",
                Email = "new2@gmail.com",
                Address = "Av. Juan G",
                Phone = "",
                UserType = "Normal",
                Money = decimal.Parse("124")
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var userService = new UserService(userRepositoryMock.Object);

            //Act
            var result = userService.CreateUserAsync(user).Result;

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("The phone is required", result.Errors.ToString());
        }
    }
}
