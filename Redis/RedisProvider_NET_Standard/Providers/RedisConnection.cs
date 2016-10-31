using RedisProvider.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisProvider.Providers
{
    using StackExchange.Redis;

    public class RedisConnection
    {
        private string _cacheName;

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        private ConnectionMultiplexer Connection
        {
            get
            {
                return _lazyConnection.Value;
            }
        }

        public ISubscriber Subscriber
        {
            get
            {
                return Connection.GetSubscriber();
            }
        }

        public RedisConnection(string connectionString, string cacheName)
        {
             _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
             {
                 return ConnectionMultiplexer.Connect(connectionString);
             });

            _cacheName = Cache.partnerdatacache.ToString();
        }


        public List<string> ReadKeys(string match = null)
        {
            var keys = new List<string>();

            try
            {
                foreach (var endpoint in Connection.GetEndPoints())
                {
                    var server = Connection.GetServer(endpoint);

                    var dbId = (int)Enum.Parse(typeof(CacheName), _cacheName.ToLower());

                    foreach (var key in server.Keys(dbId))
                        if (match == null || key.ToString().Contains(match))
                            keys.Add(key);
                }

                keys.Sort();

                return keys;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PutValue(string cacheKey, object cacheValue)
        {
            try
            {
                foreach (var endpoint in Connection.GetEndPoints())
                {
                    var dbId = (int)Enum.Parse(typeof(CacheName), _cacheName.ToLower());

                    var cache = Connection.GetDatabase(dbId);

                    cache.StringSet(cacheKey, cacheValue.Serialize());
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public byte[] GetValue(string cacheKey)
        {
            //PDS::GetBrand::134::Admiral.Services.PartnerData.Contract, Version=7.0.0.0, Culture=neutral, PublicKeyToken=8fd4efed560dfb20

            try
            {
                foreach (var endpoint in Connection.GetEndPoints())
                {
                    var dbId = (int)Enum.Parse(typeof(CacheName), _cacheName.ToLower());

                    var cache = Connection.GetDatabase(dbId);

                    byte[] data = cache.StringGet(cacheKey);

                    return data;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public void RemoveValue(string cacheKey)
        {
            //PDS::GetBrand::134::Admiral.Services.PartnerData.Contract, Version=7.0.0.0, Culture=neutral, PublicKeyToken=8fd4efed560dfb20

            try
            {
                foreach (var endpoint in Connection.GetEndPoints())
                {
                    var dbId = (int)Enum.Parse(typeof(CacheName), _cacheName.ToLower());

                    var cache = Connection.GetDatabase(dbId);

                    cache.KeyDelete(cacheKey);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clears content of a NamedCache.
        /// </summary>
        /// <param name="cacheName">Named Cache Name</param>
        public void ClearCache(string cacheName = "ALL")
        {
            try
            {
                foreach (var endpoint in Connection.GetEndPoints())
                {
                    var server = Connection.GetServer(endpoint);
                    if (cacheName == "ALL")
                    {
                        server.FlushAllDatabases();
                    }
                    else if (cacheName != "")
                    {
                        var dbId = (int)Enum.Parse(typeof(CacheName), _cacheName.ToLower());

                        server.FlushDatabase(dbId);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Clears contents of a region inside a NamedCache.
        /// </summary>
        /// <param name="cacheName">Named Cache Name</param>
        /// <param name="regionName">Region Name</param>
        public void ClearCacheRegion(string regionName)
        {
            try
            {
                foreach (var endpoint in Connection.GetEndPoints())
                {
                    var server = Connection.GetServer(endpoint);
                    var dbId = (int)Enum.Parse(typeof(CacheName), _cacheName.ToLower());
                    var cache = Connection.GetDatabase(dbId);

                    var keys = server.Keys(dbId, pattern: regionName + "_*");
                    if (keys.LongCount() > 0)
                    {
                        foreach (var key in keys)
                        {
                            cache.KeyDelete(key);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
