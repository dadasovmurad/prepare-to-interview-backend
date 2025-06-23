using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepareToInterview.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class questionshorturl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "question");

            migrationBuilder.AddColumn<string>(
                name: "short_url",
                table: "question",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "short_url",
                table: "question");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "question",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
