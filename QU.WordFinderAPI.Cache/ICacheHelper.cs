

namespace QU.WordFinderAPI.Cache
{
    public interface ICacheHelper
    {
        T? Get<T>(string key);
        bool Set<T>(string key, T value, TimeSpan? expiry);
        bool Delete(string key);
    }
}
