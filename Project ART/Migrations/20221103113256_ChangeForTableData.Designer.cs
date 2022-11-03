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
    [Migration("20221103113256_ChangeForTableData")]
    partial class ChangeForTableData
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

                    b.Property<int>("Exam_ID")
                        .HasColumnType("int");

                    b.Property<int>("Interview_ID")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.HasKey("Assessment_ID");

                    b.HasIndex("Exam_ID");

                    b.HasIndex("Interview_ID");

                    b.ToTable("Assessment");
                });

            modelBuilder.Entity("Project_ART.Models.TableBenefit", b =>
                {
                    b.Property<int>("Benefit_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Benefit_ID"), 1L, 1);

                    b.Property<string>("Benefit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Job_Application_ID")
                        .HasColumnType("int");

                    b.HasKey("Benefit_ID");

                    b.HasIndex("Job_Application_ID");

                    b.ToTable("Benefit");
                });

            modelBuilder.Entity("Project_ART.Models.TableCandidate", b =>
                {
                    b.Property<int>("Candidate_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Candidate_ID"), 1L, 1);

                    b.Property<int?>("Assessment_ID")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Email_Confirmed")
                        .HasColumnType("bit");

                    b.Property<string>("First_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Introduction_ID")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Job_Application_ID")
                        .HasColumnType("int");

                    b.Property<string>("Last_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Middle_Initital")
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Mobile_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Resume_ID")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Candidate_ID");

                    b.HasIndex("Assessment_ID");

                    b.HasIndex("Introduction_ID");

                    b.HasIndex("Job_Application_ID");

                    b.HasIndex("Resume_ID");

                    b.ToTable("Candidate");
                });

            modelBuilder.Entity("Project_ART.Models.TableData", b =>
                {
                    b.Property<int>("Data_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Data_ID"), 1L, 1);

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("Resume_ID")
                        .HasColumnType("int");

                    b.HasKey("Data_ID");

                    b.HasIndex("Resume_ID");

                    b.ToTable("Data");
                });

            modelBuilder.Entity("Project_ART.Models.TableExam", b =>
                {
                    b.Property<int>("Exam_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Exam_ID"), 1L, 1);

                    b.Property<int?>("Exam_Score")
                        .HasColumnType("int");

                    b.Property<string>("Exam_Sheet")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.HasKey("Exam_ID");

                    b.ToTable("Exam");
                });

            modelBuilder.Entity("Project_ART.Models.TableFAQ", b =>
                {
                    b.Property<int>("Question_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Question_ID"), 1L, 1);

                    b.Property<string>("Answer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Email_Confirmed")
                        .HasColumnType("bit");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Question_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("Project_ART.Models.TableInterview", b =>
                {
                    b.Property<int>("Interview_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Interview_ID"), 1L, 1);

                    b.Property<string>("Interview")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Interview_Score")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.HasKey("Interview_ID");

                    b.ToTable("Interview");
                });

            modelBuilder.Entity("Project_ART.Models.TableIntroduction", b =>
                {
                    b.Property<int>("Introduction_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Introduction_ID"), 1L, 1);

                    b.Property<string>("DISC_Trait")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Introduction_Score")
                        .HasColumnType("int");

                    b.Property<string>("Introduction_Video")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.HasKey("Introduction_ID");

                    b.ToTable("Introduction");
                });

            modelBuilder.Entity("Project_ART.Models.TableJobApplication", b =>
                {
                    b.Property<int>("Job_Application_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Job_Application_ID"), 1L, 1);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date_End")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Date_Published")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<bool>("Is_Open")
                        .HasColumnType("bit");

                    b.Property<string>("Job")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job_Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Job_Nature")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Vacancy")
                        .HasColumnType("int");

                    b.HasKey("Job_Application_ID");

                    b.ToTable("JobApplication");
                });

            modelBuilder.Entity("Project_ART.Models.TableKeyword", b =>
                {
                    b.Property<int>("Key_Word_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Key_Word_ID"), 1L, 1);

                    b.Property<int>("Introduction_ID")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Time_Stamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Key_Word_ID");

                    b.HasIndex("Introduction_ID");

                    b.ToTable("KeyWord");
                });

            modelBuilder.Entity("Project_ART.Models.TableLog", b =>
                {
                    b.Property<int>("Log_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Log_ID"), 1L, 1);

                    b.Property<string>("Date_Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Table")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Table_ID")
                        .HasColumnType("int");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("Log_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Project_ART.Models.TableQualification", b =>
                {
                    b.Property<int>("Qualification_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Qualification_ID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Job_Application_ID")
                        .HasColumnType("int");

                    b.Property<string>("Qualification")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Qualification_ID");

                    b.HasIndex("Job_Application_ID");

                    b.ToTable("Qualification");
                });

            modelBuilder.Entity("Project_ART.Models.TableResponsibility", b =>
                {
                    b.Property<int>("Responsibility_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Responsibility_ID"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<int?>("Job_Application_ID")
                        .HasColumnType("int");

                    b.Property<string>("Responsibility")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Responsibility_ID");

                    b.HasIndex("Job_Application_ID");

                    b.ToTable("Responsibility");
                });

            modelBuilder.Entity("Project_ART.Models.TableResume", b =>
                {
                    b.Property<int>("Resume_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Resume_ID"), 1L, 1);

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Resume")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Resume_Score")
                        .HasColumnType("int");

                    b.HasKey("Resume_ID");

                    b.ToTable("Resume");
                });

            modelBuilder.Entity("Project_ART.Models.TableStatus", b =>
                {
                    b.Property<int>("Status_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Status_ID"), 1L, 1);

                    b.Property<int?>("Approved_By")
                        .HasColumnType("int");

                    b.Property<int?>("Approved_By_IDCompany_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Assessed_By")
                        .HasColumnType("int");

                    b.Property<int?>("Assessed_By_IDCompany_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Candidate_ID")
                        .HasColumnType("int");

                    b.Property<string>("Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Hired_By")
                        .HasColumnType("int");

                    b.Property<int?>("Hired_By_IDCompany_ID")
                        .HasColumnType("int");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Status_ID");

                    b.HasIndex("Approved_By_IDCompany_ID");

                    b.HasIndex("Assessed_By_IDCompany_ID");

                    b.HasIndex("Candidate_ID");

                    b.HasIndex("Hired_By_IDCompany_ID");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Project_ART.Models.TableUser", b =>
                {
                    b.Property<int>("Company_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Company_ID"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("First_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Is_Admin")
                        .HasColumnType("bit");

                    b.Property<bool?>("Is_Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Last_Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Middle_Initial")
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Mobile_Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profile_Pic")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Company_ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Project_ART.Models.TableAssessment", b =>
                {
                    b.HasOne("Project_ART.Models.TableExam", "Exam")
                        .WithMany()
                        .HasForeignKey("Exam_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_ART.Models.TableInterview", "Interview")
                        .WithMany()
                        .HasForeignKey("Interview_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Interview");
                });

            modelBuilder.Entity("Project_ART.Models.TableBenefit", b =>
                {
                    b.HasOne("Project_ART.Models.TableJobApplication", "JobApplication")
                        .WithMany()
                        .HasForeignKey("Job_Application_ID");

                    b.Navigation("JobApplication");
                });

            modelBuilder.Entity("Project_ART.Models.TableCandidate", b =>
                {
                    b.HasOne("Project_ART.Models.TableAssessment", "Assessment")
                        .WithMany()
                        .HasForeignKey("Assessment_ID");

                    b.HasOne("Project_ART.Models.TableIntroduction", "Introduction")
                        .WithMany()
                        .HasForeignKey("Introduction_ID");

                    b.HasOne("Project_ART.Models.TableJobApplication", "JobApplication")
                        .WithMany()
                        .HasForeignKey("Job_Application_ID");

                    b.HasOne("Project_ART.Models.TableResume", "Resume")
                        .WithMany()
                        .HasForeignKey("Resume_ID");

                    b.Navigation("Assessment");

                    b.Navigation("Introduction");

                    b.Navigation("JobApplication");

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("Project_ART.Models.TableData", b =>
                {
                    b.HasOne("Project_ART.Models.TableResume", "Resume")
                        .WithMany()
                        .HasForeignKey("Resume_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Resume");
                });

            modelBuilder.Entity("Project_ART.Models.TableFAQ", b =>
                {
                    b.HasOne("Project_ART.Models.TableUser", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_ART.Models.TableKeyword", b =>
                {
                    b.HasOne("Project_ART.Models.TableIntroduction", "Introduction")
                        .WithMany()
                        .HasForeignKey("Introduction_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Introduction");
                });

            modelBuilder.Entity("Project_ART.Models.TableLog", b =>
                {
                    b.HasOne("Project_ART.Models.TableUser", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_ART.Models.TableQualification", b =>
                {
                    b.HasOne("Project_ART.Models.TableJobApplication", "JobApplication")
                        .WithMany()
                        .HasForeignKey("Job_Application_ID");

                    b.Navigation("JobApplication");
                });

            modelBuilder.Entity("Project_ART.Models.TableResponsibility", b =>
                {
                    b.HasOne("Project_ART.Models.TableJobApplication", "JobApplication")
                        .WithMany()
                        .HasForeignKey("Job_Application_ID");

                    b.Navigation("JobApplication");
                });

            modelBuilder.Entity("Project_ART.Models.TableStatus", b =>
                {
                    b.HasOne("Project_ART.Models.TableUser", "Approved_By_ID")
                        .WithMany()
                        .HasForeignKey("Approved_By_IDCompany_ID");

                    b.HasOne("Project_ART.Models.TableUser", "Assessed_By_ID")
                        .WithMany()
                        .HasForeignKey("Assessed_By_IDCompany_ID");

                    b.HasOne("Project_ART.Models.TableCandidate", "Candidate")
                        .WithMany()
                        .HasForeignKey("Candidate_ID");

                    b.HasOne("Project_ART.Models.TableUser", "Hired_By_ID")
                        .WithMany()
                        .HasForeignKey("Hired_By_IDCompany_ID");

                    b.Navigation("Approved_By_ID");

                    b.Navigation("Assessed_By_ID");

                    b.Navigation("Candidate");

                    b.Navigation("Hired_By_ID");
                });
#pragma warning restore 612, 618
        }
    }
}
