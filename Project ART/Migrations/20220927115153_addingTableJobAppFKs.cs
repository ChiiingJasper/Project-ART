using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class addingTableJobAppFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSheet_ID",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "Introduction_ID",
                table: "JobApplication");

            migrationBuilder.RenameColumn(
                name: "Approved_By",
                table: "JobApplication",
                newName: "IntroductionID");

            migrationBuilder.AddColumn<int>(
                name: "CompanyID",
                table: "JobApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DatasheetID",
                table: "JobApplication",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_CompanyID",
                table: "JobApplication",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_DatasheetID",
                table: "JobApplication",
                column: "DatasheetID");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplication_IntroductionID",
                table: "JobApplication",
                column: "IntroductionID");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Datasheets_DatasheetID",
                table: "JobApplication",
                column: "DatasheetID",
                principalTable: "Datasheets",
                principalColumn: "Data_Sheet_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Introductions_IntroductionID",
                table: "JobApplication",
                column: "IntroductionID",
                principalTable: "Introductions",
                principalColumn: "Introduction_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplication_Users_CompanyID",
                table: "JobApplication",
                column: "CompanyID",
                principalTable: "Users",
                principalColumn: "Company_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Datasheets_DatasheetID",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Introductions_IntroductionID",
                table: "JobApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplication_Users_CompanyID",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_CompanyID",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_DatasheetID",
                table: "JobApplication");

            migrationBuilder.DropIndex(
                name: "IX_JobApplication_IntroductionID",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "CompanyID",
                table: "JobApplication");

            migrationBuilder.DropColumn(
                name: "DatasheetID",
                table: "JobApplication");

            migrationBuilder.RenameColumn(
                name: "IntroductionID",
                table: "JobApplication",
                newName: "Approved_By");

            migrationBuilder.AddColumn<string>(
                name: "DataSheet_ID",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Introduction_ID",
                table: "JobApplication",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
