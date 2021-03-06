﻿using System;
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
    [Authorize(Roles = ("Researcher"))]
    public class ResearchersController : Controller
    {
        private readonly CJEngineContext _context;

        public ResearchersController(CJEngineContext context)
        {
            _context = context;
        }

        // GET: Researchers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Researcher.ToListAsync());
        }

        // GET: Researchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researcher = await _context.Researcher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (researcher == null)
            {
                return NotFound();
            }

            return View(researcher);
        }

        // GET: Researchers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Researchers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Researcher researcher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researcher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(researcher);
        }

        // GET: Researchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researcher = await _context.Researcher.FindAsync(id);
            if (researcher == null)
            {
                return NotFound();
            }
            return View(researcher);
        }

        // POST: Researchers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Researcher researcher)
        {
            if (id != researcher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researcher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearcherExists(researcher.Id))
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
            return View(researcher);
        }

        // GET: Researchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researcher = await _context.Researcher
                .FirstOrDefaultAsync(m => m.Id == id);
            if (researcher == null)
            {
                return NotFound();
            }

            return View(researcher);
        }

        // POST: Researchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researcher = await _context.Researcher.FindAsync(id);
            _context.Researcher.Remove(researcher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearcherExists(int id)
        {
            return _context.Researcher.Any(e => e.Id == id);
        }
    }
}
