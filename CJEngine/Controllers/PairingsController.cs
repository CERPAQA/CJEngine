using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CJEngine.Models;
using CJEngine.Models.Join_Entities;
using RDotNet;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CJEngine.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PairingsController : Controller
    {
        private static List<Pairing> allPairings = new List<Pairing>();
        private static List<string> scriptsChosen = new List<string>();
        Dictionary<string, dynamic> expParams = new Dictionary<string, dynamic>();
        private readonly CJEngineContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        List<string> fileNames = new List<string>();
       
        public PairingsController(CJEngineContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public LocalRedirectResult GetExpID(int? id)
        {
            return LocalRedirect("/cj/" + id);
        }

        public async Task<Experiment> getCurrentExp(int id)
        {
            var liveExperiment = await _context.Experiment
               .Include(exp => exp.ExperimentParameters)
               .Include(exp => exp.ExpJudges)
                   .ThenInclude(judge => judge.Judge)
               .Include(exp => exp.ExpArtefacts)
                   .ThenInclude(artefact => artefact.Artefact)
               .FirstOrDefaultAsync(m => m.Id == id);

            return liveExperiment;
        }

        [HttpGet("[action]")]
        public async Task<int> IsTimerSet(int? id)
        {
            var liveExperiment = await getCurrentExp((int)id);
            int timer = liveExperiment.ExperimentParameters.Timer;
            return timer;
        }

        [HttpGet("[action]")]
        public List<string> GetFiles(Experiment Experiment)
        {
            foreach(ExpArtefact artefact in Experiment.ExpArtefacts)
            {
                string path = artefact.Artefact.FilePath;
                fileNames.Add(path);
            }
            return fileNames;
        }

        [Produces("application/json")]
        [HttpGet]
        [Route("GetParams")]
        public async Task<Dictionary<string, dynamic>> GetParams(int? id)
        {
            //TODO: edit params once they have been created ? and if so where does this happen?
            var liveExperiment = await getCurrentExp((int)id);
            string expTitle = liveExperiment.Name;
            bool showTimer = liveExperiment.ExperimentParameters.ShowTimer;
            bool showTitle = liveExperiment.ExperimentParameters.ShowTitle;
            bool addComment = liveExperiment.ExperimentParameters.AddComment;
            bool timeLine = liveExperiment.ExperimentParameters.TimeLine;
            expParams.Add("expTitle", expTitle);
            expParams.Add("showTimer", showTimer);
            expParams.Add("showTitle", showTitle);
            expParams.Add("addComment", addComment);
            expParams.Add("timeLine", timeLine);
            return expParams;
        }

        [HttpGet("[action]")]
        public List<Tuple<int, int>> GetPairings(int noScripts, int noPairings, string rScript)
        {
            REngineClass.GetREngine().Evaluate(@"source('REngine\\RScripts\\Generate\\"+ rScript +"')");
            NumericMatrix matrix = REngineClass.GetREngine().Evaluate(string.Format("matrix <- generatePairings(noOfScripts = {0}, noOfPairings = {1})", noScripts, noPairings)).AsNumericMatrix();

            List<Tuple<int, int>> pairings = new List<Tuple<int, int>>();

            for (int i = 0; i < matrix.RowCount; i++)
            {
                pairings.Add(new Tuple<int, int>((int)matrix[i, 0], (int)matrix[i, 1]));
            }
            return pairings;
        }

        [Produces("application/json")]
        [HttpGet]
        [Route("CreatePairings")]
        public async Task<List<Tuple<string, string>>> CreatePairings(int? id)
        {
            var liveExperiment = await getCurrentExp((int)id);
            var tempScript = await (
                from x in _context.Algorithm
                join ea in _context.ExpAlgorithm on liveExperiment.Id equals ea.ExperimentId
                where ea.AlgorithmId == x.Id
                select x).FirstOrDefaultAsync();
            string scriptName = tempScript.Filename.Replace("/../../", "");

            List<string> original = GetFiles(liveExperiment);
            List<Tuple<int, int>> result = GetPairings(fileNames.Count - 1, liveExperiment.ExperimentParameters.NumberOfPairings, scriptName); 
            List<Tuple<string, string>> finalResult = new List<Tuple<string, string>>();
            foreach (Tuple<int, int> x in result)
            {
                finalResult.Add(new Tuple<string, string>(original[x.Item1], original[x.Item2]));
            }
            return finalResult;
        }

        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Produces("application/json")]
        [HttpPost]
        [Route("GetWinners")]
        public async Task GetWinners([FromBody] dynamic data, int? id)
        {
            string winner = data.Winner;
            var winningArtefact = new Artefact();
            DateTime timeJudgement = DateTime.ParseExact((string)data.TimeOfPairing, "dd/MM/yyyy, HH:mm:ss", CultureInfo.InvariantCulture);
            int elapsedTime = (int)data.ElapsedTime;
            string comment = data.Comment;
            var user = GetCurrentUserAsync().Result.Id;
            Pairing pairing = new Pairing
            {
                ExperimentId = (int)id,
                Experiment = await _context.Experiment.FirstOrDefaultAsync(m => m.Id == id),
                JudgeLoginID = user
            };
            List<ArtefactPairing> pairOfScripts = new List<ArtefactPairing>();
            string scriptOne = data.ArtefactPairings["item1"];
            Artefact artefactOne = await _context.Artefact
                .FirstOrDefaultAsync(m => m.FilePath == scriptOne);
            ArtefactPairing one = new ArtefactPairing
            {
                ArtefactId = artefactOne.Id,
                PairingId = pairing.Id,
                Artefact = artefactOne
            };
            string scriptTwo = data.ArtefactPairings["item2"];
            Artefact artefactTwo = await _context.Artefact
                .FirstOrDefaultAsync(m => m.FilePath == scriptTwo);
            ArtefactPairing two = new ArtefactPairing
            {
                ArtefactId = artefactTwo.Id,
                PairingId = pairing.Id,
                Artefact = artefactTwo
            };
            if (artefactOne.FilePath == winner )
            {
                winningArtefact = artefactOne;
            }
            else
            {
                winningArtefact = artefactTwo;
            }
            pairOfScripts.Add(one);
            pairOfScripts.Add(two);
            pairing.ArtefactPairings = pairOfScripts;
            pairing.Winner = winningArtefact;
            pairing.TimeOfPairing = timeJudgement;
            pairing.ElapsedTime = elapsedTime;
            pairing.Comment = comment;
            if (ModelState.IsValid)
            {
                _context.Update(pairing);
                await _context.SaveChangesAsync();
            }
        }

        [HttpGet("[action]")]
        public string GetLeadingScript()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            int compare = 0;
            string mostFrequent = "";
            for (var i = 0; i < scriptsChosen.Count; i++)
            {
                //TODO: this needs changing, otherwise one gets added to each key during each iterations
                var word = scriptsChosen[i];

                if (counts.ContainsKey(word))
                {
                    counts[word] = counts[word] + 1;
                }
                else
                {
                    counts.Add(word, 1);
                }
                if (counts[word] > compare)
                {
                    compare = counts[word];
                    mostFrequent = scriptsChosen[i];
                }
                else if (counts[word] == compare)
                {
                    mostFrequent = "tie";
                }
            }
            return mostFrequent;
        }
    }
}

