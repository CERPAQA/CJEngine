using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = ("Researcher"))]
    public class AlgorithmAPIController : Controller
    {
        private readonly CJEngineContext _context;
        private readonly IHostingEnvironment he;

        public AlgorithmAPIController(CJEngineContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }

        [HttpPost("[action]")]
        public async Task AddAlgorithm([FromBody][Bind("Id,FunctionName,Filename,Description,Valid")] dynamic data, Algorithm algorithm)
        {
            var formData = Request;
            string stringFile = data.file;
            stringFile = stringFile.Replace("C:\\fakepath\\", "");
            string description = data.description;
            var fileName = Path.Combine(he.WebRootPath + "/Rscripts", Path.GetFileName(stringFile));
            //file.CopyTo(new FileStream(fileName, FileMode.Create));
            
            algorithm.setAlgorithmAsRelativePath(fileName);
            algorithm.Description = description;
            if (ModelState.IsValid)
            {
                _context.Add(algorithm);
                await _context.SaveChangesAsync();
            }
        }
    }
}