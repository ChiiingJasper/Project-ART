using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_ART.Migrations
{
    public partial class asdad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "Steadiness",
                table: "PersonalityPrediction",
                newName: "DISC_Personality");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DISC_Personality",
                table: "PersonalityPrediction",
                newName: "Steadiness");

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
        }
    }
}
