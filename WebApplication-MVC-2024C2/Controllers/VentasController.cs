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
    public class VentasController : Controller
    {
        private readonly CineDataBaseContext _context;

        public VentasController(CineDataBaseContext context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Ventas
                                       // .Include(v => v.Pelicula) // Incluye la película asociada a la venta
                                        .ToListAsync();
            return View(ventas);
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        public async Task<IActionResult> Create(int? peliculaId)
        {
            // Obtener las películas desde la base de datos
            var peliculas = await _context.Peliculas.ToListAsync();

            // Si no hay películas en la base de datos, mostrar un mensaje o redirigir
            if (!peliculas.Any())
            {
                TempData["ErrorMessage"] = "No hay películas disponibles.";
                return RedirectToAction(nameof(Index)); // O puedes redirigir a otra acción si prefieres
            }

            int selectedPeliculaId;

            if (peliculaId.HasValue)
            {
                // Si se pasa un ID de película, usar ese ID
                selectedPeliculaId = peliculaId.Value;
            }
            else
            {
                // Si no se pasa un ID de película, mostrar la lista de películas
                selectedPeliculaId = 0; // Indicamos que no hay una película seleccionada
            }

            // Obtener la película seleccionada si se pasa un ID
            var peliculaSeleccionada = selectedPeliculaId > 0
                ? await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == selectedPeliculaId)
                : null;

            // Si la película seleccionada no existe, redirigir
            if (peliculaSeleccionada == null && selectedPeliculaId > 0)
            {
                TempData["ErrorMessage"] = "Película no encontrada. Por favor, selecciona una película válida.";
                return RedirectToAction(nameof(Index));
            }

            // Obtener los puntos del usuario logueado
            var userId = HttpContext.Session.GetInt32("IDUsuario");
            if (userId != null)
            {
                var usuario = await _context.NuevoUsuario.FirstOrDefaultAsync(u => u.Id == userId.Value);
                if (usuario != null)
                {
                    ViewBag.PuntosUsuario = usuario.Puntos; // Pasar los puntos al ViewBag
                }
            }

            // Cargar las películas en el ViewBag para la lista desplegable (solo si no se seleccionó una película)
            ViewBag.Peliculas = new SelectList(peliculas, "Id", "Titulo", peliculaSeleccionada?.Id);

            // Establecer las fechas en el ViewBag según la película seleccionada, si se seleccionó una
            ViewBag.Fechas = selectedPeliculaId > 0
                ? new SelectList(new List<DateTime> { peliculaSeleccionada.Fecha }, peliculaSeleccionada.Fecha)
                : new SelectList(Enumerable.Empty<DateTime>());

            return View();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPelicula,Fecha,CantButacas,Total,Pelicula,Promocion")] Venta venta)
        {
            // Validar que la película seleccionada existe
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == venta.IdPelicula);

            if (pelicula == null)
            {
                ModelState.AddModelError("IdPelicula", "La película seleccionada no existe.");
                ViewBag.Peliculas = new SelectList(await _context.Peliculas.ToListAsync(), "Id", "Titulo");
                ViewBag.Fechas = new SelectList(Enumerable.Empty<DateTime>());
                return View(venta);
            }

            if (venta.CantButacas < 0)
            {
                ModelState.AddModelError("CantButacas", "La cantidad de butacas no puede ser negativa.");
                ViewBag.Peliculas = new SelectList(await _context.Peliculas.ToListAsync(), "Id", "Titulo");
                ViewBag.Fechas = new SelectList(new List<DateTime> { pelicula.Fecha }, pelicula.Fecha);
                return View(venta);
            }

            if (venta.CantButacas > pelicula.CantButacas)
            {
                ModelState.AddModelError("CantButacas", $"No hay suficientes butacas disponibles. Solo quedan {pelicula.CantButacas} butacas.");
                ViewBag.Peliculas = new SelectList(await _context.Peliculas.ToListAsync(), "Id", "Titulo");
                ViewBag.Fechas = new SelectList(new List<DateTime> { pelicula.Fecha }, pelicula.Fecha);
                return View(venta);
            }

            // Asignar la película y la fecha a la propiedad de la venta
            venta.IdPelicula = pelicula.Id;
            venta.Fecha = pelicula.Fecha;

            // Calcular el total basado en la película seleccionada
            if (venta.Promocion)
            {
                // Si se aplica la promoción 2x1, el total es la mitad
                venta.Total = (pelicula.Precio * venta.CantButacas) / 2;
            }
            else
            {
                // Si no se aplica la promoción, el total es el precio completo
                venta.Total = pelicula.Precio * venta.CantButacas;
            }

            if (ModelState.IsValid)
            {
                // Guardar la venta y actualizar la película
                var userId = HttpContext.Session.GetInt32("IDUsuario");
                if (userId == null)
                {
                    ModelState.AddModelError("", "Debe iniciar sesión para realizar una venta.");
                    return View(venta);
                }

                var usuario = await _context.NuevoUsuario.FirstOrDefaultAsync(u => u.Id == userId);
                if (usuario != null)
                {
                    if (!venta.Promocion)
                    {
                        usuario.Puntos += 200;
                        _context.Update(usuario);
                    }
                    else
                    {
                        usuario.Puntos -= 1000;
                        _context.Update(usuario);
                    }
                }

                venta.IDUsuario = userId.Value;
                pelicula.CantButacas -= venta.CantButacas;
                _context.Update(pelicula);
                _context.Add(venta);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Si hay un error, recargar las listas para la vista
            ViewBag.Peliculas = new SelectList(await _context.Peliculas.ToListAsync(), "Id", "Titulo");
            ViewBag.Fechas = new SelectList(new List<DateTime> { pelicula.Fecha }, pelicula.Fecha);
            return View(venta);
        }

        [HttpGet]
        public async Task<IActionResult> GetFechasByPelicula(int id)
        {
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula == null)
            {
                return Json(new List<DateTime>());
            }

            return Json(new List<DateTime> { pelicula.Fecha });
        }
        [HttpGet]
        public async Task<IActionResult> GetPrecioByPelicula(int id)
        {
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula == null)
            {
                return Json(0); 
            }

            return Json(pelicula.Precio);
        }


        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPelicula,Fecha,CantButacas,Total")] Venta venta)
        {
            if (id != venta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id))
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
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Buscar la venta por su ID
            var venta = await _context.Ventas.FirstOrDefaultAsync(v => v.Id == id);

            if (venta != null)
            {
                // Obtener la película asociada usando el IdPelicula de la venta
                var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == venta.IdPelicula);

                if (pelicula != null)
                {
                    // Devolver las butacas a la película
                    pelicula.CantButacas += venta.CantButacas;  // Sumar las butacas reservadas a la cantidad disponible
                    var userId = HttpContext.Session.GetInt32("IDUsuario");
                    var usuario = await _context.NuevoUsuario.FirstOrDefaultAsync(u => u.Id == userId);
                    if (venta.Promocion)
                    {
                        usuario.Puntos += 1000;
                        _context.Update(usuario);
                    }
                    else
                    {
                        usuario.Puntos -= 200;
                        _context.Update(usuario);
                    }
                


                    // Actualizar la película en la base de datos
                    _context.Update(pelicula);
                    
                }

                // Eliminar la venta
                _context.Ventas.Remove(venta);

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
            }

            // Redirigir al índice (lista de ventas)
            return RedirectToAction(nameof(Index));
        }


        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult RedirectToCreate(int peliculaId)
        {
            // Verificar si el usuario está logueado
            var userId = HttpContext.Session.GetInt32("IDUsuario");
            if (userId == null)
            {
                // Guardar película seleccionada en la sesión para usar después del login
                HttpContext.Session.SetInt32("PeliculaSeleccionada", peliculaId);
                return RedirectToAction("Index", "Login");
            }

            // Redirigir directamente al formulario de ventas con la película seleccionada
            return RedirectToAction("Create", new { peliculaId });
        }

    }

}
