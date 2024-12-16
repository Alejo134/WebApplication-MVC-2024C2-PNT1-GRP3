using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_MVC_2024C2.Context;
using WebApplication_MVC_2024C2.Models;

namespace WebApplication_MVC_2024C2.Controllers
{
    public class NuevoUsuariosController : Controller
    {
        private readonly CineDataBaseContext _context;

        public NuevoUsuariosController(CineDataBaseContext context)
        {
            _context = context;
        }

        // GET: NuevoUsuarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.NuevoUsuario.ToListAsync());
        }

        // GET: NuevoUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nuevoUsuario = await _context.NuevoUsuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nuevoUsuario == null)
            {
                return NotFound();
            }

            return View(nuevoUsuario);
        }

        // GET: NuevoUsuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NuevoUsuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Usuario,Contrasenia,Email")] NuevoUsuario nuevoUsuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioExistente = await _context.NuevoUsuario
                    .FirstOrDefaultAsync(u => u.Usuario == nuevoUsuario.Usuario);

                if (usuarioExistente != null)
                {
                    ModelState.AddModelError("Usuario", "Este nombre de usuario ya está registrado.");
                    return View(nuevoUsuario); 
                }

                var emailExistente = await _context.NuevoUsuario
                    .FirstOrDefaultAsync(u => u.Email == nuevoUsuario.Email);

                if (emailExistente != null)
                {
                    ModelState.AddModelError("Email", "Este correo electrónico ya está registrado.");
                    return View(nuevoUsuario); 
                }
                //Iniciar con 1000 puntos.(antes de agregar a context)
                nuevoUsuario.Puntos = 1000;

                // Si el usuario y correo son únicos, agregar el nuevo usuario
                _context.Add(nuevoUsuario);
                await _context.SaveChangesAsync();

                
                // Guardar información del usuario en la sesión
                HttpContext.Session.SetInt32("IDUsuario", nuevoUsuario.Id);

                // Establecer en TempData que el usuario ha iniciado sesión
                TempData["IsUserLoggedIn"] = true;

                // Redirigir a la página de índice u otra página después de la creación
                return RedirectToAction("Index", "Home");
            }

            // Si el modelo no es válido o si hay errores de validación, retornar la vista con el modelo
            return View(nuevoUsuario);
        }

        // GET: NuevoUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nuevoUsuario = await _context.NuevoUsuario.FindAsync(id);
            if (nuevoUsuario == null)
            {
                return NotFound();
            }
            return View(nuevoUsuario);
        }

        // POST: NuevoUsuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Usuario,Contrasenia,Email")] NuevoUsuario nuevoUsuario)
        {
            if (id != nuevoUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nuevoUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NuevoUsuarioExists(nuevoUsuario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nuevoUsuario);
        }

        // GET: NuevoUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nuevoUsuario = await _context.NuevoUsuario
                .FirstOrDefaultAsync(m => m.Id == id);
            if (nuevoUsuario == null)
            {
                return NotFound();
            }

            return View(nuevoUsuario);
        }

        // POST: NuevoUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nuevoUsuario = await _context.NuevoUsuario.FindAsync(id);
            if (nuevoUsuario != null)
            {
                _context.NuevoUsuario.Remove(nuevoUsuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NuevoUsuarioExists(int id)
        {
            return _context.NuevoUsuario.Any(e => e.Id == id);
        }
    }
}
