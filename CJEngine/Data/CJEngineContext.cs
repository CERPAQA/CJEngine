using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CJEngine.Models;
using CJEngine.Models.Join_Entities;

namespace CJEngine.Models
{
    public class CJEngineContext : DbContext
    {
        public CJEngineContext (DbContextOptions<CJEngineContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpAlgorithm>().HasKey(ea => new { ea.ExperimentId, ea.AlgorithmId });
            modelBuilder.Entity<ExpArtefact>().HasKey(ea => new { ea.ExperimentId, ea.ArtefactId });
            modelBuilder.Entity<ExpJudge>().HasKey(ej => new { ej.ExperimentId, ej.JudgeId });
            modelBuilder.Entity<ExpResearcher>().HasKey(er => new { er.ExperimentId, er.ResearcherId });
            modelBuilder.Entity<ArtefactPairing>().HasKey(ap => new { ap.ArtefactId, ap.PairingId});
        }

        public DbSet<Pairing> Pairing { get; set; }

        public DbSet<Judge> Judge { get; set; }

        public DbSet<Algorithm> Algorithm { get; set; }

        public DbSet<Artefact> Artefact { get; set; }

        public DbSet<Experiment> Experiment { get; set; }

        public DbSet<Researcher> Researcher { get; set; }

        public DbSet<ExpAlgorithm> ExpAlgorithm { get; set; }

        public DbSet<ExpArtefact> ExpArtefact { get; set; }

        public DbSet<ExpJudge> ExpJudge { get; set; }

        public DbSet<ExpResearcher> ExpResearcher { get; set; }

        public DbSet<ArtefactPairing> ArtefactPairings { get; set; }

    }

}
