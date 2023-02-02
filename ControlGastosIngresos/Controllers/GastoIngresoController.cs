using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlGastosIngresos.Data;
using ControlGastosIngresos.Models;

namespace ControlGastosIngresos.Controllers
{
    public class GastoIngresoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GastoIngresoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GastoIngreso
        public async Task<IActionResult> Index(int? mes, int anio) //le colocamos el signo de interrogacion para decirle que ese campo puede ser nullo
        {
            if (mes == null)
            {
                mes = DateTime.Now.Month;
            }
            if (anio == null)
            {
                anio = DateTime.Now.Year;
            }

            ViewData["mes"] = mes;
            ViewData["anio"] = anio; ;

            var applicationDbContext = _context.GastoIngresos.Include(g => g.Categoria)
                                        .Where(i=>i.Fecha.Month==mes && i.Fecha.Year==anio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GastoIngreso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GastoIngresos == null)
            {
                return NotFound();
            }

            var gastoIngreso = await _context.GastoIngresos
                .Include(g => g.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoIngreso == null)
            {
                return NotFound();
            }

            return View(gastoIngreso);
        }

        // GET: GastoIngreso/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria");
            return View();
        }

        // POST: GastoIngreso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaId,Fecha,Valor")] GastoIngreso gastoIngreso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gastoIngreso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", gastoIngreso.CategoriaId);
            return View(gastoIngreso);
        }

        // GET: GastoIngreso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GastoIngresos == null)
            {
                return NotFound();
            }

            var gastoIngreso = await _context.GastoIngresos.FindAsync(id);
            if (gastoIngreso == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", gastoIngreso.CategoriaId);
            return View(gastoIngreso);
        }

        // POST: GastoIngreso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaId,Fecha,Valor")] GastoIngreso gastoIngreso)
        {
            if (id != gastoIngreso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gastoIngreso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GastoIngresoExists(gastoIngreso.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCategoria", gastoIngreso.CategoriaId);
            return View(gastoIngreso);
        }

        // GET: GastoIngreso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GastoIngresos == null)
            {
                return NotFound();
            }

            var gastoIngreso = await _context.GastoIngresos
                .Include(g => g.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gastoIngreso == null)
            {
                return NotFound();
            }

            return View(gastoIngreso);
        }

        // POST: GastoIngreso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GastoIngresos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.GastoIngresos'  is null.");
            }
            var gastoIngreso = await _context.GastoIngresos.FindAsync(id);
            if (gastoIngreso != null)
            {
                _context.GastoIngresos.Remove(gastoIngreso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GastoIngresoExists(int id)
        {
          return _context.GastoIngresos.Any(e => e.Id == id);
        }
    }
}
