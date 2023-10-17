using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrituurOpDeHoekMVC.Data;
using FrituurOpDeHoekMVC.Models;

namespace FrituurOpDeHoekMVC.Controllers
{
    public class OldOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OldOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OldOrders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.OldOrders.Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: OldOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OldOrders == null)
            {
                return NotFound();
            }

            var oldOrder = await _context.OldOrders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oldOrder == null)
            {
                return NotFound();
            }

            return View(oldOrder);
        }

        // GET: OldOrders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Discriminator");
            return View();
        }

        // POST: OldOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,UserId")] OldOrder oldOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oldOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Discriminator", oldOrder.UserId);
            return View(oldOrder);
        }

        // GET: OldOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OldOrders == null)
            {
                return NotFound();
            }

            var oldOrder = await _context.OldOrders.FindAsync(id);
            if (oldOrder == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Discriminator", oldOrder.UserId);
            return View(oldOrder);
        }

        // POST: OldOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,UserId")] OldOrder oldOrder)
        {
            if (id != oldOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oldOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OldOrderExists(oldOrder.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Discriminator", oldOrder.UserId);
            return View(oldOrder);
        }

        // GET: OldOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OldOrders == null)
            {
                return NotFound();
            }

            var oldOrder = await _context.OldOrders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oldOrder == null)
            {
                return NotFound();
            }

            return View(oldOrder);
        }

        // POST: OldOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OldOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OldOrders'  is null.");
            }
            var oldOrder = await _context.OldOrders.FindAsync(id);
            if (oldOrder != null)
            {
                _context.OldOrders.Remove(oldOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OldOrderExists(int id)
        {
          return (_context.OldOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
