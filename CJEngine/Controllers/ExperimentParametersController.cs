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
    [Route("api/[controller]")]
    public class ExperimentParametersController : Controller
    {
        private readonly CJEngineContext _context;
        public IList<Artefact> expArtefacts;

        public ExperimentParametersController(CJEngineContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpPost("[action]")]
        public async Task CreateParams([FromBody][Bind("Id,Description,ShowTitle,ShowTimer")] ExperimentParameters experimentParameters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experimentParameters);
                await _context.SaveChangesAsync();
            }
        }

        //needs more work
        [HttpPost("[action]")]
        public async Task AddArtefact([FromBody] Artefact artefact)
        {
            var Artefact = await _context.Artefact
                .FirstOrDefaultAsync(m => m.Id == artefact.Id);
            expArtefacts.Add(Artefact);
        }

        // GET: ExperimentParameters
        public async Task<IActionResult> Index()
        {
            return View(await _context.ExperimentParameters.ToListAsync());
        }

        // GET: ExperimentParameters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentParameters = await _context.ExperimentParameters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experimentParameters == null)
            {
                return NotFound();
            }

            return View(experimentParameters);
        }

        // GET: ExperimentParameters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ExperimentParameters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,ShowTitle,ShowTimer")] ExperimentParameters experimentParameters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experimentParameters);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experimentParameters);
        }

        // GET: ExperimentParameters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentParameters = await _context.ExperimentParameters.FindAsync(id);
            if (experimentParameters == null)
            {
                return NotFound();
            }
            return View(experimentParameters);
        }

        // POST: ExperimentParameters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowTitle,ShowTimer")] ExperimentParameters experimentParameters)
        {
            if (id != experimentParameters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experimentParameters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperimentParametersExists(experimentParameters.Id))
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
            return View(experimentParameters);
        }

        // GET: ExperimentParameters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experimentParameters = await _context.ExperimentParameters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (experimentParameters == null)
            {
                return NotFound();
            }

            return View(experimentParameters);
        }

        // POST: ExperimentParameters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experimentParameters = await _context.ExperimentParameters.FindAsync(id);
            _context.ExperimentParameters.Remove(experimentParameters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperimentParametersExists(int id)
        {
            return _context.ExperimentParameters.Any(e => e.Id == id);
        }
    }
}
