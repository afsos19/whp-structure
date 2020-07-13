using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class added_thumb_photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PHOTO",
                table: "NOVIDADES",
                newName: "FOTO");

            migrationBuilder.AddColumn<string>(
                name: "FOTO_THUMB",
                table: "NOVIDADES",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FOTO_THUMB",
                table: "NOVIDADES");

            migrationBuilder.RenameColumn(
                name: "FOTO",
                table: "NOVIDADES",
                newName: "PHOTO");
        }
    }
}
