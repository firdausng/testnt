﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Testnt.Main.Infrastructure.Data;

namespace Testnt.Main.Infrastructure.Data.Migrations
{
    [DbContext(typeof(TestntDbContext))]
    [Migration("20200304161755_addtenantIdConstructor")]
    partial class addtenantIdConstructor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TenantId");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("Name");

                    b.HasIndex("TenantId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.ProjectUser", b =>
                {
                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("ProjectId", "UserProfileId");

                    b.HasIndex("UserProfileId");

                    b.ToTable("ProjectUser");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Scenario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("FeatureId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.Property<int>("TestExecutionResult")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("FeatureId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TenantId");

                    b.ToTable("Scenarios");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.ScenarioStep", b =>
                {
                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("StepId")
                        .HasColumnType("uuid");

                    b.HasKey("ScenarioId", "StepId");

                    b.HasIndex("StepId");

                    b.ToTable("ScenarioStep");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Step", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Steps");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TenantId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.TagLink", b =>
                {
                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TagId")
                        .HasColumnType("uuid");

                    b.HasKey("ScenarioId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TagLink");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.ScenarioSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<int>("ExecutionResult")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ScenarioId")
                        .HasColumnType("uuid");

                    b.Property<string>("ScenarioName")
                        .HasColumnType("text");

                    b.Property<Guid?>("SessionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.HasIndex("TenantId");

                    b.ToTable("ScenarioSnapshot");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Finished")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Started")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TenantId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.StepSnapshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ScenarioSnapshotId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ScenarioSnapshotId");

                    b.HasIndex("TenantId");

                    b.ToTable("StepSnapshot");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Feature", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Project", "Project")
                        .WithMany("Features")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.ProjectUser", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.UserProfile", "UserProfile")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Project", "Project")
                        .WithMany("Members")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Scenario", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Feature", null)
                        .WithMany("Scenarios")
                        .HasForeignKey("FeatureId");

                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Project", "Project")
                        .WithMany("Scenarios")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.ScenarioStep", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Scenario", "Scenario")
                        .WithMany("ScenarioSteps")
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Step", "Step")
                        .WithMany("ScenarioSteps")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.Tag", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Project", null)
                        .WithMany("Tags")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.Projects.TagLink", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Tag", "Tag")
                        .WithMany("TagLinks")
                        .HasForeignKey("ScenarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Scenario", "Scenario")
                        .WithMany("Tags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.ScenarioSnapshot", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.Session", null)
                        .WithMany("ScenarioSnapshot")
                        .HasForeignKey("SessionId");
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.Session", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.Projects.Project", "Project")
                        .WithMany("Sessions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.StepSnapshot", b =>
                {
                    b.HasOne("Testnt.Main.Domain.Entity.TestSessionEntity.Projects.ScenarioSnapshot", null)
                        .WithMany("StepSnapshot")
                        .HasForeignKey("ScenarioSnapshotId");
                });
#pragma warning restore 612, 618
        }
    }
}
