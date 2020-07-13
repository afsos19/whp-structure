using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class changed_field_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CODIGO_ITEM",
                table: "PEDIDO_ITEM",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CODIGO_ITEM",
                table: "PEDIDO_ITEM",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
