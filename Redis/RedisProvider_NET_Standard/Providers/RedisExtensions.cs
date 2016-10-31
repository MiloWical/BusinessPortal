namespace RedisProvider.Providers
{
    using System.IO;
    //using System.Runtime.Serialization.Formatters.Binary;
    using BinaryFormatter;

    public static class RedisExtensions
    {
        //These serialization routines were lifted from Admiral.Components.ServiceProxy.RedisCacheExtensions

        public static byte[] Serialize(this object o)
        {
            if (o == null)
            {
                return null;
            }

            //var binaryFormatter = new BinaryFormatter();
            var binaryFormatter = new BinaryConverter();

            using (var memoryStream = new MemoryStream())
            {
                //binaryFormatter.Serialize(memoryStream, o);
                //var objectDataAsStream = memoryStream.ToArray();
                //return objectDataAsStream;
                return binaryFormatter.Serialize(o);
            }
        }

        public static T Deserialize<T>(this byte[] stream)
        {
            if (stream == null)
            {
                return default(T);
            }

            //var binaryFormatter = new BinaryFormatter();
            var binaryFormatter = new BinaryConverter();

            using (var memoryStream = new MemoryStream(stream))
            {
                //var result = (T)binaryFormatter.Deserialize(memoryStream);
                var result = binaryFormatter.Deserialize<T>(memoryStream.ToArray());
                return result;
            }
        }
    }
}
