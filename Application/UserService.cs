using Application.Exception;
using Application.Interface;
using Application.Model;
using AutoMapper;
using Domain.Entity;
using Repository.Interface;

namespace Application
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository { get; set; }
        private IAuthenticationService _authenticateionService { get; set; }
        private IMapper _mapper { get; set; }

        public UserService(IUserRepository userRepository, IAuthenticationService authenticationService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticateionService = authenticationService;
            _mapper = mapper;
        }

        public User CreateUser(UserDTO user)
        {
            if (user == null)
                throw new InvalidDataException("Cannot create user for empty data");

            var existingUser = _userRepository.Get(user.Email, user.Password);

            if(existingUser != null)
                throw new UserExistsException("User with the same username and password combination is already exist");

            var newUser = _userRepository.CreateUser(_mapper.Map<User>(user));

            return newUser;
        }

        public string AuthenticateUser(UserAuthentication user)
        {
            var existingUser = _userRepository.Get(user.Email, user.Password);

            if(existingUser == null)
                throw new InvalidDataException("Cannot find user for the username and password combination");

            var token = _authenticateionService.Authenticate(user);

            return token;
        }
    }
}
