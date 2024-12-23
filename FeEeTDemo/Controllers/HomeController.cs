using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using FeEeTDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FeEeTDemo.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /*
        public IActionResult Index()
        {

            return View();
        }
        */
        //burasý sarý uyarý alerti için
        public IActionResult Index(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                TempData["Message"] = message; // Mesajý TempData'ya aktar
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}