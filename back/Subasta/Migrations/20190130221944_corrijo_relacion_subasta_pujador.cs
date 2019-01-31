using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class corrijo_relacion_subasta_pujador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJADORES_TBL_SUBASTAS_CODIGO_SUBASTA_PUJADOR",
                table: "TBL_PUJADORES");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_PujadorClienteId_PujadorSubastaId",
                table: "TBL_PUJAS");

            migrationBuilder.RenameColumn(
                name: "PujadorSubastaId",
                table: "TBL_PUJAS",
                newName: "PujadorLoteId");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_PUJAS_PujadorClienteId_PujadorSubastaId",
                table: "TBL_PUJAS",
                newName: "IX_TBL_PUJAS_PujadorClienteId_PujadorLoteId");

            migrationBuilder.RenameColumn(
                name: "CODIGO_SUBASTA_PUJADOR",
                table: "TBL_PUJADORES",
                newName: "CODIGO_LOTE_PUJADOR");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_PUJADORES_CODIGO_SUBASTA_PUJADOR",
                table: "TBL_PUJADORES",
                newName: "IX_TBL_PUJADORES_CODIGO_LOTE_PUJADOR");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJADORES_TBL_LOTES_CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES",
                column: "CODIGO_LOTE_PUJADOR",
                principalTable: "TBL_LOTES",
                principalColumn: "CODIGO_LOTE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS",
                columns: new[] { "PujadorClienteId", "PujadorLoteId" },
                principalTable: "TBL_PUJADORES",
                principalColumns: new[] { "ID_CLI_PUJADOR", "CODIGO_LOTE_PUJADOR" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJADORES_TBL_LOTES_CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS");

            migrationBuilder.RenameColumn(
                name: "PujadorLoteId",
                table: "TBL_PUJAS",
                newName: "PujadorSubastaId");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_PUJAS_PujadorClienteId_PujadorLoteId",
                table: "TBL_PUJAS",
                newName: "IX_TBL_PUJAS_PujadorClienteId_PujadorSubastaId");

            migrationBuilder.RenameColumn(
                name: "CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES",
                newName: "CODIGO_SUBASTA_PUJADOR");

            migrationBuilder.RenameIndex(
                name: "IX_TBL_PUJADORES_CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES",
                newName: "IX_TBL_PUJADORES_CODIGO_SUBASTA_PUJADOR");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJADORES_TBL_SUBASTAS_CODIGO_SUBASTA_PUJADOR",
                table: "TBL_PUJADORES",
                column: "CODIGO_SUBASTA_PUJADOR",
                principalTable: "TBL_SUBASTAS",
                principalColumn: "CODIGO_SUB",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_PUJAS_TBL_PUJADORES_PujadorClienteId_PujadorSubastaId",
                table: "TBL_PUJAS",
                columns: new[] { "PujadorClienteId", "PujadorSubastaId" },
                principalTable: "TBL_PUJADORES",
                principalColumns: new[] { "ID_CLI_PUJADOR", "CODIGO_SUBASTA_PUJADOR" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
