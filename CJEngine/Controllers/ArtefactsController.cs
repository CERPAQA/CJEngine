using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace CJEngine.Controllers
{
    [Authorize(Roles = ("Researcher"))]
    public class ArtefactsController : Controller
    {
        private readonly CJEngineContext _context;
        private readonly IHostingEnvironment he;

        public ArtefactsController(CJEngineContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }

        // GET: Artefacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artefact.ToListAsync());
        }

        // GET: Artefacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artefact = await _context.Artefact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artefact == null)
            {
                return NotFound();
            }

            return View(artefact);
        }

        // GET: Artefacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artefacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FileName,Description,FilePath")] IFormFile file, Artefact artefact)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.Combine(he.WebRootPath + "/artefacts", Path.GetFileName(file.FileName));
                    file.CopyTo(new FileStream(fileName, FileMode.Create));
                    artefact.setImageAsRelativePath(fileName);
                    artefact.FileName = Path.GetFileName(fileName);
                    if (artefact.Name == null)
                    {
                        artefact.Name = Path.GetFileNameWithoutExtension(fileName);
                    }
                }

                if(ModelState.IsValid)
                {
                    _context.Add(artefact);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }
            }
            return View(artefact);
        }

        // GET: Artefacts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artefact = await _context.Artefact.FindAsync(id);
            if (artefact == null)
            {
                return NotFound();
            }
            return View(artefact);
        }

        // POST: Artefacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FileName,Description")] Artefact artefact)
        {
            if (id != artefact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artefact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtefactExists(artefact.Id))
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
            return View(artefact);
        }

        // GET: Artefacts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artefact = await _context.Artefact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artefact == null)
            {
                return NotFound();
            }

            return View(artefact);
        }

        // POST: Artefacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artefact = await _context.Artefact.FindAsync(id);
            _context.Artefact.Remove(artefact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtefactExists(int id)
        {
            return _context.Artefact.Any(e => e.Id == id);
        }
    }
}
