using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class haha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Website",
                table: "Candidates");
        }
    }
}
