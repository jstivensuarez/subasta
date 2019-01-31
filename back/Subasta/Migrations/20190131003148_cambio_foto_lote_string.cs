using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class cambio_foto_lote_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FOTO_LOTE",
                table: "TBL_LOTES",
                nullable: true,
                oldClrType: typeof(decimal));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "FOTO_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
