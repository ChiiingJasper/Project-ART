using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class properly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Personality",
                table: "PersonalityPrediction");

            migrationBuilder.AddColumn<int>(
                name: "Compliance",
                table: "PersonalityPrediction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Dominance",
                table: "PersonalityPrediction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Influence",
                table: "PersonalityPrediction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Steadiness",
                table: "PersonalityPrediction",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Compliance",
                table: "PersonalityPrediction");

            migrationBuilder.DropColumn(
                name: "Dominance",
                table: "PersonalityPrediction");

            migrationBuilder.DropColumn(
                name: "Influence",
                table: "PersonalityPrediction");

            migrationBuilder.DropColumn(
                name: "Steadiness",
                table: "PersonalityPrediction");

            migrationBuilder.AddColumn<string>(
                name: "Personality",
                table: "PersonalityPrediction",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
