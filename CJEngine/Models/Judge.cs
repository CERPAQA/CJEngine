using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models.Join_Entities;

namespace CJEngine.Models
{
    public class Judge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public IList<Pairing> Pairings { get; set; }

        public IList<ExpJudge> ExpJudges { get; set; }
    }
}
