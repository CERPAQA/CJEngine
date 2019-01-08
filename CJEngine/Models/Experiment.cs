using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models
{
    public class Experiment
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public ExperimentParameters Parameters {get; set; }
    }
}
