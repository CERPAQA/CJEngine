using CJEngine.Models;
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
        public Experiment Experiment { get; set; }
        public ExperimentParameters ExperimentParameters { get; set; }
        public IEnumerable<Artefact> Artefacts { get; set; }
    }
}
