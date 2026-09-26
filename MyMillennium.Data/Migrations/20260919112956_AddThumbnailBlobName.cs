using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMillennium.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailBlobName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ThumbnailBlobName",
                table: "ArtItems",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThumbnailBlobName",
                table: "ArtItems");
        }
    }
}
