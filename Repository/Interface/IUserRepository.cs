using Domain.Entity;

namespace Repository.Interface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User Get(string email, string password);
    }
}
