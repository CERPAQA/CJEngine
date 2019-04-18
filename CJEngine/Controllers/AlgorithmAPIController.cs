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
    public class AlgorithmAPIController : ControllerBase
    {
        private readonly CJEngineContext _context;
        private readonly IHostingEnvironment he;

        public AlgorithmAPIController(CJEngineContext context, IHostingEnvironment e)
        {
            _context = context;
            he = e;
        }

        [Produces("application/json")]
        [HttpPost("[action]")]
        public async Task AddAlgorithm([FromBody][Bind("Id,FunctionName,Filename,Description,Valid")] IFormFile file, Algorithm algorithm)
        {
            //TODO: add fetch call to the createexp.js file and test
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var fileName = Path.Combine(he.WebRootPath + "/Rscripts", Path.GetFileName(file.FileName));
                    file.CopyTo(new FileStream(fileName, FileMode.Create));
                    algorithm.setAlgorithmAsRelativePath(fileName);
                    _context.Add(algorithm);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}