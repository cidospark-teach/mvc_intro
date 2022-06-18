using System.Collections.Generic;
using Magazine011.Models;

namespace Magazine011.Services
{
    public interface IUserService
    {
        public List<User> Users { get; }
        public User GetUserById(string id);
        public User GetUserByEmail(string email);
        public bool UpdateUser(User user);
        public string AddUser(User user);
    }
}