using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepareToInterview.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                     table: "Questions",  // Replace with your actual table name
                     keyColumn: "Difficulty",  // Replace with your primary key column name
                     keyValue: null,  // This will apply to all rows, as we check for NULL or empty string later
                     column: "Difficulty",
                     value: "Easy"
                 );
            migrationBuilder.UpdateData(
                      table: "Questions",  // Replace with your actual table name
                      keyColumn: "Difficulty",  // Replace with your primary key column name
                      keyValue: "",  // This will apply to all rows, as we check for NULL or empty string later
                      column: "Difficulty",
                      value: "Easy"
                  );

            migrationBuilder.AddCheckConstraint(
               table: "Questions", // Replace with your actual table name
               name: "CK_Question_Difficulty", // Name of the constraint
               sql: "\"Difficulty\" IN ('Easy', 'Medium', 'Hard')" // Column name wrapped in double quotes
           );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the CHECK constraint in case of rollback
            migrationBuilder.DropCheckConstraint(
                table: "Questions", // Replace with your actual table name
                name: "CK_Question_Difficulty" // Name of the constraint
            );
        }
    }
}
