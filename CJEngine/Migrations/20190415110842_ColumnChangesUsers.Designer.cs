﻿// <auto-generated />
using System;
using CJEngine.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CJEngine.Migrations
{
    [DbContext(typeof(CJEngineContext))]
    [Migration("20190415110842_ColumnChangesUsers")]
    partial class ColumnChangesUsers
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CJEngine.Models.Algorithm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Filename");

                    b.Property<string>("FunctionName");

                    b.Property<bool>("Valid");

                    b.HasKey("Id");

                    b.ToTable("Algorithm");
                });

            modelBuilder.Entity("CJEngine.Models.Artefact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Artefact");
                });

            modelBuilder.Entity("CJEngine.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<int>("ExperimentParametersId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentParametersId");

                    b.ToTable("Experiment");
                });

            modelBuilder.Entity("CJEngine.Models.ExperimentParameters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AddComment");

                    b.Property<string>("Description");

                    b.Property<bool>("ShowTimer");

                    b.Property<bool>("ShowTitle");

                    b.Property<int>("Timer");

                    b.HasKey("Id");

                    b.ToTable("ExperimentParameters");
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ArtefactPairing", b =>
                {
                    b.Property<int>("ArtefactId");

                    b.Property<int>("PairingId");

                    b.HasKey("ArtefactId", "PairingId");

                    b.HasIndex("PairingId");

                    b.ToTable("ArtefactPairings");
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpAlgorithm", b =>
                {
                    b.Property<int>("ExperimentId");

                    b.Property<int>("AlgorithmId");

                    b.HasKey("ExperimentId", "AlgorithmId");

                    b.HasIndex("AlgorithmId");

                    b.ToTable("ExpAlgorithm");
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpArtefact", b =>
                {
                    b.Property<int>("ExperimentId");

                    b.Property<int>("ArtefactId");

                    b.HasKey("ExperimentId", "ArtefactId");

                    b.HasIndex("ArtefactId");

                    b.ToTable("ExpArtefact");
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpJudge", b =>
                {
                    b.Property<int>("ExperimentId");

                    b.Property<string>("JudgeLoginId");

                    b.Property<int?>("JudgeId");

                    b.HasKey("ExperimentId", "JudgeLoginId");

                    b.HasIndex("JudgeId");

                    b.ToTable("ExpJudge");
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpResearcher", b =>
                {
                    b.Property<int>("ExperimentId");

                    b.Property<string>("ResearcherLoginId");

                    b.Property<int?>("ResearcherId");

                    b.HasKey("ExperimentId", "ResearcherLoginId");

                    b.HasIndex("ResearcherId");

                    b.ToTable("ExpResearcher");
                });

            modelBuilder.Entity("CJEngine.Models.Judge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("LoginId");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Judge");
                });

            modelBuilder.Entity("CJEngine.Models.Pairing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<int>("ElapsedTime");

                    b.Property<int>("ExperimentId");

                    b.Property<int?>("JudgeId");

                    b.Property<string>("JudgeLoginID");

                    b.Property<DateTime>("TimeOfPairing");

                    b.Property<int>("WinnerId");

                    b.HasKey("Id");

                    b.HasIndex("ExperimentId");

                    b.HasIndex("JudgeId");

                    b.HasIndex("WinnerId");

                    b.ToTable("Pairing");
                });

            modelBuilder.Entity("CJEngine.Models.Researcher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("LoginId");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Researcher");
                });

            modelBuilder.Entity("CJEngine.Models.Experiment", b =>
                {
                    b.HasOne("CJEngine.Models.ExperimentParameters", "ExperimentParameters")
                        .WithMany()
                        .HasForeignKey("ExperimentParametersId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ArtefactPairing", b =>
                {
                    b.HasOne("CJEngine.Models.Artefact", "Artefact")
                        .WithMany("ArtefactPairings")
                        .HasForeignKey("ArtefactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CJEngine.Models.Pairing", "Pairing")
                        .WithMany("ArtefactPairings")
                        .HasForeignKey("PairingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpAlgorithm", b =>
                {
                    b.HasOne("CJEngine.Models.Algorithm", "Algorithm")
                        .WithMany("ExpAlgorithms")
                        .HasForeignKey("AlgorithmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CJEngine.Models.Experiment", "Experiment")
                        .WithMany("ExpAlgorithms")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpArtefact", b =>
                {
                    b.HasOne("CJEngine.Models.Artefact", "Artefact")
                        .WithMany("ExpArtefacts")
                        .HasForeignKey("ArtefactId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CJEngine.Models.Experiment", "Experiment")
                        .WithMany("ExpArtefacts")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpJudge", b =>
                {
                    b.HasOne("CJEngine.Models.Experiment", "Experiment")
                        .WithMany("ExpJudges")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CJEngine.Models.Judge", "Judge")
                        .WithMany("ExpJudges")
                        .HasForeignKey("JudgeId");
                });

            modelBuilder.Entity("CJEngine.Models.Join_Entities.ExpResearcher", b =>
                {
                    b.HasOne("CJEngine.Models.Experiment", "Experiment")
                        .WithMany("ExpResearchers")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CJEngine.Models.Researcher", "Researcher")
                        .WithMany("ExpResearchers")
                        .HasForeignKey("ResearcherId");
                });

            modelBuilder.Entity("CJEngine.Models.Pairing", b =>
                {
                    b.HasOne("CJEngine.Models.Experiment")
                        .WithMany("Pairings")
                        .HasForeignKey("ExperimentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CJEngine.Models.Judge")
                        .WithMany("Pairings")
                        .HasForeignKey("JudgeId");

                    b.HasOne("CJEngine.Models.Artefact", "Winner")
                        .WithMany()
                        .HasForeignKey("WinnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
