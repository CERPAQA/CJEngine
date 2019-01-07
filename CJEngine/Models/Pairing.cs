using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models
{
    public class Pairing
    {
        public int Id { get; set; }
        public int JudgeID;
        public int Artefact1 { get; set; }
        public int Artefact2 { get; set; }
        public int Winner { get; set; }
        [DataType(DataType.Date)]
        public DateTime TimeOfPairing { get; set; }
        public int ElapsedTime { get; set; }
    }
}
