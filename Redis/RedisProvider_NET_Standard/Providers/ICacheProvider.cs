using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisProvider.Providers
{
    /// <summary>
    /// Gives a collection of basic cache operations 
    /// </summary>
    interface ICacheProvider
    {
        /// <summary>
        /// Pulls all keys from the cache.
        /// </summary>
        /// <returns>An IEnumerable of all keys</returns>
        IEnumerable<string> GetKeys();

        /// <summary>
        /// Pulls the value of a specified key from the cache.
        /// </summary>
        /// <param name="key">The key to query</param>
        /// <returns>The value store in the key; null otherwise</returns>
        string GetKeyValue(string key);

        /// <summary>
        /// Clears the cache of all keys.
        /// </summary>
        /// <returns>True if the clear was successful, false otherwise</returns>
        bool ClearCache();

        /// <summary>
        /// Clears the specified key from the cache.
        /// </summary>
        /// <param name="key">The key to remove</param>
        /// <returns>True if the clear was successful, false otherwise</returns>
        bool ClearCacheKey(string key);
    }
}
