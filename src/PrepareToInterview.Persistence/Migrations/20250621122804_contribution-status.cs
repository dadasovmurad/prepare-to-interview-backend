using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepareToInterview.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class contributionstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "contribution",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "question");

            migrationBuilder.DropColumn(
                name: "status",
                table: "contribution");
        }
    }
}
