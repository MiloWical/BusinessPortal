using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RedisProvider.Providers
{
    public class RedisProvider : ICacheProvider
    {
        private readonly RedisConnection _redisConnection;
        private readonly string _cacheName;

        public RedisProvider(string connectionString, string cacheName)
        {
            _cacheName = cacheName;
            _redisConnection = new RedisConnection(connectionString, cacheName);
        }

        public bool ClearCache()
        {
            try
            {
                _redisConnection.ClearCache(_cacheName);
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

        public bool ClearCacheKey(string key)
        {
            try
            {
                _redisConnection.RemoveValue(key);
            }
            catch (Exception)
            {
                return false;                
            }

            return true;
        }

        public IEnumerable<string> GetKeys()
        {
            return _redisConnection.ReadKeys();
        }

        public string GetKeyValue(string key)
        {
            var valueBytes = _redisConnection.GetValue(key).ToArray<byte>();

            return valueBytes != null ? System.Text.Encoding.UTF8.GetString(valueBytes) : null;
        }
    }
}
