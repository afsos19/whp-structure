using Microsoft.EntityFrameworkCore.Migrations;

namespace Whp.MaisTop.Data.Migrations
{
    public partial class alter_name_table_quiz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QUIZ_LOJAS_RELACIONADA_QUIZ_RELACIONADO_ID_QUIZ_RELACIONADO",
                table: "QUIZ_LOJAS_RELACIONADA");

            migrationBuilder.DropForeignKey(
                name: "FK_QUIZ_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                table: "QUIZ_LOJAS_RELACIONADA");

            migrationBuilder.DropForeignKey(
                name: "FK_QUIZ_RELACIONADO_REDE_ID_REDE",
                table: "QUIZ_RELACIONADO");

            migrationBuilder.DropForeignKey(
                name: "FK_QUIZ_RELACIONADO_CARGO_ID_CARGO",
                table: "QUIZ_RELACIONADO");

            migrationBuilder.DropForeignKey(
                name: "FK_QUIZ_RELACIONADO_QUESTIONARIO_ID_QUIZ",
                table: "QUIZ_RELACIONADO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QUIZ_RELACIONADO",
                table: "QUIZ_RELACIONADO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QUIZ_LOJAS_RELACIONADA",
                table: "QUIZ_LOJAS_RELACIONADA");

            migrationBuilder.RenameTable(
                name: "QUIZ_RELACIONADO",
                newName: "QUESTIONARIO_RELACIONADO");

            migrationBuilder.RenameTable(
                name: "QUIZ_LOJAS_RELACIONADA",
                newName: "QUESTIONARIO_LOJAS_RELACIONADA");

            migrationBuilder.RenameIndex(
                name: "IX_QUIZ_RELACIONADO_ID_QUIZ",
                table: "QUESTIONARIO_RELACIONADO",
                newName: "IX_QUESTIONARIO_RELACIONADO_ID_QUIZ");

            migrationBuilder.RenameIndex(
                name: "IX_QUIZ_RELACIONADO_ID_CARGO",
                table: "QUESTIONARIO_RELACIONADO",
                newName: "IX_QUESTIONARIO_RELACIONADO_ID_CARGO");

            migrationBuilder.RenameIndex(
                name: "IX_QUIZ_RELACIONADO_ID_REDE",
                table: "QUESTIONARIO_RELACIONADO",
                newName: "IX_QUESTIONARIO_RELACIONADO_ID_REDE");

            migrationBuilder.RenameIndex(
                name: "IX_QUIZ_LOJAS_RELACIONADA_ID_LOJA",
                table: "QUESTIONARIO_LOJAS_RELACIONADA",
                newName: "IX_QUESTIONARIO_LOJAS_RELACIONADA_ID_LOJA");

            migrationBuilder.RenameIndex(
                name: "IX_QUIZ_LOJAS_RELACIONADA_ID_QUIZ_RELACIONADO",
                table: "QUESTIONARIO_LOJAS_RELACIONADA",
                newName: "IX_QUESTIONARIO_LOJAS_RELACIONADA_ID_QUIZ_RELACIONADO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QUESTIONARIO_RELACIONADO",
                table: "QUESTIONARIO_RELACIONADO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QUESTIONARIO_LOJAS_RELACIONADA",
                table: "QUESTIONARIO_LOJAS_RELACIONADA",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QUESTIONARIO_LOJAS_RELACIONADA_QUESTIONARIO_RELACIONADO_ID_QUIZ_RELACIONADO",
                table: "QUESTIONARIO_LOJAS_RELACIONADA",
                column: "ID_QUIZ_RELACIONADO",
                principalTable: "QUESTIONARIO_RELACIONADO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUESTIONARIO_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                table: "QUESTIONARIO_LOJAS_RELACIONADA",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUESTIONARIO_RELACIONADO_REDE_ID_REDE",
                table: "QUESTIONARIO_RELACIONADO",
                column: "ID_REDE",
                principalTable: "REDE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUESTIONARIO_RELACIONADO_CARGO_ID_CARGO",
                table: "QUESTIONARIO_RELACIONADO",
                column: "ID_CARGO",
                principalTable: "CARGO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUESTIONARIO_RELACIONADO_QUESTIONARIO_ID_QUIZ",
                table: "QUESTIONARIO_RELACIONADO",
                column: "ID_QUIZ",
                principalTable: "QUESTIONARIO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QUESTIONARIO_LOJAS_RELACIONADA_QUESTIONARIO_RELACIONADO_ID_QUIZ_RELACIONADO",
                table: "QUESTIONARIO_LOJAS_RELACIONADA");

            migrationBuilder.DropForeignKey(
                name: "FK_QUESTIONARIO_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                table: "QUESTIONARIO_LOJAS_RELACIONADA");

            migrationBuilder.DropForeignKey(
                name: "FK_QUESTIONARIO_RELACIONADO_REDE_ID_REDE",
                table: "QUESTIONARIO_RELACIONADO");

            migrationBuilder.DropForeignKey(
                name: "FK_QUESTIONARIO_RELACIONADO_CARGO_ID_CARGO",
                table: "QUESTIONARIO_RELACIONADO");

            migrationBuilder.DropForeignKey(
                name: "FK_QUESTIONARIO_RELACIONADO_QUESTIONARIO_ID_QUIZ",
                table: "QUESTIONARIO_RELACIONADO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QUESTIONARIO_RELACIONADO",
                table: "QUESTIONARIO_RELACIONADO");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QUESTIONARIO_LOJAS_RELACIONADA",
                table: "QUESTIONARIO_LOJAS_RELACIONADA");

            migrationBuilder.RenameTable(
                name: "QUESTIONARIO_RELACIONADO",
                newName: "QUIZ_RELACIONADO");

            migrationBuilder.RenameTable(
                name: "QUESTIONARIO_LOJAS_RELACIONADA",
                newName: "QUIZ_LOJAS_RELACIONADA");

            migrationBuilder.RenameIndex(
                name: "IX_QUESTIONARIO_RELACIONADO_ID_QUIZ",
                table: "QUIZ_RELACIONADO",
                newName: "IX_QUIZ_RELACIONADO_ID_QUIZ");

            migrationBuilder.RenameIndex(
                name: "IX_QUESTIONARIO_RELACIONADO_ID_CARGO",
                table: "QUIZ_RELACIONADO",
                newName: "IX_QUIZ_RELACIONADO_ID_CARGO");

            migrationBuilder.RenameIndex(
                name: "IX_QUESTIONARIO_RELACIONADO_ID_REDE",
                table: "QUIZ_RELACIONADO",
                newName: "IX_QUIZ_RELACIONADO_ID_REDE");

            migrationBuilder.RenameIndex(
                name: "IX_QUESTIONARIO_LOJAS_RELACIONADA_ID_LOJA",
                table: "QUIZ_LOJAS_RELACIONADA",
                newName: "IX_QUIZ_LOJAS_RELACIONADA_ID_LOJA");

            migrationBuilder.RenameIndex(
                name: "IX_QUESTIONARIO_LOJAS_RELACIONADA_ID_QUIZ_RELACIONADO",
                table: "QUIZ_LOJAS_RELACIONADA",
                newName: "IX_QUIZ_LOJAS_RELACIONADA_ID_QUIZ_RELACIONADO");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QUIZ_RELACIONADO",
                table: "QUIZ_RELACIONADO",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QUIZ_LOJAS_RELACIONADA",
                table: "QUIZ_LOJAS_RELACIONADA",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_QUIZ_LOJAS_RELACIONADA_QUIZ_RELACIONADO_ID_QUIZ_RELACIONADO",
                table: "QUIZ_LOJAS_RELACIONADA",
                column: "ID_QUIZ_RELACIONADO",
                principalTable: "QUIZ_RELACIONADO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUIZ_LOJAS_RELACIONADA_LOJA_ID_LOJA",
                table: "QUIZ_LOJAS_RELACIONADA",
                column: "ID_LOJA",
                principalTable: "LOJA",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUIZ_RELACIONADO_REDE_ID_REDE",
                table: "QUIZ_RELACIONADO",
                column: "ID_REDE",
                principalTable: "REDE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUIZ_RELACIONADO_CARGO_ID_CARGO",
                table: "QUIZ_RELACIONADO",
                column: "ID_CARGO",
                principalTable: "CARGO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QUIZ_RELACIONADO_QUESTIONARIO_ID_QUIZ",
                table: "QUIZ_RELACIONADO",
                column: "ID_QUIZ",
                principalTable: "QUESTIONARIO",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
