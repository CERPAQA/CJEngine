using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using System.IO;

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PairingsController : Controller
    {
        private static List<Pairing> allPairings = new List<Pairing>();

		public List<string> GetFiles()
        {
            try
            {
                List<string> fileNames = new List<string>();
                string currentFile;
                string[] pdf = Directory.GetFiles("wwwroot\\pdfjs-2.0.943-dist\\web", "*.pdf");
                string[] imgs = Directory.GetFiles("wwwroot\\images", "*.jpg");
                foreach (string dir in pdf)
                {
                    currentFile = Path.GetFileName(dir).ToLower();
                    fileNames.Add(currentFile);
                }

                foreach (string img in imgs)
                {
                    String relativeTo = "wwwroot";
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
    }
}
