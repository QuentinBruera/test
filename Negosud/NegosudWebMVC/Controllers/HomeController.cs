using Microsoft.AspNetCore.Mvc;
using NegosudWebMVC.Models;
using System.Diagnostics;
using System.Text.Encodings.Web;

namespace NegosudWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Hello()
        {
            return View();
        }

        public IActionResult Bienvenue(int id, string name = "unknown", int nbFois = 1)
        {
            ViewData["Message"] = "Bonjour " + HtmlEncoder.Default.Encode(name);
            ViewData["nbfois"] = nbFois;
            ViewData["id"] = id;

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
