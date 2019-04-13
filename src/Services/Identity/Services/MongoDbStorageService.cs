using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;

namespace Identity.Services
{
    public class MongoDbStorageService : IStorageService
    {
        private readonly List<User> users = new List<User>();


        public MongoDbStorageService()
        {
        }


        public async Task InsertUserAsync(User user)
        {
            await Task.Delay(0);
            users.Add(user);
        }

        public async Task<User> FindUserAsync(string username)
        {
            await Task.Delay(0);
            return users.FirstOrDefault(x => x.Username == username);
        }
    }
}
