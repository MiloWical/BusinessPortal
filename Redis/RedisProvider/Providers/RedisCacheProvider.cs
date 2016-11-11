using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RedisProvider.Providers
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly RedisConnection _redisConnection;
        private readonly string _cacheName;
        private readonly static Dictionary<Type, XmlSerializer> _serializerCache 
            = new Dictionary<Type, XmlSerializer>();

        public RedisCacheProvider(string connectionString, string cacheName)
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
            var obj = _redisConnection.GetValue(key);

            var objType = obj.GetType();

            XmlSerializer serializer;

            if (!_serializerCache.TryGetValue(objType, out serializer))
                _serializerCache.Add(objType, new XmlSerializer(objType));

            var writer = new StringWriter();

            _serializerCache[objType].Serialize(writer, obj);

            return writer.ToString();
        }
    }
}
