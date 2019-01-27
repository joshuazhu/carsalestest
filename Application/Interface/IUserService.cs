using Application.Model;
using Domain.Entity;

namespace Application.Interface
{
    public interface IUserService
    {
        User CreateUser(UserDTO user);
        string AuthenticateUser(UserAuthentication user);
    }
}
