using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    /// <inheritdoc />
    public partial class AddedImageFileImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Img",
                table: "Images",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageFile",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Images",
                newName: "Img");
        }
    }
}
