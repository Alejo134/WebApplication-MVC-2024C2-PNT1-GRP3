using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication_MVC_2024C2.Context;
using WebApplication_MVC_2024C2.Models;

namespace WebApplication_MVC_2024C2.Controllers
{
    public class LoginController : Controller
    {
        private readonly CineDataBaseContext _context;

        // Constructor que recibe el contexto de la base de datos
        public LoginController(CineDataBaseContext context)
        {
            _context = context;
        }

        // Acción GET para mostrar el formulario de login
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Acción POST para procesar el login
        [HttpPost]
        public IActionResult Index(LoginModel model)
        {
            // Solo asignar TempData["IsUserLoggedIn"] si el inicio de sesión es exitoso
            if (ModelState.IsValid)
            {
                var usuario = _context.NuevoUsuario
                    .FirstOrDefault(u => u.Usuario == model.Usuario && u.Contrasenia == model.Contraseña);

                if (usuario != null)
                {
                    // Guardar información del usuario en la sesión
                    HttpContext.Session.SetInt32("IDUsuario", usuario.Id);

                    // Establecer en TempData que el usuario ha iniciado sesión
                    TempData["IsUserLoggedIn"] = true;

                    // Verificar si hay una película seleccionada
                    var peliculaId = HttpContext.Session.GetInt32("PeliculaSeleccionada");
                    if (peliculaId.HasValue)
                    {
                        // Limpiar la sesión y redirigir al formulario de ventas
                        HttpContext.Session.Remove("PeliculaSeleccionada");
                        return RedirectToAction("Create", "Ventas", new { peliculaId = peliculaId.Value });
                    }

                    // Si no hay película seleccionada, redirigir a la página principal
                    return RedirectToAction("Index", "Home");
                }

                // Si las credenciales son incorrectas, asignamos false a TempData
                TempData["IsUserLoggedIn"] = false;
                ModelState.AddModelError("", "Credenciales incorrectas.");
            }

            return View(model);
        }

        // Acción POST para cerrar sesión
        [HttpPost]
        public IActionResult Logout()
        {
            // Eliminar los datos de la sesión
            HttpContext.Session.Remove("IDUsuario");
            HttpContext.Session.Remove("PeliculaSeleccionada");

            // Establecer en TempData que el usuario ha cerrado sesión
            TempData["IsUserLoggedIn"] = false;

            // Redirigir al login
            return RedirectToAction("Index", "Login");
        }
    }
}
