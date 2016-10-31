namespace RedisProvider.Providers
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class RedisExtensions
    {
        //These serialization routines were lifted from Admiral.Components.ServiceProxy.RedisCacheExtensions

        public static byte[] Serialize(this object o)
        {
            if (o == null)
            {
                return null;
            }

            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, o);
                var objectDataAsStream = memoryStream.ToArray();
                return objectDataAsStream;
            }
        }

        public static T Deserialize<T>(this byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream(stream))
            {
                var result = (T)binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }

        public static object Deserialize(this byte[] stream)
        {
            if (stream == null)
            {
                return null;
            }

            var binaryFormatter = new BinaryFormatter();

            using (var memoryStream = new MemoryStream(stream))
            {
                return binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}
