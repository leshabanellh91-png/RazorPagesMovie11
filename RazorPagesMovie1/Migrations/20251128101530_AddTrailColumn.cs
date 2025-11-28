using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovie1.Migrations
{
    /// <inheritdoc />
    public partial class AddTrailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrailerUrl",
                table: "Movie",
                newName: "Trail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Trail",
                table: "Movie",
                newName: "TrailerUrl");
        }
    }
}
