using System;
using System.Collections.Generic;
using System.Linq;
using Magazine011.Data;
using Magazine011.Models;
using Microsoft.Extensions.Configuration;

namespace Magazine011.Services
{
    public class UserService : IUserService
    {
        public List<User> Users { get; private set; }

        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            Users = Seeder.ReadMe(config);
            _config = config;
        }

        public User GetUserById(string id)
        {
             return Seeder.ReadMe(_config).FirstOrDefault(x => x.Id == id);
        }

        public bool UpdateUser(User user)
        {
            foreach(var member in Users)
            {
                if(member.Id == user.Id)
                {
                    member.Name = user.Name;
                    member.Password = user.Password;
                    member.Role = user.Role;
                    member.Photo = user.Photo;
                }
            }

            // write users to json
            return Seeder.WriteMe(Users, _config);

        }

        public User GetUserByEmail(string email)
        {
            return Seeder.ReadMe(_config).FirstOrDefault(x => x.Email == email);
        }

        public string AddUser(User user)
        {
            var userId = Guid.NewGuid().ToString();

            user.Id = userId;

            Users.Add(user);

            // write to the json file using seeder
            Seeder.WriteMe(Users, _config);

            return userId;

        }
    }
}


/*
 
 public List<User> Users {
            get {
                return Seeder.SeedMe();
            }
            private set { }
        }
 
 
 */