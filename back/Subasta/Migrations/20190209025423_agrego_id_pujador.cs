using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class agrego_id_pujador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJADORES_TBL_CLIENTES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS");

            migrationBuilder.DropIndex(
                name: "IX_TBL_PUJAS_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_PUJADORES",
                table: "TBL_PUJADORES");

            migrationBuilder.DropColumn(
                name: "PujadorClienteId",
                table: "TBL_PUJAS");

            migrationBuilder.DropColumn(
                name: "PujadorLoteId",
                table: "TBL_PUJAS");

            migrationBuilder.AlterColumn<int>(
                name: "CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ID_CLI_PUJADOR",
                table: "TBL_PUJADORES",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "ID_PUJADOR",
                table: "TBL_PUJADORES",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_PUJADORES",
                table: "TBL_PUJADORES",
                column: "ID_PUJADOR");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PUJAS_COD_SUBASTA_PUJADOR",
                table: "TBL_PUJAS",
                column: "COD_SUBASTA_PUJADOR");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PUJADORES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES",
                column: "ID_CLI_PUJADOR");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJADORES_TBL_CLIENTES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES",
                column: "ID_CLI_PUJADOR",
                principalTable: "TBL_CLIENTES",
                principalColumn: "ID_CLI",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_COD_SUBASTA_PUJADOR",
                table: "TBL_PUJAS",
                column: "COD_SUBASTA_PUJADOR",
                principalTable: "TBL_PUJADORES",
                principalColumn: "ID_PUJADOR",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJADORES_TBL_CLIENTES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_COD_SUBASTA_PUJADOR",
                table: "TBL_PUJAS");

            migrationBuilder.DropIndex(
                name: "IX_TBL_PUJAS_COD_SUBASTA_PUJADOR",
                table: "TBL_PUJAS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_PUJADORES",
                table: "TBL_PUJADORES");

            migrationBuilder.DropIndex(
                name: "IX_TBL_PUJADORES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES");

            migrationBuilder.DropColumn(
                name: "ID_PUJADOR",
                table: "TBL_PUJADORES");

            migrationBuilder.AddColumn<string>(
                name: "PujadorClienteId",
                table: "TBL_PUJAS",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PujadorLoteId",
                table: "TBL_PUJAS",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "ID_CLI_PUJADOR",
                table: "TBL_PUJADORES",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_PUJADORES",
                table: "TBL_PUJADORES",
                columns: new[] { "ID_CLI_PUJADOR", "CODIGO_LOTE_PUJADOR" });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PUJAS_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS",
                columns: new[] { "PujadorClienteId", "PujadorLoteId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJADORES_TBL_CLIENTES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES",
                column: "ID_CLI_PUJADOR",
                principalTable: "TBL_CLIENTES",
                principalColumn: "ID_CLI",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS",
                columns: new[] { "PujadorClienteId", "PujadorLoteId" },
                principalTable: "TBL_PUJADORES",
                principalColumns: new[] { "ID_CLI_PUJADOR", "CODIGO_LOTE_PUJADOR" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
