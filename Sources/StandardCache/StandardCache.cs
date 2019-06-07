using System.Collections.Concurrent;

namespace StandardCache
{
    public class StandardCache : IStandardCache
    {
        private readonly ConcurrentDictionary<string, ConcurrentDictionary<string, CacheItem>> Items = new ConcurrentDictionary<string, ConcurrentDictionary<string, CacheItem>>();

        public void AddOrUpdate(string category, CacheItem item)
        {
            var categoryData = GetOrAddCategory(category);
            categoryData.AddOrUpdate(item.Key, item, (key, oldItem) => oldItem.Update(item));
        }

        public CacheItem Get(string category, string key)
        {
            if (!Items.TryGetValue(category, out var categoryItems))
            {
                return null;
            }

            if (!categoryItems.TryGetValue(key, out var item))
            {
                return null;
            }

            item.Touch();
            return item;
        }

        public void Remove(string category, string key)
        {
            if (Items.TryGetValue(category, out var categoryItems))
            {
                categoryItems.TryRemove(key, out var item);
            }
        }

        public void ClearExpired(string category)
        {
            if (Items.TryGetValue(category, out var categoryItems))
            {
                foreach (var item in categoryItems.Values)
                {
                    if (item.IsExpired())
                    {
                        Items.TryRemove(item.Key, out var removedItem);
                    }
                }
            }
        }

        public void Clear(string category)
        {
            if (Items.TryRemove(category, out var categoryItems))
            {
                categoryItems.Clear();
            }
        }

        public ConcurrentDictionary<string, CacheItem> GetOrAddCategory(string category) => Items.GetOrAdd(category, key => new ConcurrentDictionary<string, CacheItem>());
    }
}
