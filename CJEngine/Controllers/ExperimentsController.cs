using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using CJEngine.Models.Join_Entities;
using CJEngine.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace CJEngine.Controllers
{
    [Authorize]
    public class ExperimentsController : Controller
    {
        private readonly CJEngineContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExperimentsController(CJEngineContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Experiments
        [Authorize(Roles = ("Researcher"))]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var experiments = await (
                from exp in _context.Experiment
                join r in _context.Researcher on user.Id equals r.LoginId
                join er in _context.ExpResearcher on r.LoginId equals er.ResearcherLoginId
                where exp.Id == er.ExperimentId
                select exp)
                .ToListAsync();
            return View(experiments);
        }

        //This method is what renders when the CJ option is selected on the NavBar
        [Authorize(Roles =("Judge, Researcher"))]
        public async Task<IActionResult> CJIndex()
        {
            var user = await GetCurrentUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains("Judge"))
            {
                var experiments = await (
                from exp in _context.Experiment
                join j in _context.Judge on user.Id equals j.LoginId
                join ej in _context.ExpJudge on j.LoginId equals ej.JudgeLoginId
                where exp.Id == ej.ExperimentId
                select exp)
                .ToListAsync();
                return View(experiments);
            } 
            else if (roles.Contains("Researcher"))
            {
                var experiments = await (
                from exp in _context.Experiment
                join r in _context.Researcher on user.Id equals r.LoginId
                join er in _context.ExpResearcher on r.LoginId equals er.ResearcherLoginId
                where exp.Id == er.ExperimentId
                select exp)
                .ToListAsync();
                return View(experiments);
            }
            return View();
        }

        // GET: Experiments/Details/5
        [Authorize(Roles = ("Researcher"))]
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
                    .ThenInclude(artefact => artefact.Artefact)
               .Include(exp => exp.ExpAlgorithms)
                    .ThenInclude(algorithm => algorithm.Algorithm)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (experiment == null)
            {
                return NotFound();
            }

            return View(experiment);
        }

        public async Task<FileContentResult> GenerateReport(int? id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Experiment ID,");
            sb.Append("Winner,");
            sb.Append("Script One,");
            sb.Append("Script Two,");
            sb.Append("Judge,");
            sb.Append("Date,");
            sb.Append("TimeJudged,");
            sb.Append("ElapsedTime,"); 
            sb.Append("Timer,"); 
            //TODO: see below
            //sb.Append("Timer (s),"); whatever the seconds parameter was e.g 20 seconds
            sb.Append("Comments,");
            // whatever comment was left perhaps in here??
            sb.Append("Title,");
            sb.AppendLine();

            var records = await (
                from p in _context.Pairing
                where p.ExperimentId == id
                select p).ToListAsync();

            foreach(var pairing in records)
            {
                //TODO: can this for loop be refactored?
                var winner = await _context.Artefact
                    .Where(a => a.Id == pairing.WinnerId)
                    .FirstOrDefaultAsync();

                var artefactIDList = await _context.ArtefactPairings
                    .Where(ap => ap.PairingId == pairing.Id).ToListAsync();

                var scriptOne = await _context.Artefact
                    .Where(s => s.Id == artefactIDList[0].ArtefactId)
                    .FirstOrDefaultAsync();
                var scriptTwo = await _context.Artefact
                    .Where(s => s.Id == artefactIDList[1].ArtefactId)
                    .FirstOrDefaultAsync();

                var judgeName = await (
                    from j in _context.Judge
                    where j.LoginId == pairing.JudgeLoginID
                    select j.Name).FirstOrDefaultAsync();

                var expParam = await (
                    from ex in _context.ExperimentParameters
                    join e in _context.Experiment on id equals e.Id
                    where ex.Id == e.ExperimentParametersId
                    select ex
                    ).FirstOrDefaultAsync();

                var judgeDate = pairing.TimeOfPairing.Date.ToString("d");
                var judgeTime = pairing.TimeOfPairing.TimeOfDay.ToString();
                var elapsedTimeMS = pairing.ElapsedTime * 1000;

                sb.Append(id + ",");
                sb.Append(winner.Name + ",");
                sb.Append(scriptOne.Name + ",");
                sb.Append(scriptTwo.Name + ",");
                sb.Append(judgeName + ",");
                sb.Append(judgeDate + ",");
                sb.Append(judgeTime + ",");
                sb.Append(elapsedTimeMS + ",");
                sb.Append(expParam.ShowTimer + ",");
                sb.Append(expParam.AddComment + ",");
                sb.Append(expParam.ShowTitle + ",");
                sb.AppendLine();
            }
            var fullReport = sb.ToString(); // can probably take this out
            var fileName = "report.csv";

            return File(new System.Text.UTF8Encoding().GetBytes(fullReport), "text/csv", fileName);
        }

        // GET: Experiments/Create
        [Authorize(Roles = ("Researcher"))]
        public IActionResult Create()
        {
            CreateExperimentViewModel CEVM = new CreateExperimentViewModel();
            CEVM.ExperimentParametersList = _context.ExperimentParameters.ToList();
            CEVM.Artefacts = _context.Artefact.ToList();
            CEVM.Judges = _context.Judge.ToList();
            CEVM.Algorithms = _context.Algorithm.ToList();
            return View(CEVM);
        }

        // POST: Experiments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ("Researcher"))]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Experiment experiment)
        {
            var form = Request.Form;
            experiment.ExpArtefacts = new List<ExpArtefact>();
            experiment.ExpJudges = new List<ExpJudge>();
            experiment.ExpResearchers = new List<ExpResearcher>();
            experiment.ExpAlgorithms = new List<ExpAlgorithm>();

            var expID = experiment.Id;
            string expNameParam = form["Parameters"];
            var expParam = await _context.ExperimentParameters
                .FirstOrDefaultAsync(m => m.Description == expNameParam);
            int expParamID = expParam.Id;
            experiment.ExperimentParametersId = expParamID;
            experiment.ExperimentParameters = expParam;

            var expAlgorithmName = form["algorithms"];
            var expAlgorithm = await _context.Algorithm
                .FirstOrDefaultAsync(m => m.Description == expAlgorithmName);
            int expAlgorithmID = expAlgorithm.Id;
            ExpAlgorithm expAL = new ExpAlgorithm();
            expAL.AlgorithmId = expAlgorithmID;
            expAL.Algorithm = expAlgorithm;
            expAL.Experiment = experiment;
            expAL.ExperimentId = expID;
            experiment.ExpAlgorithms.Add(expAL);

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
                exp.JudgeLoginId = judge.LoginId;
                exp.Experiment = experiment;
                exp.Judge = judge;
                experiment.ExpJudges.Add(exp);
            }

            var researcher = GetCurrentUserAsync().Result.Id;
            ExpResearcher expResearcher = new ExpResearcher();
            expResearcher.ResearcherLoginId = researcher;
            expResearcher.Researcher = await _context.Researcher.
                FirstOrDefaultAsync(r => r.LoginId == researcher);
            expResearcher.ExperimentId = expID;
            expResearcher.Experiment = experiment;
            experiment.ExpResearchers.Add(expResearcher);

            if (ModelState.IsValid)
            {
                _context.Add(experiment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(experiment);
        }

        // GET: Experiments/Edit/5
        [Authorize(Roles = ("Researcher"))]
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
        [Authorize(Roles = ("Researcher"))]
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
                        exp.JudgeLoginId = judge.LoginId;
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
        [Authorize(Roles = ("Researcher"))]
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
        [Authorize(Roles = ("Researcher"))]
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
