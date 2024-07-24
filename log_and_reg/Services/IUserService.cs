using log_and_reg.Models;
using System.Threading.Tasks;

namespace log_and_reg.Services
{
    public interface IUserService
    {
        Task Register(User user, string password);
        Task<User> Login(string username, string password);
    }
}
