using DanceConnect.Server.Entities;

namespace DanceConnect.Server.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync(User user);
    }
}
