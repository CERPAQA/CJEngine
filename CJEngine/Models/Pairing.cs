using CJEngine.Models.Join_Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        public int WinnerId { get; set; }
        public Artefact Winner { get; set; }

        [DataType(DataType.Date)]
        public DateTime TimeOfPairing { get; set; }
        public int ElapsedTime { get; set; }
        public string Comment { get; set; }
    }
}
