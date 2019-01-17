using CJEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.ViewModel
{
    public class ExperimentParameterViewModel
    {
        public Experiment Experiment { get; set; }
        public IEnumerable<ExperimentParameters> Parameters { get; set; }
    }
}
