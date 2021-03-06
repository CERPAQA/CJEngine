﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CJEngine.Models.Join_Entities
{
    public class ArtefactPairing
    {
        public int ArtefactId { get; set; }
        public Artefact Artefact { get; set; }

        public int PairingId { get; set; }
        public Pairing Pairing { get; set; }
    }
}
