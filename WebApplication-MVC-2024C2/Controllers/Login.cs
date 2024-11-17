using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(LoginModel model)
        {
            // Validar si el modelo es válido
            if (ModelState.IsValid)
            {
                // Validación de credenciales en la base de datos
                var user = await _context.NuevoUsuario
                                         .SingleOrDefaultAsync(u => u.Usuario == model.Usuario && u.Contrasenia == model.Contraseña);

                if (user != null)

                {


                    model.IDUsuario = user.Id;

                    HttpContext.Session.SetInt32("IDUsuario", user.Id);


                    // Aquí puedes redirigir al usuario a una página protegida, como el Home
                    return RedirectToAction("Index", "Home"); // Redirigir al Home u otra página

                   


                }
                else
                {
                    // Si las credenciales no coinciden, agregar un error
                    ModelState.AddModelError("", "Credenciales incorrectas");
                }
            }

            // Si el modelo no es válido, se retorna la vista de nuevo
            return View(model);
        }

        // Acción para cerrar sesión (ya no se necesita manejar cookies aquí)
        [HttpPost]
        public IActionResult Logout()
        {
            // No es necesario limpiar cookies porque no estamos usando cookies
            return RedirectToAction("Index", "Login");
        }
    }
}