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

        // GET: Ventas/Create
        public async Task<IActionResult> Create()
        {
            // Obtener las películas desde la base de datos
            var peliculas = await _context.Peliculas.ToListAsync();

            // Si no hay películas, redirigir o mostrar un mensaje de error
            if (!peliculas.Any())
            {
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

            // Cargar las películas en el ViewBag
            ViewBag.Peliculas = new SelectList(peliculas, "Id", "Titulo");

            // Inicialmente, las fechas estarán vacías hasta que se seleccione una película
            ViewBag.Fechas = new SelectList(Enumerable.Empty<DateTime>());

            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPelicula,Fecha,CantButacas,Total,Pelicula,Promocion")] Venta venta)


        {

            // Agregar para depuración
            Console.WriteLine("Entra a la acción Create");

            // Validar que la película seleccionada existe
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == venta.IdPelicula);

            if (pelicula == null)
            {
                ModelState.AddModelError("IdPelicula", "La película seleccionada no existe.");
                ViewBag.Peliculas = new SelectList(await _context.Peliculas.ToListAsync(), "Id", "Titulo");
                ViewBag.Fechas = new SelectList(Enumerable.Empty<DateTime>());
                return View(venta);
            }

            /* Verificar que la fecha de la venta coincida con la fecha de la película
            if (venta.Fecha.Date != pelicula.Fecha.Date)
            {
                ModelState.AddModelError("Fecha", "La fecha seleccionada no coincide con la fecha disponible para la película.");
                ViewBag.Peliculas = new SelectList(await _context.Peliculas.ToListAsync(), "Id", "Titulo");
                ViewBag.Fechas = new SelectList(new List<DateTime> { pelicula.Fecha }, pelicula.Fecha);
                return View(venta);
            }*/

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

            // Asignar la película a la propiedad 'Pelicula' de la venta
           // venta.Pelicula = pelicula;

            venta.IdPelicula = pelicula.Id;
            

            // Calcular el total basado en la película seleccionada
            // venta.Total = pelicula.Precio * venta.CantButacas;
            // Calcula el total
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
                Console.WriteLine($"Promocion: {venta.Promocion}");
                var userId = HttpContext.Session.GetInt32("IDUsuario"); // Recuperar el ID del usuario logueado

                if (userId == null)
                {
                    ModelState.AddModelError("", "Debe iniciar sesión para realizar una venta.");
                    return View(venta);

                    //break?
                }
                
                var usuario = await _context.NuevoUsuario.FirstOrDefaultAsync(u => u.Id == userId); // Obtener usuario

                if (usuario != null)
                    if (!venta.Promocion)
                    {


                        usuario.Puntos += 200;  // Suma 200 puntos
                        _context.Update(usuario);


                    } else {

                        usuario.Puntos -= 1000;
                        _context.Update(usuario);

                    }


                
                venta.IDUsuario = userId.Value;

                pelicula.CantButacas -= venta.CantButacas;
                
                _context.Update(pelicula);
                _context.Add(venta);

                await _context.SaveChangesAsync();
                Console.WriteLine($"Guardando venta: {venta.IdPelicula}, {venta.Fecha}, {venta.CantButacas}, {venta.Total}");

                return RedirectToAction(nameof(Index));


            }
            // Depuración: mostrar todos los errores del modelo
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"Error: {error.ErrorMessage}");
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

            // Devuelve la fecha de la película (o ajusta si necesitas más de una fecha)
            return Json(new List<DateTime> { pelicula.Fecha });
        }
        [HttpGet]
        public async Task<IActionResult> GetPrecioByPelicula(int id)
        {
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(p => p.Id == id);

            if (pelicula == null)
            {
                return Json(0); // Si no se encuentra, devuelve 0
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
    }
}
