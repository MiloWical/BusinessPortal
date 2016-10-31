using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedisProvider.Providers;
using Microsoft.Extensions.Configuration;
using System.Text;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisInterface.Controllers
{
    public class RedisApiController : Controller
    {
        private readonly ICacheProvider _cacheProvider;

        public RedisApiController() : base()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            _cacheProvider = new RedisCacheProvider(
                configuration.GetSection("ConnectionStrings").GetValue<string>("RedisCache"), 
                "partnerdatacache");
        }

        // GET: /<controller>/
        public IEnumerable<string> GetKeys()
        {
            return _cacheProvider.GetKeys();
        }

        public string GetCacheKeyValue(string key)
        {
            return _cacheProvider.GetKeyValue(key);
        }

        //[HttpGet]
        public string TestGet()
        {
            return "Test Get";
        }

        //[HttpGet]
        public string TestGetWithParam(string param)
        {
            return "TestGetWithParam -> You entered: " + param;
        }

        [HttpPost]
        public string TestPost()
        {
            return "Test Post";
        }

        [HttpPost]
        public string TestPostWithParam(string param)
        {
            return "TestPostWithParam -> You entered: " + param;
        }
    }
}
