using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class addingFKtoTableKeyword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Introduction_ID",
                table: "KeyWords",
                newName: "IntroductionID");

            migrationBuilder.CreateIndex(
                name: "IX_KeyWords_IntroductionID",
                table: "KeyWords",
                column: "IntroductionID");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyWords_Introductions_IntroductionID",
                table: "KeyWords",
                column: "IntroductionID",
                principalTable: "Introductions",
                principalColumn: "Introduction_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyWords_Introductions_IntroductionID",
                table: "KeyWords");

            migrationBuilder.DropIndex(
                name: "IX_KeyWords_IntroductionID",
                table: "KeyWords");

            migrationBuilder.RenameColumn(
                name: "IntroductionID",
                table: "KeyWords",
                newName: "Introduction_ID");
        }
    }
}
