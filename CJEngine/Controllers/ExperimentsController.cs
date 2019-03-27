using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using CJEngine.Models.Join_Entities;
using CJEngine.ViewModel;

namespace CJEngine.Controllers
{
    public class ExperimentsController : Controller
    {
        private readonly CJEngineContext _context;
        //public static IList<Artefact> expArtefacts = new List<Artefact>();

        public ExperimentsController(CJEngineContext context)
        {
            _context = context;
        }

        // GET: Experiments
        public async Task<IActionResult> Index()
        {

            return View(await _context.Experiment.ToListAsync());
        }

        //This method is what renders when the cj tab is clicked
        public async Task<IActionResult> CJIndex()
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
                .Include(exp => exp.ExperimentParameters)
                .Include(exp => exp.ExpJudges)
                    .ThenInclude(judge => judge.Judge)
                .Include(exp => exp.ExpArtefacts)
                    .ThenInclude(artefact => artefact. Artefact)
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
            CreateExperimentViewModel CEVM = new CreateExperimentViewModel();
            CEVM.ExperimentParametersList = _context.ExperimentParameters.ToList();
            CEVM.Artefacts = _context.Artefact.ToList();
            CEVM.Judges = _context.Judge.ToList();
            return View(CEVM);
        }

        // POST: Experiments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Experiment experiment)
        {
            var form = Request.Form;
            experiment.ExpArtefacts = new List<ExpArtefact>();
            experiment.ExpJudges = new List<ExpJudge>();

            var expID = experiment.Id;
            string expNameParam = form["Parameters"];
            var expParam = await _context.ExperimentParameters
                .FirstOrDefaultAsync(m => m.Description == expNameParam);
            int expParamID = expParam.Id;
            experiment.ExperimentParametersId = expParamID;
            experiment.ExperimentParameters = expParam;

            var artefacts = form["expArtefacts"];
            foreach (string x in artefacts)
            {
                var artefactName = x.Trim();
                var expArtefact = await _context.Artefact.
                    FirstOrDefaultAsync(m => m.Name == artefactName);
                var artefactID = expArtefact.Id;
                ExpArtefact exp = new ExpArtefact();
                exp.ExperimentId = expID;
                exp.ArtefactId = artefactID;
                exp.Experiment = experiment;
                exp.Artefact = expArtefact;              
                experiment.ExpArtefacts.Add(exp);
            }

            var judges = form["expJudges"];
            foreach (string x in judges)
            {
                var judgeName = x.Trim();
                var judge = await _context.Judge.
                    FirstOrDefaultAsync(m => m.Name == judgeName);
                var judgeID = judge.Id;
                ExpJudge exp = new ExpJudge();
                exp.ExperimentId = expID;
                exp.JudgeId = judgeID;
                exp.Experiment = experiment;
                exp.Judge = judge;
                experiment.ExpJudges.Add(exp);
            }
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
            EditExperimentViewModel EEVM = new EditExperimentViewModel();
            EEVM.Experiment = await _context.Experiment
                .Include(exp => exp.ExperimentParameters)
                .Include(exp => exp.ExpJudges)
                    .ThenInclude(judge => judge.Judge)
                .Include(exp => exp.ExpArtefacts)
                    .ThenInclude(artefact => artefact.Artefact)
                .FirstOrDefaultAsync(m => m.Id == id);
            EEVM.ExperimentParametersList = _context.ExperimentParameters.ToList();
            EEVM.ExperimentParametersList = _context.ExperimentParameters.ToList();
            EEVM.Artefacts = _context.Artefact.ToList();
            EEVM.Judges = _context.Judge.ToList();
            if (EEVM == null)
            {
                return NotFound();
            }
            return View(EEVM);
        }

        // POST: Experiments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Experiment experiment)
        {
            if (id != experiment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var form = Request.Form;
                    experiment.ExpArtefacts = new List<ExpArtefact>();
                    experiment.ExpJudges = new List<ExpJudge>();
                    var expID = experiment.Id;
                    string expNameParam = form["Parameters"];
                    var expParam = await _context.ExperimentParameters
                        .FirstOrDefaultAsync(m => m.Description == expNameParam);
                    int expParamID = expParam.Id;
                    experiment.ExperimentParametersId = expParamID;
                    experiment.ExperimentParameters = expParam;

                    var artefacts = form["expArtefacts"];
                    foreach (string x in artefacts)
                    {
                        var artefactName = x.Trim();
                        var expArtefact = await _context.Artefact.
                            FirstOrDefaultAsync(m => m.Name == artefactName);
                        var artefactID = expArtefact.Id;
                        ExpArtefact exp = new ExpArtefact();
                        exp.ExperimentId = expID;
                        exp.ArtefactId = artefactID;
                        exp.Experiment = experiment;
                        exp.Artefact = expArtefact;
                        experiment.ExpArtefacts.Add(exp);
                    }

                    var judges = form["expJudges"];
                    foreach (string x in judges)
                    {
                        var judgeName = x.Trim();
                        var judge = await _context.Judge.
                            FirstOrDefaultAsync(m => m.Name == judgeName);
                        var judgeID = judge.Id;
                        ExpJudge exp = new ExpJudge();
                        exp.ExperimentId = expID;
                        exp.JudgeId = judgeID;
                        exp.Experiment = experiment;
                        exp.Judge = judge;
                        experiment.ExpJudges.Add(exp);
                    }
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
