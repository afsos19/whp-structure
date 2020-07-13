using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class new_field_user_status_log : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG");

            migrationBuilder.RenameColumn(
                name: "ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                newName: "PARA_ID_USUARIO_STATUS");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIO_STATUS_LOG_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                newName: "IX_USUARIO_STATUS_LOG_PARA_ID_USUARIO_STATUS");

            migrationBuilder.AddColumn<string>(
                name: "DESCRICAO",
                table: "USUARIO_STATUS_LOG",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DE_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_STATUS_LOG_DE_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                column: "DE_ID_USUARIO_STATUS");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_DE_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                column: "DE_ID_USUARIO_STATUS",
                principalTable: "USUARIO_STATUS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_PARA_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                column: "PARA_ID_USUARIO_STATUS",
                principalTable: "USUARIO_STATUS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_DE_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG");

            migrationBuilder.DropForeignKey(
                name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_PARA_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG");

            migrationBuilder.DropIndex(
                name: "IX_USUARIO_STATUS_LOG_DE_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG");

            migrationBuilder.DropColumn(
                name: "DESCRICAO",
                table: "USUARIO_STATUS_LOG");

            migrationBuilder.DropColumn(
                name: "DE_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG");

            migrationBuilder.RenameColumn(
                name: "PARA_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                newName: "ID_USUARIO_STATUS");

            migrationBuilder.RenameIndex(
                name: "IX_USUARIO_STATUS_LOG_PARA_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                newName: "IX_USUARIO_STATUS_LOG_ID_USUARIO_STATUS");

            migrationBuilder.AddForeignKey(
                name: "FK_USUARIO_STATUS_LOG_USUARIO_STATUS_ID_USUARIO_STATUS",
                table: "USUARIO_STATUS_LOG",
                column: "ID_USUARIO_STATUS",
                principalTable: "USUARIO_STATUS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
