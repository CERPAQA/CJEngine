using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;

namespace CJEngine.Controllers
{
    public class ExperimentsController : Controller
    {
        private readonly CJEngineContext _context;

        public ExperimentsController(CJEngineContext context)
        {
            _context = context;
        }

        // GET: Experiments
        public async Task<IActionResult> Index()
        {

            return View(await _context.Experiment.ToListAsync());
        }

        // GET: Experiments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // GET: Experiments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Experiments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Experiment experiment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experiment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experiment);
        }

        // GET: Experiments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiment.FindAsync(id);
            if (experiment == null)
            {
                return NotFound();
            }
            return View(experiment);
        }

        // POST: Experiments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Experiment experiment)
        {
            if (id != experiment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experiment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentExists(experiment.Id))
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
            return View(experiment);
        }

        // GET: Experiments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experiment = await _context.Experiment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        // POST: Experiments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experiment = await _context.Experiment.FindAsync(id);
            _context.Experiment.Remove(experiment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentExists(int id)
        {
            return _context.Experiment.Any(e => e.Id == id);
        }
    }
}
