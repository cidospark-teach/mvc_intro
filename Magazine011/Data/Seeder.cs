using System;
using System.Collections.Generic;
using System.IO;
using Magazine011.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Magazine011.Data
{
    public class Seeder
    {
        public Seeder()
        {
        }

        public static List<User> ReadMe(IConfiguration config)
        {
            var data = File.ReadAllText(config.GetSection("DummyFilePath:Path").Value);

            var users = JsonConvert.DeserializeObject<List<User>>(data);

            return users;
        }

        public static bool WriteMe(List<User> users, IConfiguration config)
        {
            //var data = File.ReadAllText("Data/ClassMembers.json");

            var data = JsonConvert.SerializeObject(users);

            File.WriteAllText(config.GetSection("DummyFilePath:Path").Value, data);

            return true;
        }
    }
}
