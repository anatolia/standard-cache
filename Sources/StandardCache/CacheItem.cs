using System;

namespace StandardCache
{
    public class CacheItem
    {
        public string Key { get; }
        public object Item { get; private set; }

        public DateTime CreatedAt { get; }
        public DateTime LastUpdatedAt { get; private set; }
        public DateTime LastAccessedAt { get; private set; }
        public DateTime ExpiresAt { get; private set; }

        public CacheItem(string key, object item, DateTime expiresAt)
        {
            Key = key;
            Item = item;
            ExpiresAt = expiresAt;
            CreatedAt = DateTime.UtcNow;
        }

        public CacheItem(string key, object item) : this(key, item, DateTime.UtcNow)
        {

        }

        internal void Touch()
        {
            LastAccessedAt = DateTime.UtcNow;
        }

        public bool IsExpired()
        {
            return ExpiresAt > DateTime.UtcNow;
        }

        public CacheItem Update(CacheItem item)
        {
            Item = item.Item;
            ExpiresAt = item.ExpiresAt;
            LastUpdatedAt = DateTime.UtcNow;

            return this;
        }

        public void ExtendExpiration(double durationInMinutes)
        {
            ExpiresAt = ExpiresAt.AddMinutes(durationInMinutes);
            LastUpdatedAt = DateTime.UtcNow;
        }
    }
}