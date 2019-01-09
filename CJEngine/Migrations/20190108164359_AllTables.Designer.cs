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
    [Migration("20190108164359_AllTables")]
    partial class AllTables
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

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Artefact");
                });

            modelBuilder.Entity("CJEngine.Models.Experiment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name");

                    b.Property<int?>("ParametersId");

                    b.HasKey("Id");

                    b.HasIndex("ParametersId");

                    b.ToTable("Experiment");
                });

            modelBuilder.Entity("CJEngine.Models.ExperimentParameters", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ShowTimer");

                    b.Property<bool>("ShowTitle");

                    b.HasKey("Id");

                    b.ToTable("ExperimentParameters");
                });

            modelBuilder.Entity("CJEngine.Models.Judge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Judge");
                });

            modelBuilder.Entity("CJEngine.Models.Pairing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Artefact1");

                    b.Property<int>("Artefact2");

                    b.Property<int>("ElapsedTime");

                    b.Property<DateTime>("TimeOfPairing");

                    b.Property<int>("Winner");

                    b.HasKey("Id");

                    b.ToTable("Pairing");
                });

            modelBuilder.Entity("CJEngine.Models.Researcher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Name");

                    b.HasKey("Id");

                    b.ToTable("Researcher");
                });

            modelBuilder.Entity("CJEngine.Models.Experiment", b =>
                {
                    b.HasOne("CJEngine.Models.ExperimentParameters", "Parameters")
                        .WithMany()
                        .HasForeignKey("ParametersId");
                });
#pragma warning restore 612, 618
        }
    }
}