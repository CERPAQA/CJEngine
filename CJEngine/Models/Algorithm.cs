using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models.Join_Entities;

namespace CJEngine.Models
{
    public class Algorithm
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }
        public string Filename { get; set; }
        public string Description { get; set; }
        public bool Valid { get; set; }

        public IList<ExpAlgorithm> ExpAlgorithms { get; set; }
    }
}
