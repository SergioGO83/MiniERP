using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly AppDbContext _context;

        public DepartamentosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departamentos = await _context.Departamentos
                .Include(d => d.Empleados)   // ✅ Incluye empleados
                .ToListAsync();

            return View(departamentos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var dep = await _context.Departamentos.FindAsync(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre")] Departamento departamento)
        {
            if (!ModelState.IsValid) return View(departamento);

            _context.Add(departamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // -> 302
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var dep = await _context.Departamentos.FindAsync(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Departamento dep)
        {
            if (id != dep.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(dep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dep);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var dep = await _context.Departamentos.FindAsync(id);
            if (dep == null) return NotFound();
            return View(dep);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dep = await _context.Departamentos.FindAsync(id);
            if (dep == null) return NotFound();

            _context.Departamentos.Remove(dep);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
