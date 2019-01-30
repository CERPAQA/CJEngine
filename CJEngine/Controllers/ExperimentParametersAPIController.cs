using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    public class ExperimentParametersAPIController : Controller
    {
        private readonly CJEngineContext _context;

        public ExperimentParametersAPIController(CJEngineContext context)
        {
            _context = context;
        }

        [Produces("application/json")]
        [HttpPost("[action]")]
        public async Task CreateParams([FromBody][Bind("Id,Description,ShowTitle,ShowTimer")] ExperimentParameters experimentParameters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experimentParameters);
                await _context.SaveChangesAsync();
            }
        }
    }
}
