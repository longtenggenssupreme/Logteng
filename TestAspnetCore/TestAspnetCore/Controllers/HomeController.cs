using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TestAspnetCore.Cache;
using TestAspnetCore.Models;

namespace TestAspnetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _iMemoryCache;
        //自定义缓存
        private readonly MyCustomCache _myCustomCache;

        //分布式
        private readonly IDistributedCache _distributed;
        //分布式redis
        private readonly IDistributedCache _distributedredis;
        //分布式sqlserver
        private readonly IDistributedCache _distributedsql;
        public HomeController(ILogger<HomeController> logger, IMemoryCache cache, MyCustomCache myCustomCache, IDistributedCache distributed)
        {
            _logger = logger;
            _iMemoryCache = cache;
            //自定义的缓存
            _myCustomCache = myCustomCache;

            //分布式
            _distributed = distributed;
            //分布式redis和分布式sqlserver都是分布式的，所以可以使用同一个IDistributedCache对象
            //redis缓存
            _distributedredis = distributed;
            //分布式sqlserver缓存
            _distributedsql = distributed;
        }

        public IActionResult Index()
        {
            #region MyRegion
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().SetSize(10).SetSlidingExpiration(TimeSpan.FromSeconds(2));
            _iMemoryCache.Set("cache", "这是缓存信息", options);
            MemoryCacheEntryOptions myCustomOptions = new MemoryCacheEntryOptions().SetSize(10).SetSlidingExpiration(TimeSpan.FromSeconds(2));
            //自定义的缓存的大小设置100单位，那么这里添加一个缓存（大小10单位）的话，MemoryCache中添加缓存就只能添加10缓存了
            _myCustomCache.MemoryCache.Set("myCustomCache", "这是myCustomCache缓存信息", myCustomOptions);

            //分布式
            DistributedCacheEntryOptions distributed = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
            _distributed.SetString("mydistributed", "这是distributed缓存", distributed);

            //分布式redis缓存
            DistributedCacheEntryOptions distributedOptions = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
            _distributedredis.SetString("myrediscache", "这是redis缓存", distributedOptions);
            #endregion

            //分布式sqlserver缓存
            DistributedCacheEntryOptions _distributedsql1 = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromSeconds(2));
            _distributedsql.SetString("mysqlservercache", "这是sqlserver缓存", _distributedsql1);
            return View();
        }

        public IActionResult Privacy()
        {
            ViewBag.ss = _iMemoryCache.Get("cache");
            //自定义的缓存
            ViewBag.myCustomCache = _myCustomCache.MemoryCache.Get("myCustomCache");
            //分布式
            ViewBag.distributed = _distributed.GetString("mydistributed");

            //分布式redis缓存
            ViewBag.rediscache = _distributedredis.GetString("myrediscache");

            //分布式sqlserver缓存
            ViewBag.qlservercache = _distributedsql.GetString("mysqlservercache");
            return View(_iMemoryCache.Get("cache"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
