using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CJEngine.Models;
using CJEngine.Models.Join_Entities;
using System.IO;
using RDotNet;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    public class PairingsController : Controller
    {
        private static List<Pairing> allPairings = new List<Pairing>();
        private static List<Judge> judges = new List<Judge>();
        private static List<int> ids = new List<int>();
        private static List<string> scriptsChosen = new List<string>();
        private static int maxJudges = 5;

        private readonly CJEngineContext _context;
        List<string> fileNames = new List<string>();
        Dictionary<string, bool> expParams = new Dictionary<string, bool>();

        public PairingsController(CJEngineContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public LocalRedirectResult GetExpID(int? id)
        {
            return LocalRedirect("/cj/" + id);
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
        public async Task<Dictionary<string, bool>> GetParams(int? id)
        {
            var liveExperiment = await _context.Experiment
               .Include(exp => exp.ExperimentParameters)
               .Include(exp => exp.ExpJudges)
                   .ThenInclude(judge => judge.Judge)
               .Include(exp => exp.ExpArtefacts)
                   .ThenInclude(artefact => artefact.Artefact)
               .FirstOrDefaultAsync(m => m.Id == id);
            bool showTimer = liveExperiment.ExperimentParameters.ShowTimer;
            bool showTitle = liveExperiment.ExperimentParameters.ShowTitle;
            expParams.Add("showTimer", showTimer);
            expParams.Add("showTitle", showTitle);

            return expParams;
        }

        [HttpGet("[action]")]
        public List<Tuple<int, int>> GetPairings(int noScripts, int noPairings)
        {
            REngineClass.GetREngine().Evaluate(@"source('REngine\\RScripts\\ComparativeJudgmentPairingsTest.R')");
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
            var liveExperiment = await _context.Experiment
               .Include(exp => exp.ExperimentParameters)
               .Include(exp => exp.ExpJudges)
                   .ThenInclude(judge => judge.Judge)
               .Include(exp => exp.ExpArtefacts)
                   .ThenInclude(artefact => artefact.Artefact)
               .FirstOrDefaultAsync(m => m.Id == id);
            List<string> original = GetFiles(liveExperiment);
            List<Tuple<int, int>> result = GetPairings(fileNames.Count - 1, 20); 
            List<Tuple<string, string>> finalResult = new List<Tuple<string, string>>();
            foreach (Tuple<int, int> x in result)
            {
                finalResult.Add(new Tuple<string, string>(original[x.Item1], original[x.Item2]));
            }
            return finalResult;
        }

        [HttpGet("[action]")]
        public int GenerateID()
        {
            Random rnd = new Random();
            int id;
            do
            {
                id = rnd.Next(1, maxJudges);
            }
            while (ids.Contains(id));
            maxJudges++; //Max judges is a low number to keep generated IDs low, it scales up if we want more browser windows to test.
            ids.Add(id);
            Judge j = new Judge(id);
            judges.Add(j);
            return id;
        }

        [Produces("application/json")]
        [HttpPost]
        [Route("GetWinners")]
        public async void GetWinners([FromBody] dynamic data, int? id)
        {
            var liveExperiment = await _context.Experiment
              .FirstOrDefaultAsync(m => m.Id == id);

            string winner = data.Winner;
            List<string> pairOfScripts = new List<string>();
            string scriptOne = data.ArtefactPairings["item1"];
            string scriptTwo = data.ArtefactPairings["item2"];
            pairOfScripts.Add(scriptOne);
            pairOfScripts.Add(scriptTwo);
            //DateTime now = DateTime.Now;
            CultureInfo culture = new CultureInfo("en-US");
            //DateTime timeJudgement = DateTime.ParseExact(data.TimeOfPairing,"dd/MM/yyyy, HH:mm:ss" , CultureInfo.InvariantCulture);
            //int elapsedTime = data.ElapsedTime;
            //int judgeID = data.judgeID;
            Pairing pairing = new Pairing();
            //allPairings.Add(p);
            //scriptsChosen.Add(winner);

        }

        [HttpGet("[action]")]
        public string GetLeadingScript()
        {
            Dictionary<string, int> counts = new Dictionary<string, int>();
            int compare = 0;
            string mostFrequent = "";
            for (var i = 0; i < scriptsChosen.Count; i++)
            {
                // this needs changing, otherwise one gets added to each key during each iterations
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

        [HttpGet("[action]")]
        public string GenerateCSVString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Winner,");
            sb.Append("Script One,");
            sb.Append("Script Two,");
            sb.Append("Date,");
            sb.Append("TimeJudged,");
            sb.Append("ElapsedTime,");
            sb.Append("Judge");
            sb.AppendLine();
            for (int i = 0; i < allPairings.Count; i++)
            {
                //Just cleaning the strings up a little bit for easier reading/analysis
                /*sb.Append(allPairings[i].winner.Replace("/images/", "") + ",");
                sb.Append(allPairings[i].pairOfScripts.ToString().Replace("/images/", "").Replace("(", "").Replace(")", "") + ",");
                sb.Append(allPairings[i].timeJudgement + ",");
                sb.Append(allPairings[i].elapsedTime + ",");
                sb.Append(allPairings[i].judgeID + ",");*/
                sb.AppendLine();
            }
            return sb.ToString();
        }

        [HttpGet("[action]")]
        public FileContentResult ReportBuilder()
        {
            var csv = GenerateCSVString();
            var fileName = "report.csv";

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", fileName);
        }

        /*public class Pairing
        {
            public string winner { get; set; }
            public Tuple<string, string> pairOfScripts { get; set; }
            public string timeJudgement { get; set; }
            public string elapsedTime { get; set; }
            public int judgeID;

            public Pairing(string winner, Tuple<string, string> pairOfScripts, string timeJudgement, string elapsedTime, int id)
            {
                this.winner = winner;
                this.pairOfScripts = pairOfScripts;
                this.timeJudgement = timeJudgement;
                this.elapsedTime = elapsedTime;
                this.judgeID = id;
            }
        }*/

        public class Judge
        {
            public int judgeID { get; set; }
            public Judge(int judgeID)
            {
                this.judgeID = judgeID;
            }
        }
    }
}

