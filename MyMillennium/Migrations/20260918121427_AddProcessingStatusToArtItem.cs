using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMillenniumApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProcessingStatusToArtItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcessingStatus",
                table: "ArtItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcessingStatus",
                table: "ArtItems");
        }
    }
}
