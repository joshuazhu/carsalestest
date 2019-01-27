using System.Collections.Generic;
using Application;
using Application.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarsalesTest.Application
{
    [TestClass]
    public class AuthenticationTest
    {
        private Dictionary<string, string> _tokens = new Dictionary<string, string>();

        [TestMethod]
        public void WhenTokenExists_ReturnExistsToken()
        {
            //Arrange 
            _tokens.Add("test@gmail.com", "token");
            var authenticationService = new AuthenticationService(_tokens);
            var toBeAuthenticatedUser = new UserAuthentication
            {
                Email = "test@gmail.com",
                Password = "test"
            };

            //Act
            var token = authenticationService.Authenticate(toBeAuthenticatedUser);

            //Assert
            Assert.AreEqual("token", token);
        }

        [TestMethod]
        public void WhenTokenNotExists_ReturnNewToken()
        {
            //Arrange 
            var authenticationService = new AuthenticationService(_tokens);
            var toBeAuthenticatedUser = new UserAuthentication
            {
                Email = "test@gmail.com",
                Password = "test"
            };

            //Act
            var token = authenticationService.Authenticate(toBeAuthenticatedUser);

            //Assert
            Assert.IsNotNull(token);
        }
    }
}
