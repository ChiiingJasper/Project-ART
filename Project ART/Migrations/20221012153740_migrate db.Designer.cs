﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_ART.Data;

#nullable disable

namespace Project_ART.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221012153740_migrate db")]
    partial class migratedb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project_ART.Models.TableAssessment", b =>
                {
                    b.Property<int>("Assessment_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Assessment_ID"), 1L, 1);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_Assessed")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExamID")
                        .HasColumnType("int");

                    b.Property<int>("InterviewID")
                        .HasColumnType("int");

                    b.HasKey("Assessment_ID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("ExamID");

                    b.HasIndex("InterviewID");

                    b.ToTable("Assessments");
                });

            modelBuilder.Entity("Project_ART.Models.TableCandidate", b =>
                {
                    b.Property<int>("Candidate_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Candidate_ID"), 1L, 1);

                    b.Property<int?>("ApplicationID")
                        .HasColumnType("int");

                    b.Property<int?>("AssessmentID")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<bool>("Is_Hired")
                        .HasColumnType("bit");

                    b.HasKey("Candidate_ID");

                    b.HasIndex("ApplicationID");

                    b.HasIndex("AssessmentID");

                    b.HasIndex("CompanyID");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("Project_ART.Models.TableDatasheet", b =>
                {
                    b.Property<int>("Data_Sheet_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Data_Sheet_ID"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Last_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkedIn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Middle_Initial")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Mobile_Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Data_Sheet_ID");

                    b.ToTable("Datasheets");
                });

            modelBuilder.Entity("Project_ART.Models.TableExam", b =>
                {
                    b.Property<int>("Exam_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Exam_ID"), 1L, 1);

                    b.Property<double>("Exam_Score")
                        .HasColumnType("float");

                    b.HasKey("Exam_ID");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("Project_ART.Models.TableInterview", b =>
                {
                    b.Property<int>("Interview_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Interview_ID"), 1L, 1);

                    b.Property<double>("Interview_Score")
                        .HasColumnType("float");

                    b.HasKey("Interview_ID");

                    b.ToTable("Interviews");
                });

            modelBuilder.Entity("Project_ART.Models.TableIntroduction", b =>
                {
                    b.Property<int>("Introduction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Introduction_ID"), 1L, 1);

                    b.Property<string>("B5_Trait")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DISC_Trait")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Introduction_ID");

                    b.ToTable("Introductions");
                });

            modelBuilder.Entity("Project_ART.Models.TableJobApplication", b =>
                {
                    b.Property<int>("Application_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Application_ID"), 1L, 1);

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int>("DatasheetID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_Received")
                        .HasColumnType("datetime2");

                    b.Property<int>("IntroductionID")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Approved")
                        .HasColumnType("bit");

                    b.HasKey("Application_ID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("DatasheetID");

                    b.HasIndex("IntroductionID");

                    b.ToTable("JobApplication");
                });

            modelBuilder.Entity("Project_ART.Models.TableKeyword", b =>
                {
                    b.Property<int>("Key_Word_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Key_Word_ID"), 1L, 1);

                    b.Property<int>("IntroductionID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time_Stamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key_Word_ID");

                    b.HasIndex("IntroductionID");

                    b.ToTable("KeyWords");
                });

            modelBuilder.Entity("Project_ART.Models.TableRecruiter", b =>
                {
                    b.Property<int>("Recruiter_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Recruiter_ID"), 1L, 1);

                    b.Property<int>("Company_ID")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Recruiter_ID");

                    b.ToTable("Recruiters");
                });

            modelBuilder.Entity("Project_ART.Models.TableSkill", b =>
                {
                    b.Property<int>("Skill_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Skill_ID"), 1L, 1);

                    b.Property<int>("DatasheetID")
                        .HasColumnType("int");

                    b.Property<string>("Skill_Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Skill_ID");

                    b.HasIndex("DatasheetID");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("Project_ART.Models.TableUser", b =>
                {
                    b.Property<int>("Company_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Company_ID"), 1L, 1);

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Admin")
                        .HasColumnType("bit");

                    b.Property<string>("Last_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Middle_Initial")
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Mobile_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profile_Pic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Company_ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_ART.Models.TableAssessment", b =>
                {
                    b.HasOne("Project_ART.Models.TableUser", "Users")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_ART.Models.TableExam", "Exams")
                        .WithMany()
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_ART.Models.TableInterview", "Interviews")
                        .WithMany()
                        .HasForeignKey("InterviewID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exams");

                    b.Navigation("Interviews");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Project_ART.Models.TableCandidate", b =>
                {
                    b.HasOne("Project_ART.Models.TableJobApplication", "JobApplication")
                        .WithMany()
                        .HasForeignKey("ApplicationID");

                    b.HasOne("Project_ART.Models.TableAssessment", "Assessments")
                        .WithMany()
                        .HasForeignKey("AssessmentID");

                    b.HasOne("Project_ART.Models.TableUser", "Users")
                        .WithMany()
                        .HasForeignKey("CompanyID");

                    b.Navigation("Assessments");

                    b.Navigation("JobApplication");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Project_ART.Models.TableJobApplication", b =>
                {
                    b.HasOne("Project_ART.Models.TableUser", "Users")
                        .WithMany()
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_ART.Models.TableDatasheet", "Datasheets")
                        .WithMany()
                        .HasForeignKey("DatasheetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_ART.Models.TableIntroduction", "Introductions")
                        .WithMany()
                        .HasForeignKey("IntroductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Datasheets");

                    b.Navigation("Introductions");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("Project_ART.Models.TableKeyword", b =>
                {
                    b.HasOne("Project_ART.Models.TableIntroduction", "Introductions")
                        .WithMany()
                        .HasForeignKey("IntroductionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Introductions");
                });

            modelBuilder.Entity("Project_ART.Models.TableSkill", b =>
                {
                    b.HasOne("Project_ART.Models.TableDatasheet", "Datasheets")
                        .WithMany()
                        .HasForeignKey("DatasheetID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Datasheets");
                });
#pragma warning restore 612, 618
        }
    }
}
