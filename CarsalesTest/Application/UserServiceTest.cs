using System.Collections.Generic;
using System.Linq;
using Application;
using Application.Exception;
using Application.Interface;
using Application.Model;
using Application.Profile;
using AutoMapper;
using Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.Interface;

namespace CarsalesTest.Application
{
    [TestClass]
    public class UserServiceTest
    {
        private Mock<IUserRepository> mockUserRepository { get; set; }
        private Mock<IAuthenticationService> mockAuthenticationService { get; set; }
        private IMapper mapper { get; set; }
        private List<User> userList = new List<User>();

        [TestInitialize]
        public void Setup()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockAuthenticationService = new Mock<IAuthenticationService>();

            var config = new MapperConfiguration(x => x.AddProfile(new MappingProfile()));
            mapper = new Mapper(config);
        }

        [TestClass]
        public class CreateUserTest : UserServiceTest
        {
            [TestMethod]
            public void WhenParameterIsNow_ThrowInvalidDataException()
            {
                //Arrange
                var userService = new UserService(mockUserRepository.Object, mockAuthenticationService.Object, mapper);

                //Action
                //Assert
                Assert.ThrowsException<InvalidDataException>(() => userService.CreateUser(null));
            }

            [TestMethod]
            public void WhenHasExistingUser_ThrowUserExistsException()
            {
                //Arrange
                mockUserRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns((string email, string password) => new User
                    {
                        Email = email,
                        Password = password
                    });

                var toBeCreatedUser = new UserDTO
                {
                    Email = "test@gmail.com",
                    Password = "test",
                    FirstName = "firstname",
                    LastName = "lastname"
                };

                var userService = new UserService(mockUserRepository.Object, mockAuthenticationService.Object, mapper);

                //Action
                //Assert
                Assert.ThrowsException<UserExistsException>(() => userService.CreateUser(toBeCreatedUser));
            }

            [TestMethod]
            public void WhenNewUser_AddUserToList()
            {
                //Arrange
                mockUserRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns((string email, string password) => null);

                mockUserRepository.Setup(x => x.CreateUser(It.IsAny<User>()))
                    .Callback((User u) => userList.Add(u));


                var toBeCreatedUser = new UserDTO
                {
                    Email = "test@gmail.com",
                    Password = "test",
                    FirstName = "firstname",
                    LastName = "lastname"
                };

                var userService = new UserService(mockUserRepository.Object, mockAuthenticationService.Object, mapper);

                //Action
                userService.CreateUser(toBeCreatedUser);

                //Assert
                Assert.AreEqual(1, userList.Count);
                Assert.IsNotNull(userList.FirstOrDefault(x => x.Email == toBeCreatedUser.Email));
            }
        }

        [TestClass]
        public class AuthenticateUserTest : UserServiceTest
        {
            [TestMethod]
            public void WhenUserNotExists_ThrowInvalidDataException()
            {
                //Arrange
                mockUserRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns((string email, string password) => null);

                var toBeAuthenticatedUser = new UserAuthentication()
                {
                    Email = "test@gmail.com",
                    Password = "test"
                };

                var userService = new UserService(mockUserRepository.Object, mockAuthenticationService.Object, mapper);

                //Action
                //Assert
                Assert.ThrowsException<InvalidDataException>(() => userService.AuthenticateUser(toBeAuthenticatedUser));

            }

            [TestMethod]
            public void WhenUserExists_GetToken()
            {
                //Arrange
                var user = new User
                {
                    Email = "test@gmail.com",
                    Password = "test",
                    FirstName = "firstname",
                    LastName = "lastname"
                };

                var toBeAuthenticatedUser = new UserAuthentication()
                {
                    Email = "test@gmail.com",
                    Password = "test"
                };

                mockUserRepository.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns((string email, string password) => user);

                mockAuthenticationService.Setup(x => x.Authenticate(It.IsAny<UserAuthentication>()))
                    .Returns(() => "token");

                var userService = new UserService(mockUserRepository.Object, mockAuthenticationService.Object, mapper);

                //Action
                var token = userService.AuthenticateUser(toBeAuthenticatedUser);

                //Assert
                Assert.IsNotNull(token);
            }
        }
    }
}
