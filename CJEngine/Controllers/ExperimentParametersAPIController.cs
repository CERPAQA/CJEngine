using System.Threading.Tasks;
using CJEngine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// 

namespace CJEngine.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = ("Researcher"))]
    public class ExperimentParametersAPIController : Controller
    {
        private readonly CJEngineContext _context;

        public ExperimentParametersAPIController(CJEngineContext context)
        {
            _context = context;
        }
        //This method is used when adding new parameters and is called from the createExperiment.js file
        [Produces("application/json")]
        [HttpPost("[action]")]
        public async Task CreateParams([FromBody][Bind("Id,Description,ShowTitle,ShowTimer,AddComment,Timer, TimeLine, NumberOfPairings")] ExperimentParameters experimentParameters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experimentParameters);
                await _context.SaveChangesAsync();
            }
        }
    }
}
