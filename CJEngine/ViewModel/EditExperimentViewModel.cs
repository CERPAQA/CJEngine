using CJEngine.Models;
using System.Collections.Generic;
namespace CJEngine.ViewModel
{
    public class EditExperimentViewModel
    {
        public Experiment Experiment { get; set; }
        public ExperimentParameters ExperimentParameters { get; set; }
        public IEnumerable<ExperimentParameters> ExperimentParametersList { get; set; }
        public IEnumerable<Artefact> Artefacts { get; set; }
        public IEnumerable<Judge> Judges { get; set; }
    }
}
