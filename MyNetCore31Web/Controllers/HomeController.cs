using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MyNetCore31Web.Models;

namespace MyNetCore31Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer _localizer;
        private readonly Jwtextension _jwtextension;
        
        public HomeController(ILogger<HomeController> logger, IStringLocalizer localizer, Jwtextension jwtextension)
        {
            _logger = logger;
            _localizer = localizer;
            _jwtextension = jwtextension;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(_localizer.GetString("myname"));
            _logger.LogInformation(_jwtextension.ProductJWT()); 
            return View();
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
