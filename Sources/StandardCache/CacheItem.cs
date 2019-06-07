using NodaTime;

namespace StandardCache
{
    public class CacheItem
    {
        public string Key { get; }
        public object Item { get; private set; }

        public Instant CreatedAt { get; }
        public Instant LastUpdatedAt { get; private set; }
        public Instant LastAccessedAt { get; private set; }
        public Instant ExpiresAt { get; private set; }

        public CacheItem(string key, object item, Instant expiresAt)
        {
            Key = key;
            Item = item;
            ExpiresAt = expiresAt;
            CreatedAt = SystemClock.Instance.GetCurrentInstant();
        }

        public CacheItem(string key, object item) : this(key, item, SystemClock.Instance.GetCurrentInstant())
        {

        }

        internal void Touch()
        {
            LastAccessedAt = SystemClock.Instance.GetCurrentInstant();
        }

        public bool IsExpired()
        {
            return ExpiresAt > SystemClock.Instance.GetCurrentInstant();
        }

        public CacheItem Update(CacheItem item)
        {
            Item = item.Item;
            ExpiresAt = item.ExpiresAt;
            LastUpdatedAt = SystemClock.Instance.GetCurrentInstant();

            return this;
        }

        public void ExtendExpiration(Duration duration)
        {
            ExpiresAt = ExpiresAt.Plus(duration);
            LastUpdatedAt = SystemClock.Instance.GetCurrentInstant();
        }
    }
}