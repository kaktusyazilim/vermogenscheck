using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.Cache
{
    public static class CacheManager
    {
        private static readonly List<string> _cacheKeys = new List<string>();

        // Cache Key'i ekler
        public static void AddCacheKey(string cacheKey)
        {
            if (!_cacheKeys.Contains(cacheKey))
            {
                _cacheKeys.Add(cacheKey);
            }
        }

        // Cache Key'i siler
        public static void RemoveCacheKey(string cacheKey)
        {
            if (_cacheKeys.Contains(cacheKey))
            {
                _cacheKeys.Remove(cacheKey);
            }
        }

        // Cache Key'leri temizler (Belirli bir Type için)
        public static void ClearCacheForType<T>(IMemoryCache memoryCache)
        {
            try
            {
                var startWiths = $"{typeof(T).Name}_list";
                var startWithList = $"{typeof(T).Name}_single";
                var keysToRemove = _cacheKeys.Where(key => key.StartsWith(startWiths) || key.StartsWith(startWithList)).ToList();
                foreach (var key in keysToRemove)
                {
                    memoryCache.Remove(key);
                    _cacheKeys.Remove(key);
                }
            }
            catch (Exception ex)
            {

            }
        }

        // Cache'i temizle (Belirli bir Cache Key için)
        public static void ClearSpecificCache(IMemoryCache memoryCache, string cacheKey)
        {
            memoryCache.Remove(cacheKey);
            _cacheKeys.Remove(cacheKey);
        }

        // Tüm Cache'leri temizle
        public static void ClearAllCache(IMemoryCache memoryCache)
        {
            foreach (var key in _cacheKeys)
            {
                memoryCache.Remove(key);
            }
            _cacheKeys.Clear();
        }

        // Cache Key'leri listele
        public static List<string> GetCacheKeys()
        {
            return _cacheKeys;
        }

        // Remove Cache
        public static void RemoveCache(IMemoryCache memoryCache, string cacheKey)
        {
            memoryCache.Remove(cacheKey);
            _cacheKeys.Remove(cacheKey);
        }


    }

}
