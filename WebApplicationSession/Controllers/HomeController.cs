using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationSession.Models;
using Microsoft.Extensions.Caching.Memory;

namespace WebApplicationSession.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache memoryCache;

        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            this.memoryCache = memoryCache;
        }
        //public HomeController(IMemoryCache memoryCache)
        //{
        //    this.memoryCache = memoryCache;
        //}

        public IActionResult Index()
        {
            DateTime currentTime;
            bool isExist = memoryCache.TryGetValue("CacheTime",out currentTime);
            if (!isExist)
            {
                currentTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(15));

                memoryCache.Set("CacheTime", currentTime, cacheEntryOptions);

            }
            return View(currentTime);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
