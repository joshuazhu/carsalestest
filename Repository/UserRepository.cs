using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entity;
using Repository.Interface;

namespace Repository
{
    public class UserRepository :IUserRepository
    {
        private List<User> _users = new List<User>();

        public User CreateUser(User user)
        {
            user.Id = Guid.NewGuid();
            _users.Add(user);

            return user;
        }

        public User Get(string email, string password)
        {
            return _users.FirstOrDefault(x => x.Email == email && x.Password == password);
        }
    }
}
