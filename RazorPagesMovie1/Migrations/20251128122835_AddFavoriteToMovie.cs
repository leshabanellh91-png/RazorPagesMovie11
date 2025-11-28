using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovie1.Migrations
{
    /// <inheritdoc />
    public partial class AddFavoriteToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Isfavorite",
                table: "Movie",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Isfavorite",
                table: "Movie");
        }
    }
}
