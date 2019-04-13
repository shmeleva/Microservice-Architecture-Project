using System;
using System.Threading.Tasks;
using Identity.Models;

namespace Identity.Services
{
    public interface IIdentityService
    {
        Task<User> CreateUserAsync(string username, string password);

        Task<string> IssueUserJwtAsync(string username, string password);
    }
}
