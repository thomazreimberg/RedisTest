namespace APIWithRedis.Domain.Entities
{
    public class User
    {
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public User(string name, string username, string password)
        {
            Name = name;
            Username = username ?? throw new ArgumentNullException(username);
            Password = password ?? throw new ArgumentNullException(password);
        }
    }
}
