using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVentaDeRopaOnline.Data;
using SistemaVentaDeRopaOnline.Models;

namespace SistemaVentaDeRopaOnline.Controllers
{
    
        public class CategoriaController : Controller
        {
            private readonly SistemaContext _context;

            public CategoriaController(SistemaContext context)
            {
                _context = context;
            }

            // GET: Categoria
            public async Task<IActionResult> Index()
            {
                var categorias = await _context.Categorias.ToListAsync();
                return View(categorias);
            }

            // GET: Categoria/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Categoria/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Categoria categoria)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(categoria);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return View(categoria);
            }

            // GET: Categoria/Edit/5
            public async Task<IActionResult> Edit(int id)
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return View(categoria);
            }

            // POST: Categoria/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Categoria categoria)
            {
                if (id != categoria.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(categoria);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CategoriaExists(categoria.Id))
                        {
                            return NotFound();
                        }
                        throw;
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(categoria);
            }

            // GET: Categoria/Delete/5
            public async Task<IActionResult> Delete(int id)
            {
                var categoria = await _context.Categorias
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (categoria == null)
                {
                    return NotFound();
                }
                return View(categoria);
            }

            // POST: Categoria/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var categoria = await _context.Categorias.FindAsync(id);
                if (categoria != null)
                {
                    _context.Categorias.Remove(categoria);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }

            private bool CategoriaExists(int id)
            {
                return _context.Categorias.Any(e => e.Id == id);
            }
        }
    }