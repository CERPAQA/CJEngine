using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using Microsoft.AspNetCore.Authorization;

namespace CJEngine.Controllers
{
    //[Route("api/[controller]")]
    [Authorize]
    public class ExperimentParametersController : Controller
    {
        private readonly CJEngineContext _context;
        

        public ExperimentParametersController(CJEngineContext context)
        {
            _context = context;
        }

        //nullreferenceException: object not set to an instance of an object....its not passing the view model along.
        public PartialViewResult LsParam()
        {
            try
            {
                return PartialView("~/Views/ExperimentParameters/_LsParams.cshtml");
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //list is empty each time the method is called
     
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
        public async Task<IActionResult> Create([Bind("Id,Description,ShowTitle,ShowTimer, AddComment")] ExperimentParameters experimentParameters)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowTitle,ShowTimer, AddComment")] ExperimentParameters experimentParameters)
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
