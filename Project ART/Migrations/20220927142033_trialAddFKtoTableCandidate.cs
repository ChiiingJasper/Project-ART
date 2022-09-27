using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class trialAddFKtoTableCandidate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Application_ID",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Assessment_ID",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Hired_By",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationID",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssessmentID",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ApplicationID",
                table: "Candidates",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_AssessmentID",
                table: "Candidates",
                column: "AssessmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CompanyID",
                table: "Candidates",
                column: "CompanyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Assessments_AssessmentID",
                table: "Candidates",
                column: "AssessmentID",
                principalTable: "Assessments",
                principalColumn: "Assessment_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_JobApplication_ApplicationID",
                table: "Candidates",
                column: "ApplicationID",
                principalTable: "JobApplication",
                principalColumn: "Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_Users_CompanyID",
                table: "Candidates",
                column: "CompanyID",
                principalTable: "Users",
                principalColumn: "Company_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Assessments_AssessmentID",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_JobApplication_ApplicationID",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_Users_CompanyID",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_ApplicationID",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_AssessmentID",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CompanyID",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "ApplicationID",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "AssessmentID",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "Candidates");

            migrationBuilder.AddColumn<int>(
                name: "Application_ID",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Assessment_ID",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hired_By",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
