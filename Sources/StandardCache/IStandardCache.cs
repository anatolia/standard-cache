namespace StandardCache
{
    public interface IStandardCache
    {
        void AddOrUpdate(string category, CacheItem item);
        CacheItem Get(string category, string key);
        void Remove(string category, string key);
        void ClearExpired(string category);
        void Clear(string category);
    }
}