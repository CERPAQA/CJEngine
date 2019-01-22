using CJEngine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.ViewModel
{
    public class CreateExperimentViewModel
    {
        public Experiment Experiment { get; set; }
        public ExperimentParameters ExperimentParameters { get; set; }
        public IEnumerable<ExperimentParameters> ExperimentParametersList { get; set; }
        public IEnumerable<Artefact> Artefacts { get; set; }
    }
}
