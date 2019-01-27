using System;
using Application.Interface;
using Application.Model;
using Carsales.Model;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Carsales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService { get; set; }

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("create")]
        public ResponseViewModel<User> Create([FromBody] UserDTO user)
        {
            try
            {
                var newUser = _userService.CreateUser(user);

                return new ResponseViewModel<User>
                {
                    Success = true,
                    Data = newUser,
                    Message = ""
                };

            }
            catch (Exception e)
            {
                return new ResponseViewModel<User>
                {
                    Success = false,
                    Data = null,
                    Message = e.Message
                };
            }
        }

        [HttpPost("authenticate")]
        public ResponseViewModel<string> Authenticate([FromBody] UserAuthentication user)
        {
            try
            {
                var newUser = _userService.AuthenticateUser(user);

                return new ResponseViewModel<string>
                {
                    Success = true,
                    Data = newUser,
                    Message = ""
                };

            }
            catch (Exception e)
            {
                return new ResponseViewModel<string>
                {
                    Success = false,
                    Data = null,
                    Message = e.Message
                };
            }
        }
    }
}
