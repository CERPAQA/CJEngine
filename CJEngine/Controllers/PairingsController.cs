using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using System.IO;
using RDotNet;
using System.Text;

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

        public List<string> GetFiles()
        {
            try
            {
                List<string> fileNames = new List<string>();
                string[] pdfs = Directory.GetFiles("ClientApp\\public\\pdf", "*.pdf");
                string[] imgs = Directory.GetFiles("ClientApp\\public\\images", "*.jpg");
                foreach (string pdf in pdfs)
                {
                    String relativeTo = "ClientApp\\public";
                    String relPath = Path.GetRelativePath(relativeTo, pdf);
                    string pdfPath = "/" + relPath.Replace("\\", "/");
                    fileNames.Add(pdfPath);
                }

                foreach (string img in imgs)
                {
                    String relativeTo = "ClientApp\\public";
                    String relPath = Path.GetRelativePath(relativeTo, img);
                    string imagePath = "/" + relPath.Replace("\\", "/");
                    fileNames.Add(imagePath);
                }
                return fileNames;
            }
            catch (Exception e)
            {
                List<string> error = new List<string>();
                error.Add(e.ToString());
                return error;
            }
        }

        public List<Tuple<int, int>> GetPairings(int noScripts, int noPairings)
        {
            REngineClass.GetREngine().Evaluate(@"source('REngine\\RScripts\\ComparativeJudgmentPairingsTest.R')");
           // REngineClass.GetREngine().Evaluate(@"source('C:\\Users\\owner\\Source\\Repos\\CERPAQA\\CJEngine\\CJEngine\\REngine\\RScripts\\ComparativeJudgmentPairingsTest.R')");
            NumericMatrix matrix = REngineClass.GetREngine().Evaluate(string.Format("matrix <- generatePairings(noOfScripts = {0}, noOfPairings = {1})", noScripts, noPairings)).AsNumericMatrix();

            //create an empty list of tuples
            List<Tuple<int, int>> pairings = new List<Tuple<int, int>>();

            for (int i = 0; i < matrix.RowCount; i++)
            {
                //add new tuple to list
                pairings.Add(new Tuple<int, int>((int)matrix[i, 0], (int)matrix[i, 1]));
            }
            return pairings;
        }

        [HttpGet("[action]")]
        public List<Tuple<string, string>> CreatePairings()
        {
            //taken from line 90 above in get file method
            List<Tuple<int, int>> result = GetPairings(GetFiles().Count - 1, 20); //change seoond number back to 30 once done testing counter
            List<string> original = GetFiles();
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
            //not yet working smoothly, needs more testing and work.
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
        public string GetWinners([FromBody] Pairing data)
        {
            string winner = data.winner;
            Tuple<string, string> pairOfScripts = data.pairOfScripts;
            string timeJudgement = data.timeJudgement;
            string elapsedTime = data.elapsedTime;
            int judgeID = data.judgeID;

            Pairing p = new Pairing(winner, pairOfScripts, timeJudgement, elapsedTime, judgeID);
            allPairings.Add(p);
            scriptsChosen.Add(winner);
            return winner;
        }


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
                sb.Append(allPairings[i].winner.Replace("/images/", "") + ",");
                sb.Append(allPairings[i].pairOfScripts.ToString().Replace("/images/", "").Replace("(", "").Replace(")", "") + ",");
                sb.Append(allPairings[i].timeJudgement + ",");
                sb.Append(allPairings[i].elapsedTime + ",");
                sb.Append(allPairings[i].judgeID + ",");
                sb.AppendLine();
            }
            return sb.ToString();
        }

        [HttpGet]
        public FileContentResult ReportBuilder()
        {
            var csv = GenerateCSVString();
            var fileName = "report.csv";

            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", fileName);
        }

        public class Pairing
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
        }

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

