using System.Threading.Tasks;
using Identity.Models;

namespace Identity.Services
{
    public interface IStorageService
    {
        Task InsertUserAsync(User user);

        Task<User> FindUserAsync(string username);
    }
}
