using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_database_app.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Movie");
        }
    }
}
