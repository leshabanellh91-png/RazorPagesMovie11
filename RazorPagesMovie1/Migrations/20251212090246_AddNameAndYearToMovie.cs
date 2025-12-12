using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RazorPagesMovie1.Migrations.RazorPagesMovie1
{
    /// <inheritdoc />
    public partial class AddNameAndYearToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Actors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Year",
                table: "Movie");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Actors");
        }
    }
}
