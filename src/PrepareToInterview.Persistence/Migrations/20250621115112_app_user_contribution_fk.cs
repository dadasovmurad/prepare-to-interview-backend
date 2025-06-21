using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepareToInterview.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class app_user_contribution_fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_question_category_category_id",
                table: "question");

            migrationBuilder.DropIndex(
                name: "ix_app_user_pass_key_hash",
                table: "app_user");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "question",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "ix_question_user_id",
                table: "question",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_app_user_email",
                table: "app_user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_app_user_username",
                table: "app_user",
                column: "username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_question_app_user_user_id",
                table: "question",
                column: "user_id",
                principalTable: "app_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_question_category_category_id",
                table: "question",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_question_app_user_user_id",
                table: "question");

            migrationBuilder.DropForeignKey(
                name: "fk_question_category_category_id",
                table: "question");

            migrationBuilder.DropIndex(
                name: "ix_question_user_id",
                table: "question");

            migrationBuilder.DropIndex(
                name: "ix_app_user_email",
                table: "app_user");

            migrationBuilder.DropIndex(
                name: "ix_app_user_username",
                table: "app_user");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "question");

            migrationBuilder.CreateIndex(
                name: "ix_app_user_pass_key_hash",
                table: "app_user",
                column: "pass_key_hash",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_question_category_category_id",
                table: "question",
                column: "category_id",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
