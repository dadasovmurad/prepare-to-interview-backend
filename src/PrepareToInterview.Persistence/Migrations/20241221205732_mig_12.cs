using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepareToInterview.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SuitableFor",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "Difficulty",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Questions");

            migrationBuilder.AddColumn<string>(
                name: "SuitableFor",
                table: "Questions",
                type: "text",
                nullable: true);
        }
    }
}
