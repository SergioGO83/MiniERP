using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniERP.Data;
using MiniERP.Models;

namespace MiniERP.Controllers
{
    public class ProductosController : Controller
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Productos.Include(p => p.Categoria).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var prod = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
            if (prod == null) return NotFound();
            return View(prod);
        }

        public IActionResult Create()
        {
            ViewData["Categorias"] = _context.Categorias.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto prod)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var prod = await _context.Productos.FindAsync(id);
            if (prod == null) return NotFound();
            ViewData["Categorias"] = _context.Categorias.ToList();
            return View(prod);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Producto prod)
        {
            if (id != prod.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(prod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prod);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var prod = await _context.Productos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);
            if (prod == null) return NotFound();
            return View(prod);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prod = await _context.Productos.FindAsync(id);
            if (prod == null) return NotFound();

            _context.Productos.Remove(prod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
