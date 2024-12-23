using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class migtekraronleme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_UserId",
                table: "SurveyResponses");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_UserId_DateTimeOptionId",
                table: "SurveyResponses",
                columns: new[] { "UserId", "DateTimeOptionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SurveyResponses_UserId_DateTimeOptionId",
                table: "SurveyResponses");

            migrationBuilder.CreateIndex(
                name: "IX_SurveyResponses_UserId",
                table: "SurveyResponses",
                column: "UserId");
        }
    }
}
