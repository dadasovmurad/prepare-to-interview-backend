using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PrepareToInterview.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class added_category_icon_details : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "icon_url",
                table: "category",
                newName: "iconurl");

            migrationBuilder.AddColumn<string>(
                name: "icon_name",
                table: "category",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "icon_name",
                table: "category");

            migrationBuilder.RenameColumn(
                name: "iconurl",
                table: "category",
                newName: "icon_url");
        }
    }
}
