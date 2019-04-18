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
    public class AlgorithmsController : Controller
    {
        private readonly CJEngineContext _context;

        public AlgorithmsController(CJEngineContext context)
        {
            _context = context;
        }

        //nullreferenceException: object not set to an instance of an object....its not passing the view model along.
        public PartialViewResult LsAlgorithm()
        {
            try
            {
                return PartialView("~/Views/Algorithms/_LsAlgorithm.cshtml");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // GET: Algorithms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Algorithm.ToListAsync());
        }

        // GET: Algorithms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var algorithm = await _context.Algorithm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (algorithm == null)
            {
                return NotFound();
            }

            return View(algorithm);
        }

        // GET: Algorithms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Algorithms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FunctionName,Filename,Description,Valid")] Algorithm algorithm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(algorithm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(algorithm);
        }

        // GET: Algorithms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var algorithm = await _context.Algorithm.FindAsync(id);
            if (algorithm == null)
            {
                return NotFound();
            }
            return View(algorithm);
        }

        // POST: Algorithms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FunctionName,Filename,Description,Valid")] Algorithm algorithm)
        {
            if (id != algorithm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(algorithm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlgorithmExists(algorithm.Id))
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
            return View(algorithm);
        }

        // GET: Algorithms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var algorithm = await _context.Algorithm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (algorithm == null)
            {
                return NotFound();
            }

            return View(algorithm);
        }

        // POST: Algorithms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var algorithm = await _context.Algorithm.FindAsync(id);
            _context.Algorithm.Remove(algorithm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlgorithmExists(int id)
        {
            return _context.Algorithm.Any(e => e.Id == id);
        }
    }
}
