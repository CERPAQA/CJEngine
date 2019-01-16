using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models;

namespace CJEngine.ViewModel
{
    public class ExperimentParameterModel
    {
        public Experiment Experiment { get; set; }
        public List<ExperimentParameters> Parameters { get; set; }
    }
}
