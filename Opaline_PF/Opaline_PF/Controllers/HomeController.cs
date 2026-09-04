using Microsoft.AspNetCore.Mvc;
using Opaline_PF.Models;
using System.Diagnostics;

namespace Opaline_PF.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Perfumes()
        {
            return View();
        }

        public IActionResult Make()
        {
            return View();
        }

        public IActionResult Kits()
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
