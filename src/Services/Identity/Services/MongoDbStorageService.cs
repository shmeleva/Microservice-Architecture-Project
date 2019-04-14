using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Identity.Services
{
    public class MongoDbStorageService : IStorageService
    {
        private readonly IMongoCollection<User> users;


        public MongoDbStorageService(IConfiguration config)
        {
            var client = new MongoClient("mongodb://mongo-identity:27017");
            var database = client.GetDatabase("identity");
            users = database.GetCollection<User>("Users");
        }


        public async Task InsertUserAsync(User user) =>
            await users.InsertOneAsync(user);

        public async Task<User> FindUserAsync(string username) =>
            await users.Find(x => x.Username == username).FirstOrDefaultAsync();
    }
}
