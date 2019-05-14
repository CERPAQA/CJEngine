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
        public Experiment Experiment { get; set; }
        public string JudgeLoginId { get; set; }
        public Judge Judge { get; set; }
    }
}
