using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CJEngine.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CJEngine.Controllers
{
    public class ListController : Controller
    {
        private readonly CJEngineContext _context;

        public IEnumerable<SelectListItem> GetArtefacts()
        {
            IEnumerable<SelectListItem> allArtefacts = (IEnumerable<SelectListItem>)_context.Artefact.ToList();
            return allArtefacts;
        }

        public ActionResult AllArtefacts()
        {
            return (ActionResult)GetArtefacts();
        }
    }
}