using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJEngine.Models.Join_Entities;

namespace CJEngine.Models
{
    public class Artefact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }

        public IList<ExpArtefact> ExpArtefacts { get; set; }
        public IList<ArtefactPairing> ArtefactPairings { get; set; }
    }
}
