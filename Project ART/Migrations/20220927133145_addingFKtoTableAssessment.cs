using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class addingFKtoTableAssessment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assessed_By",
                table: "Assessments");

            migrationBuilder.RenameColumn(
                name: "Interview_ID",
                table: "Assessments",
                newName: "InterviewID");

            migrationBuilder.RenameColumn(
                name: "Exam_ID",
                table: "Assessments",
                newName: "ExamID");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Assessments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Exams_ExamID",
                table: "Assessments",
                column: "ExamID",
                principalTable: "Exams",
                principalColumn: "Exam_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Interviews_InterviewID",
                table: "Assessments",
                column: "InterviewID",
                principalTable: "Interviews",
                principalColumn: "Interview_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assessments_Users_CompanyID",
                table: "Assessments",
                column: "CompanyID",
                principalTable: "Users",
                principalColumn: "Company_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Exams_ExamID",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Interviews_InterviewID",
                table: "Assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_Assessments_Users_CompanyID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_CompanyID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_ExamID",
                table: "Assessments");

            migrationBuilder.DropIndex(
                name: "IX_Assessments_InterviewID",
                table: "Assessments");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Assessments");

            migrationBuilder.RenameColumn(
                name: "InterviewID",
                table: "Assessments",
                newName: "Interview_ID");

            migrationBuilder.RenameColumn(
                name: "ExamID",
                table: "Assessments",
                newName: "Exam_ID");

            migrationBuilder.AddColumn<int>(
                name: "Assessed_By",
                table: "Assessments",
                type: "int",
                nullable: true);
        }
    }
}
