using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class addmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exam",
                columns: table => new
                {
                    Exam_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_Score = table.Column<int>(type: "int", nullable: true),
                    Exam_Sheet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Exam_ID);
                });

            migrationBuilder.CreateTable(
                name: "Interview",
                columns: table => new
                {
                    Interview_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Interview_Score = table.Column<int>(type: "int", nullable: true),
                    Interview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interview", x => x.Interview_ID);
                });

            migrationBuilder.CreateTable(
                name: "Introduction",
                columns: table => new
                {
                    Introduction_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DISC_Trait = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Introduction_Video = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Introduction_Score = table.Column<int>(type: "int", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Introduction", x => x.Introduction_ID);
                });

            migrationBuilder.CreateTable(
                name: "JobApplication",
                columns: table => new
                {
                    Job_Application_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Published = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_End = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vacancy = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Job_Nature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Open = table.Column<bool>(type: "bit", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplication", x => x.Job_Application_ID);
                });

            migrationBuilder.CreateTable(
                name: "TableResume",
                columns: table => new
                {
                    Resume_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Resume = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resume_Score = table.Column<int>(type: "int", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableResume", x => x.Resume_ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Company_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Initial = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Admin = table.Column<bool>(type: "bit", nullable: false),
                    Profile_Pic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Company_ID);
                });

            migrationBuilder.CreateTable(
                name: "Assessment",
                columns: table => new
                {
                    Assessment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Exam_ID = table.Column<int>(type: "int", nullable: false),
                    Interview_ID = table.Column<int>(type: "int", nullable: false),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessment", x => x.Assessment_ID);
                    table.ForeignKey(
                        name: "FK_Assessment_Exam_Exam_ID",
                        column: x => x.Exam_ID,
                        principalTable: "Exam",
                        principalColumn: "Exam_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assessment_Interview_Interview_ID",
                        column: x => x.Interview_ID,
                        principalTable: "Interview",
                        principalColumn: "Interview_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyWord",
                columns: table => new
                {
                    Key_Word_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Introduction_ID = table.Column<int>(type: "int", nullable: false),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time_Stamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyWord", x => x.Key_Word_ID);
                    table.ForeignKey(
                        name: "FK_KeyWord_Introduction_Introduction_ID",
                        column: x => x.Introduction_ID,
                        principalTable: "Introduction",
                        principalColumn: "Introduction_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Benefit",
                columns: table => new
                {
                    Benefit_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Job_Application_ID = table.Column<int>(type: "int", nullable: false),
                    JobApplicationJob_Application_ID = table.Column<int>(type: "int", nullable: true),
                    Benefit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benefit", x => x.Benefit_ID);
                    table.ForeignKey(
                        name: "FK_Benefit_JobApplication_JobApplicationJob_Application_ID",
                        column: x => x.JobApplicationJob_Application_ID,
                        principalTable: "JobApplication",
                        principalColumn: "Job_Application_ID");
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                columns: table => new
                {
                    Qualification_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Job_Application_ID = table.Column<int>(type: "int", nullable: false),
                    JobApplicationJob_Application_ID = table.Column<int>(type: "int", nullable: true),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.Qualification_ID);
                    table.ForeignKey(
                        name: "FK_Qualification_JobApplication_JobApplicationJob_Application_ID",
                        column: x => x.JobApplicationJob_Application_ID,
                        principalTable: "JobApplication",
                        principalColumn: "Job_Application_ID");
                });

            migrationBuilder.CreateTable(
                name: "Responsibility",
                columns: table => new
                {
                    Responsibility_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Job_Application_ID = table.Column<int>(type: "int", nullable: false),
                    JobApplicationJob_Application_ID = table.Column<int>(type: "int", nullable: true),
                    Responsibility = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsibility", x => x.Responsibility_ID);
                    table.ForeignKey(
                        name: "FK_Responsibility_JobApplication_JobApplicationJob_Application_ID",
                        column: x => x.JobApplicationJob_Application_ID,
                        principalTable: "JobApplication",
                        principalColumn: "Job_Application_ID");
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Question_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Confirmed = table.Column<bool>(type: "bit", nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Question_ID);
                    table.ForeignKey(
                        name: "FK_FAQ_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "Company_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Log_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Table_ID = table.Column<int>(type: "int", nullable: true),
                    Table = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Log_ID);
                    table.ForeignKey(
                        name: "FK_Log_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "Company_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidate",
                columns: table => new
                {
                    Candidate_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Initital = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email_Confirmed = table.Column<bool>(type: "bit", nullable: true),
                    Resume_ID = table.Column<int>(type: "int", nullable: true),
                    Introduction_ID = table.Column<int>(type: "int", nullable: true),
                    Assessment_ID = table.Column<int>(type: "int", nullable: true),
                    Job_Application_ID = table.Column<int>(type: "int", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidate", x => x.Candidate_ID);
                    table.ForeignKey(
                        name: "FK_Candidate_Assessment_Assessment_ID",
                        column: x => x.Assessment_ID,
                        principalTable: "Assessment",
                        principalColumn: "Assessment_ID");
                    table.ForeignKey(
                        name: "FK_Candidate_Introduction_Introduction_ID",
                        column: x => x.Introduction_ID,
                        principalTable: "Introduction",
                        principalColumn: "Introduction_ID");
                    table.ForeignKey(
                        name: "FK_Candidate_JobApplication_Job_Application_ID",
                        column: x => x.Job_Application_ID,
                        principalTable: "JobApplication",
                        principalColumn: "Job_Application_ID");
                    table.ForeignKey(
                        name: "FK_Candidate_TableResume_Resume_ID",
                        column: x => x.Resume_ID,
                        principalTable: "TableResume",
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
                    Approved_By = table.Column<int>(type: "int", nullable: true),
                    Approved_By_IDCompany_ID = table.Column<int>(type: "int", nullable: true),
                    Assessed_By = table.Column<int>(type: "int", nullable: true),
                    Assessed_By_IDCompany_ID = table.Column<int>(type: "int", nullable: true),
                    Hired_By = table.Column<int>(type: "int", nullable: true),
                    Hired_By_IDCompany_ID = table.Column<int>(type: "int", nullable: true),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Status_ID);
                    table.ForeignKey(
                        name: "FK_Status_Candidate_Candidate_ID",
                        column: x => x.Candidate_ID,
                        principalTable: "Candidate",
                        principalColumn: "Candidate_ID");
                    table.ForeignKey(
                        name: "FK_Status_User_Approved_By_IDCompany_ID",
                        column: x => x.Approved_By_IDCompany_ID,
                        principalTable: "User",
                        principalColumn: "Company_ID");
                    table.ForeignKey(
                        name: "FK_Status_User_Assessed_By_IDCompany_ID",
                        column: x => x.Assessed_By_IDCompany_ID,
                        principalTable: "User",
                        principalColumn: "Company_ID");
                    table.ForeignKey(
                        name: "FK_Status_User_Hired_By_IDCompany_ID",
                        column: x => x.Hired_By_IDCompany_ID,
                        principalTable: "User",
                        principalColumn: "Company_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_Exam_ID",
                table: "Assessment",
                column: "Exam_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Assessment_Interview_ID",
                table: "Assessment",
                column: "Interview_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Benefit_JobApplicationJob_Application_ID",
                table: "Benefit",
                column: "JobApplicationJob_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Assessment_ID",
                table: "Candidate",
                column: "Assessment_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Introduction_ID",
                table: "Candidate",
                column: "Introduction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Job_Application_ID",
                table: "Candidate",
                column: "Job_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidate_Resume_ID",
                table: "Candidate",
                column: "Resume_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_User_ID",
                table: "FAQ",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWord_Introduction_ID",
                table: "KeyWord",
                column: "Introduction_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Log_User_ID",
                table: "Log",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_JobApplicationJob_Application_ID",
                table: "Qualification",
                column: "JobApplicationJob_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_JobApplicationJob_Application_ID",
                table: "Responsibility",
                column: "JobApplicationJob_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Approved_By_IDCompany_ID",
                table: "Status",
                column: "Approved_By_IDCompany_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Assessed_By_IDCompany_ID",
                table: "Status",
                column: "Assessed_By_IDCompany_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Candidate_ID",
                table: "Status",
                column: "Candidate_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_Hired_By_IDCompany_ID",
                table: "Status",
                column: "Hired_By_IDCompany_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Benefit");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "KeyWord");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Qualification");

            migrationBuilder.DropTable(
                name: "Responsibility");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Candidate");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Assessment");

            migrationBuilder.DropTable(
                name: "Introduction");

            migrationBuilder.DropTable(
                name: "JobApplication");

            migrationBuilder.DropTable(
                name: "TableResume");

            migrationBuilder.DropTable(
                name: "Exam");

            migrationBuilder.DropTable(
                name: "Interview");
        }
    }
}
