using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;

namespace CJEngine.Models
{
    public class CJEngineContext : DbContext
    {
        public CJEngineContext (DbContextOptions<CJEngineContext> options)
            : base(options)
        {
        }

        public DbSet<CJEngine.Models.Pairing> Pairing { get; set; }

        public DbSet<CJEngine.Models.Judge> Judge { get; set; }

        public DbSet<CJEngine.Models.Algorithm> Algorithm { get; set; }

        public DbSet<CJEngine.Models.Artefact> Artefact { get; set; }

        public DbSet<CJEngine.Models.Experiment> Experiment { get; set; }

        public DbSet<CJEngine.Models.Researcher> Researcher { get; set; }
    }
}
