using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BJJTrainer.Models;

namespace BJJTrainer.Controllers
{
    public class TechniquesController : Controller
    {
        private readonly BJJTrainerContext _context;

        public TechniquesController(BJJTrainerContext context)
        {
            _context = context;    
        }

        // GET: Techniques
        public async Task<IActionResult> Index()
        {
            var bJJTrainerContext = _context.Technique.Include(t => t.Category).Include(t => t.Position);
            return View(await bJJTrainerContext.ToListAsync());
        }

        // GET: Techniques/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique
                .Include(t => t.Category)
                .Include(t => t.Position)
                .SingleOrDefaultAsync(m => m.TechniqueId == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // GET: Techniques/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "Name");
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "Name");
            return View();
        }

        // POST: Techniques/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TechniqueId,CategoryId,Name,PositionId")] Technique technique)
        {
            if (ModelState.IsValid)
            {
                _context.Add(technique);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", technique.CategoryId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", technique.PositionId);
            return View(technique);
        }

        // GET: Techniques/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique.SingleOrDefaultAsync(m => m.TechniqueId == id);
            if (technique == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", technique.CategoryId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", technique.PositionId);
            return View(technique);
        }

        // POST: Techniques/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TechniqueId,CategoryId,Name,PositionId")] Technique technique)
        {
            if (id != technique.TechniqueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(technique);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TechniqueExists(technique.TechniqueId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "CategoryId", "CategoryId", technique.CategoryId);
            ViewData["PositionId"] = new SelectList(_context.Position, "PositionId", "PositionId", technique.PositionId);
            return View(technique);
        }

        // GET: Techniques/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var technique = await _context.Technique.SingleOrDefaultAsync(m => m.TechniqueId == id);
            if (technique == null)
            {
                return NotFound();
            }

            return View(technique);
        }

        // POST: Techniques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var technique = await _context.Technique.SingleOrDefaultAsync(m => m.TechniqueId == id);
            _context.Technique.Remove(technique);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TechniqueExists(int id)
        {
            return _context.Technique.Any(e => e.TechniqueId == id);
        }
    }
}
