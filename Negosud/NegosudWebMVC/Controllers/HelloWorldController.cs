using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace NegosudWebMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET : /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }

        //// GET : /HelloWorld/Bienvenue/
        //public string Bienvenue(string name, int nbFois = 1)
        //{
        //    return HtmlEncoder.Default.Encode($"Bonjour {name}, nb fois = {nbFois}");
        //}

        // GET : /HelloWorld/Bienvenue/1?name=Nadine&nbfois=4
        public IActionResult Bienvenue(int id, string name="unknown", int nbFois = 1)
        {
            ViewData["Message"] = "Bonjour " + HtmlEncoder.Default.Encode(name);
            ViewData["nbfois"] = nbFois;
            ViewData["id"] = id;

            return View();
        }
    }
}
