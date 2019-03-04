using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models.Join_Entities
{
    public class ExpResearcher
    {
        public int ExperimentId { get; set; }
        public Experiment Experiment { get; set; }
        public int ResearcherId { get; set; }
        public Researcher Researcher { get; set; }
    }
}
