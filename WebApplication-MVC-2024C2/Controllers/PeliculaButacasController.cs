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
    public class PeliculaButacasController : Controller
    {
        private readonly CineDataBaseContext _context;

        public PeliculaButacasController(CineDataBaseContext context)
        {
            _context = context;
        }

        // GET: PeliculaButacas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Butacas.ToListAsync());
        }

        // GET: PeliculaButacas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculaButaca = await _context.Butacas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peliculaButaca == null)
            {
                return NotFound();
            }

            return View(peliculaButaca);
        }

        // GET: PeliculaButacas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PeliculaButacas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPelicula,Disponible")] PeliculaButaca peliculaButaca)
        {
            if (ModelState.IsValid)
            {
                _context.Add(peliculaButaca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(peliculaButaca);
        }

        // GET: PeliculaButacas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculaButaca = await _context.Butacas.FindAsync(id);
            if (peliculaButaca == null)
            {
                return NotFound();
            }
            return View(peliculaButaca);
        }

        // POST: PeliculaButacas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPelicula,Disponible")] PeliculaButaca peliculaButaca)
        {
            if (id != peliculaButaca.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(peliculaButaca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaButacaExists(peliculaButaca.Id))
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
            return View(peliculaButaca);
        }

        // GET: PeliculaButacas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var peliculaButaca = await _context.Butacas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (peliculaButaca == null)
            {
                return NotFound();
            }

            return View(peliculaButaca);
        }

        // POST: PeliculaButacas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var peliculaButaca = await _context.Butacas.FindAsync(id);
            if (peliculaButaca != null)
            {
                _context.Butacas.Remove(peliculaButaca);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaButacaExists(int id)
        {
            return _context.Butacas.Any(e => e.Id == id);
        }
    }
}
