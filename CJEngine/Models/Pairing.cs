using CJEngine.Models.Join_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models
{
    public class Pairing
    {
        public int Id { get; set; }

        public int JudgeId { get; set; } 
        public Judge Judge;

        public int ExperimentId { get; set; }
        public Experiment Experiment;

        public IList<ArtefactPairing> ArtefactPairings { get; set; }

        public int ArtefactId { get; set; }
        public Artefact Winner { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeOfPairing { get; set; }
        public int ElapsedTime { get; set; }
    }
}
