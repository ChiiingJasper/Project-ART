using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class ChangeForTableData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_TableResume_Resume_ID",
                table: "Candidate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TableResume",
                table: "TableResume");

            migrationBuilder.RenameTable(
                name: "TableResume",
                newName: "Resume");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resume",
                table: "Resume",
                column: "Resume_ID");

            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Data_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resume_ID = table.Column<int>(type: "int", nullable: false),
                    Is_Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Data_ID);
                    table.ForeignKey(
                        name: "FK_Data_Resume_Resume_ID",
                        column: x => x.Resume_ID,
                        principalTable: "Resume",
                        principalColumn: "Resume_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Data_Resume_ID",
                table: "Data",
                column: "Resume_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_Resume_Resume_ID",
                table: "Candidate",
                column: "Resume_ID",
                principalTable: "Resume",
                principalColumn: "Resume_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidate_Resume_Resume_ID",
                table: "Candidate");

            migrationBuilder.DropTable(
                name: "Data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resume",
                table: "Resume");

            migrationBuilder.RenameTable(
                name: "Resume",
                newName: "TableResume");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableResume",
                table: "TableResume",
                column: "Resume_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidate_TableResume_Resume_ID",
                table: "Candidate",
                column: "Resume_ID",
                principalTable: "TableResume",
                principalColumn: "Resume_ID");
        }
    }
}
