using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class corrijo_campos_lote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_LOTES_TBL_MUNICIPIOS_MunicipioId1",
                table: "TBL_LOTES");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_LOTES_TBL_SUBASTAS_SubastaId1",
                table: "TBL_LOTES");

            migrationBuilder.DropIndex(
                name: "IX_TBL_LOTES_MunicipioId1",
                table: "TBL_LOTES");

            migrationBuilder.DropIndex(
                name: "IX_TBL_LOTES_SubastaId1",
                table: "TBL_LOTES");

            migrationBuilder.DropColumn(
                name: "MunicipioId1",
                table: "TBL_LOTES");

            migrationBuilder.DropColumn(
                name: "SubastaId1",
                table: "TBL_LOTES");

            migrationBuilder.AlterColumn<int>(
                name: "COD_SUBASTA_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COD_MUN_UBI_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_COD_MUN_UBI_LOTE",
                table: "TBL_LOTES",
                column: "COD_MUN_UBI_LOTE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_COD_SUBASTA_LOTE",
                table: "TBL_LOTES",
                column: "COD_SUBASTA_LOTE");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_LOTES_TBL_MUNICIPIOS_COD_MUN_UBI_LOTE",
                table: "TBL_LOTES",
                column: "COD_MUN_UBI_LOTE",
                principalTable: "TBL_MUNICIPIOS",
                principalColumn: "CODIGO_MUN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_LOTES_TBL_SUBASTAS_COD_SUBASTA_LOTE",
                table: "TBL_LOTES",
                column: "COD_SUBASTA_LOTE",
                principalTable: "TBL_SUBASTAS",
                principalColumn: "CODIGO_SUB",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_LOTES_TBL_MUNICIPIOS_COD_MUN_UBI_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.DropForeignKey(
                name: "FK_TBL_LOTES_TBL_SUBASTAS_COD_SUBASTA_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.DropIndex(
                name: "IX_TBL_LOTES_COD_MUN_UBI_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.DropIndex(
                name: "IX_TBL_LOTES_COD_SUBASTA_LOTE",
                table: "TBL_LOTES");

            migrationBuilder.AlterColumn<string>(
                name: "COD_SUBASTA_LOTE",
                table: "TBL_LOTES",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "COD_MUN_UBI_LOTE",
                table: "TBL_LOTES",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MunicipioId1",
                table: "TBL_LOTES",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubastaId1",
                table: "TBL_LOTES",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_MunicipioId1",
                table: "TBL_LOTES",
                column: "MunicipioId1");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_SubastaId1",
                table: "TBL_LOTES",
                column: "SubastaId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_LOTES_TBL_MUNICIPIOS_MunicipioId1",
                table: "TBL_LOTES",
                column: "MunicipioId1",
                principalTable: "TBL_MUNICIPIOS",
                principalColumn: "CODIGO_MUN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_LOTES_TBL_SUBASTAS_SubastaId1",
                table: "TBL_LOTES",
                column: "SubastaId1",
                principalTable: "TBL_SUBASTAS",
                principalColumn: "CODIGO_SUB",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
