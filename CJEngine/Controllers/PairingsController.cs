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

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    public class PairingsController : Controller
    {
        private static List<Pairing> allPairings = new List<Pairing>();

		public List<string> GetFiles()
        {
            try
            {
                List<string> fileNames = new List<string>();
                string currentFile;
                string[] pdf = Directory.GetFiles("ClientApp\\public\\Root\\web", "*.pdf");
                string[] imgs = Directory.GetFiles("ClientApp\\public", "*.jpg");
                foreach (string dir in pdf)
                {
                    currentFile = Path.GetFileName(dir).ToLower();
                    fileNames.Add(currentFile);
                }

                foreach (string img in imgs)
                {
                    String relativeTo = "Root";
                    String relPath = Path.GetRelativePath(relativeTo, img);
                    string ImagePath = "/" + relPath.Replace("\\", "/");
                    fileNames.Add(ImagePath);
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
            REngineClass.GetREngine().Evaluate(@"source('C:\\Users\\owner\\Source\\Repos\\CERPAQA\\CJEngine\\CJEngine\\REngine\\RScripts\\ComparativeJudgmentPairingsTest.R')");
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
    }
}
