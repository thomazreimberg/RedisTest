using StackExchange.Redis;

internal class Program
{
    private static void Main(string[] args)
    {
        // Connect to the Redis server
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        // Get a reference to the Redis database
        IDatabase db = redis.GetDatabase();

        db.StringSet("mykey", "Hello, Redis!");
        // Retrieve a value by key
        string value = db.StringGet("mykey");

        // Create a Redis Hash
        HashEntry[] hashEntries = new HashEntry[]
        {
            new HashEntry("field1", "value1"),
            new HashEntry("field2", "value2")
        };
        db.HashSet("myhash", hashEntries);
        // Retrieve a specific field from the Hash
        string fieldValue = db.HashGet("myhash", "field1");

        // Add items to a Redis List
        db.ListLeftPush("mylist", "item1");
        db.ListLeftPush("mylist", "item2");
        // Retrieve all items from the List
        RedisValue[] listItems = db.ListRange("mylist");

        // Disconnect from Redis

        Console.WriteLine(fieldValue);
        redis.Close();

        Console.ReadKey();
    }
}