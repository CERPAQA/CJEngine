﻿using CJEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.ViewModel
{
    public class ExperimentParameterViewModel
    {
        private readonly CJEngineContext _context;
        public Experiment Experiment { get; set; }
        //public IEnumerable<ExperimentParameters> Parameters { get; set; }
        public Artefact artefact { get; set; }
        public Artefact selected { get; set; }

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
