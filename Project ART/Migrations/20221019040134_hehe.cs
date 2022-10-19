using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class hehe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Candidates",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MI",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Candidates",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MI",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Candidates");
        }
    }
}
