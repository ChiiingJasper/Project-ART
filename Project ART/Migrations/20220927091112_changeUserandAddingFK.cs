using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class changeUserandAddingFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.RenameColumn(
                name: "Skill",
                table: "Skills",
                newName: "Skill_Name");

            migrationBuilder.RenameColumn(
                name: "Data_Sheet_ID",
                table: "Skills",
                newName: "DatasheetID");

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
                    Is_Admin = table.Column<bool>(type: "bit", nullable: false),
                    Profile_Pic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Company_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_DatasheetID",
                table: "Skills",
                column: "DatasheetID");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Datasheets_DatasheetID",
                table: "Skills",
                column: "DatasheetID",
                principalTable: "Datasheets",
                principalColumn: "Data_Sheet_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Datasheets_DatasheetID",
                table: "Skills");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Skills_DatasheetID",
                table: "Skills");

            migrationBuilder.RenameColumn(
                name: "Skill_Name",
                table: "Skills",
                newName: "Skill");

            migrationBuilder.RenameColumn(
                name: "DatasheetID",
                table: "Skills",
                newName: "Data_Sheet_ID");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Company_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Middle_Initial = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    Mobile_Number = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Company_ID);
                });
        }
    }
}
