using APIWithRedis.Domain.Entities;
using APIWithRedis.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace APIWithRedis.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDistributedCache _redis;

        public UserRepository(IDistributedCache redis)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public async Task<User> Add(User user)
        {
            await _redis.SetStringAsync(user.Username, JsonSerializer.Serialize(user));
            return user;
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var data = await _redis.GetStringAsync(username);

            if (string.IsNullOrEmpty(data)) return null;

            var user = JsonSerializer.Deserialize<User>(data);

            return user;
        }
    }
}
