using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_database_app.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Rate_NotMapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Movie");

            migrationBuilder.AddColumn<float>(
                name: "TotalScore",
                table: "Movie",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "VoteCount",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "VoteCount",
                table: "Movie");

            migrationBuilder.AddColumn<float>(
                name: "Rate",
                table: "Movie",
                type: "real",
                nullable: true);
        }
    }
}
