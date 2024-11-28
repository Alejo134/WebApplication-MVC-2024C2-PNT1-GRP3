using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication_MVC_2024C2.Models;

namespace WebApplication_MVC_2024C2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PeliculasController _peliculasController;


        public HomeController(ILogger<HomeController> logger, PeliculasController peliculasController)
        {
            _peliculasController = peliculasController;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var peliculas = _peliculasController.Index(); // Llama al método Index de PeliculasController
            ViewBag.Cartelera = peliculas;
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
