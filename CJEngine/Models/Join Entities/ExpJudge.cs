using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models.Join_Entities
{
    public class ExpJudge
    {
        public int ExperimentId { get; set; }
        [NotMapped]
        public Experiment Experiment { get; set; }

        public int JudgeId { get; set; }
        [NotMapped]
        public Judge Judge { get; set; }
    }
}
