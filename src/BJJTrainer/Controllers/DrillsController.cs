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
    public class DrillsController : Controller
    {
        private readonly BJJTrainerContext _context;

        public DrillsController(BJJTrainerContext context)
        {
            _context = context;    
        }

        // GET: Drills
        public async Task<IActionResult> Index()
        {
            var bJJTrainerContext = _context.Drill.Include(d => d.Routine).Include(d => d.Technique).ThenInclude(t => t.Position);
            return View(await bJJTrainerContext.ToListAsync());
        }

        // GET: Drills/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill.SingleOrDefaultAsync(m => m.DrillId == id);
            if (drill == null)
            {
                return NotFound();
            }

            return View(drill);
        }

        // GET: Drills/Create
        public IActionResult Create()
        {
            var techniques = _context.Technique
                                            .ToList()
                                            .Select(t => new {
                                                TechniqueId = t.TechniqueId,
                                                Position =_context.Position.FirstOrDefault(p => p.PositionId == t.PositionId),
                                                Description = string.Format("{0} | {1}", t.Name, t.Position.Name)
                                            });
            ViewData["RoutineId"] = new SelectList(_context.Set<Routine>(), "RoutineId", "Name");
            ViewData["TechniqueId"] = new SelectList(techniques, "TechniqueId", "Description");
            return View();
        }

        // POST: Drills/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrillId,Description,RoutineId,TechniqueId,Time")] Drill drill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(drill);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RoutineId"] = new SelectList(_context.Set<Routine>(), "RoutineId", "RoutineId", drill.RoutineId);
            ViewData["TechniqueId"] = new SelectList(_context.Technique, "TechniqueId", "TechniqueId", drill.TechniqueId);
            return View(drill);
        }

        // GET: Drills/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill.SingleOrDefaultAsync(m => m.DrillId == id);
            if (drill == null)
            {
                return NotFound();
            }

            var techniques = _context.Technique
                                            .ToList()
                                            .Select(t => new {
                                                TechniqueId = t.TechniqueId,
                                                Position = _context.Position.FirstOrDefault(p => p.PositionId == t.PositionId),
                                                Description = string.Format("{0} | {1}", t.Name, t.Position.Name)
                                            });
            ViewData["RoutineId"] = new SelectList(_context.Set<Routine>(), "RoutineId", "Name", drill.RoutineId);
            ViewData["TechniqueId"] = new SelectList(techniques, "TechniqueId", "Description", drill.TechniqueId);
            return View(drill);
        }

        // POST: Drills/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrillId,Description,RoutineId,TechniqueId,Time")] Drill drill)
        {
            if (id != drill.DrillId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrillExists(drill.DrillId))
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
            ViewData["RoutineId"] = new SelectList(_context.Set<Routine>(), "RoutineId", "RoutineId", drill.RoutineId);
            ViewData["TechniqueId"] = new SelectList(_context.Technique, "TechniqueId", "TechniqueId", drill.TechniqueId);
            return View(drill);
        }

        // GET: Drills/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drill = await _context.Drill.SingleOrDefaultAsync(m => m.DrillId == id);
            if (drill == null)
            {
                return NotFound();
            }

            return View(drill);
        }

        // POST: Drills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drill = await _context.Drill.SingleOrDefaultAsync(m => m.DrillId == id);
            _context.Drill.Remove(drill);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DrillExists(int id)
        {
            return _context.Drill.Any(e => e.DrillId == id);
        }
    }
}
