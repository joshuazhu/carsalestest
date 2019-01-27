using Application.Model;

namespace Application.Interface
{
    public interface IAuthenticationService
    {
        string Authenticate(UserAuthentication user);
    }
}
