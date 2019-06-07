using System;
using NUnit.Framework;

namespace StandardCache.Tests
{
    [TestFixture]
    public class StandardCacheUnitTests
    {
        const string CACHE_CATEGORY = "test";

        [Test]
        public void StandardCache_AddOrUpdate()
        {
            // arrange
            var standardCache = GetStandardCache();
            var item = GetCacheItem();

            // act
            standardCache.AddOrUpdate(CACHE_CATEGORY, item);

            // assert
            var cacheItem = standardCache.Get(CACHE_CATEGORY, item.Key);
            Assert.IsNotNull(cacheItem);
            Assert.AreEqual(cacheItem.Item, item.Item);
        }

        [Test]
        public void StandardCache_Remove()
        {
            // arrange
            var standardCache = GetStandardCache();
            var item = GetCacheItem();
            standardCache.AddOrUpdate(CACHE_CATEGORY, item);

            // act
            standardCache.Remove(CACHE_CATEGORY, item.Key);

            // assert
            var cacheItem = standardCache.Get(CACHE_CATEGORY, item.Key);
            Assert.IsNull(cacheItem);
        }

        private static CacheItem GetCacheItem()
        {
            var item = new CacheItem("test-key", "test-item");
            return item;
        }

        private static StandardCache GetStandardCache()
        {
            var standardCache = new StandardCache();
            return standardCache;
        }
    }
}
