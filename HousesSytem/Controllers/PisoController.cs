using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReservationWeb.App;
using ReservationWeb.Models;

namespace ReservationWeb.Controllers
{
    public class PisoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PisoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Piso
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pisos.ToListAsync());
        }

        // GET: Piso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piso = await _context.Pisos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (piso == null)
            {
                return NotFound();
            }

            return View(piso);
        }

        // GET: Piso/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Piso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Piso piso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(piso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(piso);
        }

        // GET: Piso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piso = await _context.Pisos.FindAsync(id);
            if (piso == null)
            {
                return NotFound();
            }
            return View(piso);
        }

        // POST: Piso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Piso piso)
        {
            if (id != piso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(piso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PisoExists(piso.Id))
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
            return View(piso);
        }

        // GET: Piso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var piso = await _context.Pisos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (piso == null)
            {
                return NotFound();
            }

            return View(piso);
        }

        // POST: Piso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var piso = await _context.Pisos.FindAsync(id);
            _context.Pisos.Remove(piso);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PisoExists(int id)
        {
            return _context.Pisos.Any(e => e.Id == id);
        }
    }
}
