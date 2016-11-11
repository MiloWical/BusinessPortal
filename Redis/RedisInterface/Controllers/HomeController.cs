using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedisProvider.Providers;

namespace RedisInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICacheProvider _cacheProvider;

        public HomeController() : base()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            _cacheProvider = new RedisCacheProvider(
                configuration.GetSection("ConnectionStrings").GetValue<string>("RedisCache"),
                "partnerdatacache");
        }

        public IActionResult Index()
        {
            return View(_cacheProvider.GetKeys());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
