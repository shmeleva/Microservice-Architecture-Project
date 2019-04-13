using System;
using System.Threading.Tasks;
using Identity.Models;

namespace Identity.Services
{
    public class SqlServerStorageService : IStorageService
    {
        public SqlServerStorageService()
        {
        }

        public async Task<User> FindUserAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task InsertUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
