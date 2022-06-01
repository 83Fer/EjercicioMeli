using InformacionIP.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using System.Diagnostics;

namespace InformacionIP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IInformacionIPService _informacionIPService; 

        public HomeController(ILogger<HomeController> logger, IInformacionIPService informacionIPService)
        {
            _logger = logger;
            _informacionIPService = informacionIPService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetInfoIP(string ip)
        {
            IPInformation iPInformation = await _informacionIPService.Get(ip);
            return Json(iPInformation);
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