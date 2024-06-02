using StackExchange.Redis;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace QU.WordFinderAPI.Cache
{
    public class RedisCacheHelper : ICacheHelper
    {
        private readonly IDatabase _database;
        public RedisCacheHelper(IOptions<RedisOptions> redis)
        {
            _database = ConnectionMultiplexer.Connect(
                new ConfigurationOptions
                {
                    EndPoints = { redis.Value.Server }
                }).GetDatabase();
        }

        public T? Get<T>(string key)
        {
            RedisValue value = _database.StringGet(key);
            return value.IsNullOrEmpty ? default(T?) : JsonConvert.DeserializeObject<T>(value);
        }

        public bool Set<T>(string key, T value, TimeSpan? expiry)
        {
            return _database.StringSet(key, JsonConvert.SerializeObject(value), expiry);
        }

        public bool Delete(string key)
        {
            return _database.KeyDelete(key);
        }
    }
}
