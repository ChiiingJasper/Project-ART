using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class hehe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exams",
                columns: table => new
                {
                    Exam_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exams", x => x.Exam_ID);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Interview_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interview_Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Interview_ID);
                });

            migrationBuilder.CreateTable(
                name: "Introductions",
                columns: table => new
                {
                    Introduction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Introduction_Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DISC_Trait = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Introductions", x => x.Introduction_ID);
                });

            migrationBuilder.CreateTable(
                name: "Resume",
                columns: table => new
                {
                    Resume_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resume", x => x.Resume_ID);
                });

            migrationBuilder.CreateTable(
                name: "TableDatasheet",
                columns: table => new
                {
                    Data_Sheet_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Initial = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkedIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableDatasheet", x => x.Data_Sheet_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Company_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Initial = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Confirm_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Admin = table.Column<bool>(type: "bit", nullable: false),
                    Profile_Pic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Company_ID);
                });

            migrationBuilder.CreateTable(
                name: "KeyWords",
                columns: table => new
                {
                    Key_Word_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_Stamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntroductionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWords", x => x.Key_Word_ID);
                    table.ForeignKey(
                        name: "FK_KeyWords_Introductions_IntroductionID",
                        column: x => x.IntroductionID,
                        principalTable: "Introductions",
                        principalColumn: "Introduction_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Skill_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Skill_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DatasheetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Skill_ID);
                    table.ForeignKey(
                        name: "FK_Skills_TableDatasheet_DatasheetID",
                        column: x => x.DatasheetID,
                        principalTable: "TableDatasheet",
                        principalColumn: "Data_Sheet_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Assessment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_Assessed = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamID = table.Column<int>(type: "int", nullable: false),
                    InterviewID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Assessment_ID);
                    table.ForeignKey(
                        name: "FK_Assessments_Exams_ExamID",
                        column: x => x.ExamID,
                        principalTable: "Exams",
                        principalColumn: "Exam_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessments_Interviews_InterviewID",
                        column: x => x.InterviewID,
                        principalTable: "Interviews",
                        principalColumn: "Interview_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessments_Users_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Users",
                        principalColumn: "Company_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Application_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Is_Approved = table.Column<bool>(type: "bit", nullable: true),
                    Date_Received = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IntroductionID = table.Column<int>(type: "int", nullable: false),
                    DatasheetID = table.Column<int>(type: "int", nullable: false),
                    CompanyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Application_ID);
                    table.ForeignKey(
                        name: "FK_JobApplications_Introductions_IntroductionID",
                        column: x => x.IntroductionID,
                        principalTable: "Introductions",
                        principalColumn: "Introduction_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplications_TableDatasheet_DatasheetID",
                        column: x => x.DatasheetID,
                        principalTable: "TableDatasheet",
                        principalColumn: "Data_Sheet_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplications_Users_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Users",
                        principalColumn: "Company_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Candidate_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Initital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    Resume_ID = table.Column<int>(type: "int", nullable: true),
                    Introduction_ID = table.Column<int>(type: "int", nullable: true),
                    Assessment_ID = table.Column<int>(type: "int", nullable: true),
                    Job_Application_ID = table.Column<int>(type: "int", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Candidate_ID);
                    table.ForeignKey(
                        name: "FK_Candidates_Assessments_Assessment_ID",
                        column: x => x.Assessment_ID,
                        principalTable: "Assessments",
                        principalColumn: "Assessment_ID");
                    table.ForeignKey(
                        name: "FK_Candidates_Introductions_Introduction_ID",
                        column: x => x.Introduction_ID,
                        principalTable: "Introductions",
                        principalColumn: "Introduction_ID");
                    table.ForeignKey(
                        name: "FK_Candidates_JobApplications_Job_Application_ID",
                        column: x => x.Job_Application_ID,
                        principalTable: "JobApplications",
                        principalColumn: "Application_ID");
                    table.ForeignKey(
                        name: "FK_Candidates_Resume_Resume_ID",
                        column: x => x.Resume_ID,
                        principalTable: "Resume",
                        principalColumn: "Resume_ID");
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Candidate_ID = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Status_ID);
                    table.ForeignKey(
                        name: "FK_Status_Candidates_Candidate_ID",
                        column: x => x.Candidate_ID,
                        principalTable: "Candidates",
                        principalColumn: "Candidate_ID");
                    table.ForeignKey(
                        name: "FK_Status_Users_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Users",
                        principalColumn: "Company_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CompanyID",
                table: "Assessments",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_ExamID",
                table: "Assessments",
                column: "ExamID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_InterviewID",
                table: "Assessments",
                column: "InterviewID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Assessment_ID",
                table: "Candidates",
                column: "Assessment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Introduction_ID",
                table: "Candidates",
                column: "Introduction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Job_Application_ID",
                table: "Candidates",
                column: "Job_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_Resume_ID",
                table: "Candidates",
                column: "Resume_ID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_CompanyID",
                table: "JobApplications",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_DatasheetID",
                table: "JobApplications",
                column: "DatasheetID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_IntroductionID",
                table: "JobApplications",
                column: "IntroductionID");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_IntroductionID",
                table: "KeyWords",
                column: "IntroductionID");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_DatasheetID",
                table: "Skills",
                column: "DatasheetID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Candidate_ID",
                table: "Status",
                column: "Candidate_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_CompanyID",
                table: "Status",
                column: "CompanyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeyWords");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "Resume");

            migrationBuilder.DropTable(
                name: "Exams");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "Introductions");

            migrationBuilder.DropTable(
                name: "TableDatasheet");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
