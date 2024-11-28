using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication_MVC_2024C2.Context;
using WebApplication_MVC_2024C2.Models;

namespace WebApplication_MVC_2024C2.Controllers
{
    public class HomeController : Controller
    {
        //  private readonly ILogger<HomeController> _logger;
        //  private readonly PeliculasController _peliculasController;
        private readonly CineDataBaseContext _context;


        public HomeController(/*ILogger<HomeController> logger*/CineDataBaseContext context)
        {
            _context = context;
          //  _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            /*  var peliculas = _peliculasController.Index(); // Llama al método Index de PeliculasController
              ViewBag.Cartelera = peliculas;*/
            var peliculas = await _context.Peliculas.ToListAsync();
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
