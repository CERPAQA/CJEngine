using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models
{
    public class ExperimentParameters
    {
        public int Id { get; set; }
        public Experiment Experiment { get; set; }
        public bool ShowTitle { get; set; }
        public bool ShowTimer { get; set; }
    }
}
