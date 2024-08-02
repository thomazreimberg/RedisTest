using APIWithRedis.Domain.Entities;

namespace APIWithRedis.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsername(string username);
        Task<User> Add(User user);
    }
}
