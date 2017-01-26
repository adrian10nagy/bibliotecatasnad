

namespace BL.Cache
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Caching;

    public class CacheHelper
    {
        private static CacheHelper _instance = new CacheHelper();
        public static CacheHelper Instance { get { return _instance ?? getMyCache(); } }

        static CacheHelper getMyCache()
        {
            if (_instance != null)
            {
                _instance = new CacheHelper();
            }

            return _instance;
        }

        // Gets a reference to the default MemoryCache instance. 
        private static ObjectCache cache = MemoryCache.Default;
        private CacheItemPolicy policy = null;
        private CacheEntryRemovedCallback callback = null;

        public void AddToMyCacheWithFilePath(
            String CacheKeyName,
            Object CacheItem,
            MyCachePriority MyCacheItemPriority,
            List<String> FilePath)
        {
            callback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback);
            policy = new CacheItemPolicy();
            policy.Priority = (MyCacheItemPriority == MyCachePriority.Default) ?
                    CacheItemPriority.Default : CacheItemPriority.NotRemovable;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00);
            policy.RemovedCallback = callback;
            policy.ChangeMonitors.Add(new HostFileChangeMonitor(FilePath));

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }

        public void AddToMyCache(
            String CacheKeyName,
            Object CacheItem,
            MyCachePriority MyCacheItemPriority)
        {
            callback = new CacheEntryRemovedCallback(this.MyCachedItemRemovedCallback);
            policy = new CacheItemPolicy();
            policy.Priority = (MyCacheItemPriority == MyCachePriority.Default) ?
                    CacheItemPriority.Default : CacheItemPriority.NotRemovable;
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(120.00);
            policy.RemovedCallback = callback;

            // Add inside cache 
            cache.Set(CacheKeyName, CacheItem, policy);
        }

        public Object GetMyCachedItem(String CacheKeyName)
        {
            return cache[CacheKeyName] as Object;
        }

        public void RemoveMyCachedItem(String CacheKeyName)
        {
            if (cache.Contains(CacheKeyName))
            {
                cache.Remove(CacheKeyName);
            }
        }

        private void MyCachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
        {
            String strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), " | Key-Name: ", arguments.CacheItem.Key, " | Value-Object: ",
            arguments.CacheItem.Value.ToString());
        }
    }
}
