using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categorias.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria cat)
        {
            if (ModelState.IsValid)
            {
                _context.Categorias.Add(cat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var cat = await _context.Categorias.FindAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Categoria cat)
        {
            if (id != cat.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(cat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cat);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var cat = await _context.Categorias
                .Include(c => c.Productos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cat = await _context.Categorias.Include(c => c.Productos).FirstOrDefaultAsync(c => c.Id == id);

            if (cat == null)
            {
                TempData["Error"] = "La categoría no existe.";
                return RedirectToAction(nameof(Index));
            }

            if (cat.Productos.Any())
            {
                TempData["Error"] = "No puedes borrar una categoría con productos asignados.";
                return RedirectToAction(nameof(Index));
            }

            _context.Categorias.Remove(cat);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Categoría eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }
    }
}
