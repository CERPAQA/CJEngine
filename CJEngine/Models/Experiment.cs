using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models.Join_Entities;

namespace CJEngine.Models
{
    public class Experiment
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public ExperimentParameters ExperimentParameters { get; set; }
        public int ExperimentParametersId { get; set; }

        public IList<ExpAlgorithm> ExpAlgorithms { get; set; }
        public IList<ExpArtefact> ExpArtefacts { get; set; }
        public IList<ExpJudge> ExpJudges { get; set; }
        public IList<ExpResearcher> ExpResearchers { get; set; }
    }
}
