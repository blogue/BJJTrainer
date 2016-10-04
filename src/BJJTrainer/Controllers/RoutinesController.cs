using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BJJTrainer.Models;
using BJJTrainer.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BJJTrainer.Controllers
{
    public class RoutinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoutinesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Routines
        public async Task<IActionResult> Index()
        {
            var routines = await _context.Routines
                .Include(r => r.Drills)
                .ThenInclude(d => d.Technique)
                .ThenInclude(t => t.Position)
                .ToListAsync();

            return View(routines);
        }

        // GET: Routines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routines
                .Include(r => r.Drills)
                .ThenInclude(d => d.Technique)
                .ThenInclude(t => t.Position)
                .SingleOrDefaultAsync(m => m.RoutineId == id);
            int totalTime = 0;
            foreach(var drill in routine.Drills)
            {
                totalTime += drill.Time;
            }
            TimeSpan ts = TimeSpan.FromSeconds(totalTime);
            ViewData["TotalTime"] = ts.ToString("mm':'ss");
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        public string DisplayPositionTime(int id)
        {
            List<TypeTime> positionTime = new List<TypeTime> { };
            var routine = _context.Routines
                .Include(r => r.Drills)
                .ThenInclude(d => d.Technique)
                .ThenInclude(t => t.Position)
                .FirstOrDefault(r => r.RoutineId == id);
            foreach(var drill in routine.Drills)
            {               
                positionTime.Add(new TypeTime(drill.Technique.Position.Name, drill.Time));                 
            }
            
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(positionTime, Formatting.None, settings);
        }

        public string DisplayTechniqueTime(int id)
        {
            List<TypeTime> techniqueTime = new List<TypeTime> { };
            var routine = _context.Routines
                .Include(r => r.Drills)
                .ThenInclude(d => d.Technique)
                .FirstOrDefault(r => r.RoutineId == id);
            foreach (var drill in routine.Drills)
            {
                techniqueTime.Add(new TypeTime(drill.Technique.Name, drill.Time));
            }
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(techniqueTime, Formatting.None, settings);
        }

        // GET: Routines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Routines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoutineId,Name")] Routine routine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(routine);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(routine);
        }

        // GET: Routines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routines.SingleOrDefaultAsync(m => m.RoutineId == id);
            if (routine == null)
            {
                return NotFound();
            }
            return View(routine);
        }

        // POST: Routines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoutineId,Name")] Routine routine)
        {
            if (id != routine.RoutineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(routine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoutineExists(routine.RoutineId))
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
            return View(routine);
        }

        // GET: Routines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routine = await _context.Routines.SingleOrDefaultAsync(m => m.RoutineId == id);
            if (routine == null)
            {
                return NotFound();
            }

            return View(routine);
        }

        // POST: Routines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var routine = await _context.Routines.SingleOrDefaultAsync(m => m.RoutineId == id);
            _context.Routines.Remove(routine);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RoutineExists(int id)
        {
            return _context.Routines.Any(e => e.RoutineId == id);
        }
    }
}
