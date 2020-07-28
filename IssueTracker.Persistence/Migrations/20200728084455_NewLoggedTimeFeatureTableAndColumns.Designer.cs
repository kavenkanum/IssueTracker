﻿// <auto-generated />
using System;
using IssueTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IssueTracker.Migrations
{
    [DbContext(typeof(IssueTrackerDbContext))]
    [Migration("20200728084455_NewLoggedTimeFeatureTableAndColumns")]
    partial class NewLoggedTimeFeatureTableAndColumns
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IssueTracker.Domain.Entities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfComment")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AssignedUserId")
                        .HasColumnType("bigint");

                    b.Property<long>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("DateOfCreate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EstimatedTime")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FinishedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("JobDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedUserId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.LoggedTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<double>("LoggedTimeSlice")
                        .HasColumnType("float");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("UserId");

                    b.ToTable("LoggedTime");
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.StartsAfterJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("JobId")
                        .HasColumnType("int");

                    b.Property<int>("StartsAfterJobId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("StartsAfterJob");
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("IssueTracker.Domain.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.Comment", b =>
                {
                    b.HasOne("IssueTracker.Domain.Entities.Job", null)
                        .WithMany("Comments")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.Job", b =>
                {
                    b.HasOne("IssueTracker.Domain.Entities.User", null)
                        .WithMany("AssignedTasks")
                        .HasForeignKey("AssignedUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("IssueTracker.Domain.Project", null)
                        .WithMany("Jobs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("IssueTracker.Domain.Language.ValueObjects.Deadline", "Deadline", b1 =>
                        {
                            b1.Property<int>("JobId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<DateTime>("DeadlineDate")
                                .HasColumnName("Deadline")
                                .HasColumnType("datetime2");

                            b1.HasKey("JobId");

                            b1.ToTable("Jobs");

                            b1.WithOwner()
                                .HasForeignKey("JobId");
                        });
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.LoggedTime", b =>
                {
                    b.HasOne("IssueTracker.Domain.Entities.Job", null)
                        .WithMany("TotalLoggedTime")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IssueTracker.Domain.Entities.User", null)
                        .WithMany("LoggedTimes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IssueTracker.Domain.Entities.StartsAfterJob", b =>
                {
                    b.HasOne("IssueTracker.Domain.Entities.Job", null)
                        .WithMany("StartsAfterJobs")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
