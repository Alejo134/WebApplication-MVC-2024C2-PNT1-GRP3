using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApplication_MVC_2024C2.Context;
using WebApplication_MVC_2024C2.Models;

namespace WebApplication_MVC_2024C2.Controllers
{
    public class HomeController : Controller
    {
        private readonly CineDataBaseContext _context;


        public HomeController(CineDataBaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Verificar si la sesi�n est� inicializada
            if (HttpContext.Session.GetInt32("IDUsuario") != null)
            {
                // Si la sesi�n est� iniciada, establecer en TempData que el usuario ha iniciado sesi�n
                TempData["IsUserLoggedIn"] = true;
            }

            // Obtener las pel�culas de la base de datos
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
