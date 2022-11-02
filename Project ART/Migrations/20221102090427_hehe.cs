using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class hehe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefit_JobApplication_JobApplicationJob_Application_ID",
                table: "Benefit");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_JobApplication_JobApplicationJob_Application_ID",
                table: "Qualification");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibility_JobApplication_JobApplicationJob_Application_ID",
                table: "Responsibility");

            migrationBuilder.DropIndex(
                name: "IX_Responsibility_JobApplicationJob_Application_ID",
                table: "Responsibility");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_JobApplicationJob_Application_ID",
                table: "Qualification");

            migrationBuilder.DropIndex(
                name: "IX_Benefit_JobApplicationJob_Application_ID",
                table: "Benefit");

            migrationBuilder.DropColumn(
                name: "JobApplicationJob_Application_ID",
                table: "Responsibility");

            migrationBuilder.DropColumn(
                name: "JobApplicationJob_Application_ID",
                table: "Qualification");

            migrationBuilder.DropColumn(
                name: "JobApplicationJob_Application_ID",
                table: "Benefit");

            migrationBuilder.AlterColumn<int>(
                name: "Job_Application_ID",
                table: "Responsibility",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Job_Application_ID",
                table: "Qualification",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Open",
                table: "JobApplication",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Deleted",
                table: "JobApplication",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Job_Application_ID",
                table: "Benefit",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_Job_Application_ID",
                table: "Responsibility",
                column: "Job_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_Job_Application_ID",
                table: "Qualification",
                column: "Job_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Benefit_Job_Application_ID",
                table: "Benefit",
                column: "Job_Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefit_JobApplication_Job_Application_ID",
                table: "Benefit",
                column: "Job_Application_ID",
                principalTable: "JobApplication",
                principalColumn: "Job_Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_JobApplication_Job_Application_ID",
                table: "Qualification",
                column: "Job_Application_ID",
                principalTable: "JobApplication",
                principalColumn: "Job_Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibility_JobApplication_Job_Application_ID",
                table: "Responsibility",
                column: "Job_Application_ID",
                principalTable: "JobApplication",
                principalColumn: "Job_Application_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Benefit_JobApplication_Job_Application_ID",
                table: "Benefit");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualification_JobApplication_Job_Application_ID",
                table: "Qualification");

            migrationBuilder.DropForeignKey(
                name: "FK_Responsibility_JobApplication_Job_Application_ID",
                table: "Responsibility");

            migrationBuilder.DropIndex(
                name: "IX_Responsibility_Job_Application_ID",
                table: "Responsibility");

            migrationBuilder.DropIndex(
                name: "IX_Qualification_Job_Application_ID",
                table: "Qualification");

            migrationBuilder.DropIndex(
                name: "IX_Benefit_Job_Application_ID",
                table: "Benefit");

            migrationBuilder.AlterColumn<int>(
                name: "Job_Application_ID",
                table: "Responsibility",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobApplicationJob_Application_ID",
                table: "Responsibility",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Job_Application_ID",
                table: "Qualification",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobApplicationJob_Application_ID",
                table: "Qualification",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Open",
                table: "JobApplication",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Is_Deleted",
                table: "JobApplication",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "Job_Application_ID",
                table: "Benefit",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobApplicationJob_Application_ID",
                table: "Benefit",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Responsibility_JobApplicationJob_Application_ID",
                table: "Responsibility",
                column: "JobApplicationJob_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_JobApplicationJob_Application_ID",
                table: "Qualification",
                column: "JobApplicationJob_Application_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Benefit_JobApplicationJob_Application_ID",
                table: "Benefit",
                column: "JobApplicationJob_Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Benefit_JobApplication_JobApplicationJob_Application_ID",
                table: "Benefit",
                column: "JobApplicationJob_Application_ID",
                principalTable: "JobApplication",
                principalColumn: "Job_Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualification_JobApplication_JobApplicationJob_Application_ID",
                table: "Qualification",
                column: "JobApplicationJob_Application_ID",
                principalTable: "JobApplication",
                principalColumn: "Job_Application_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Responsibility_JobApplication_JobApplicationJob_Application_ID",
                table: "Responsibility",
                column: "JobApplicationJob_Application_ID",
                principalTable: "JobApplication",
                principalColumn: "Job_Application_ID");
        }
    }
}
