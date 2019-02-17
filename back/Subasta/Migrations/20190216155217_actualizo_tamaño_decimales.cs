using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class actualizo_tamaño_decimales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_ANTICIPO_SUB",
                table: "TBL_SUBASTAS",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_PUJA",
                table: "TBL_PUJAS",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_CONSIGNACION_PUJADOR",
                table: "TBL_PUJADORES",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECIO_INICIAL_LOTE",
                table: "TBL_LOTES",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECIO_BASE_LOTE",
                table: "TBL_LOTES",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PESO_TOTAL_LOTE",
                table: "TBL_LOTES",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PESO_PROMEDIO_LOTE",
                table: "TBL_LOTES",
                type: "decimal(10, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PESO_ANI",
                table: "TBL_ANIMALES",
                type: "decimal(15, 4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_ANTICIPO_SUB",
                table: "TBL_SUBASTAS",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_PUJA",
                table: "TBL_PUJAS",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "VALOR_CONSIGNACION_PUJADOR",
                table: "TBL_PUJADORES",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECIO_INICIAL_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PRECIO_BASE_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PESO_TOTAL_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PESO_PROMEDIO_LOTE",
                table: "TBL_LOTES",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(10, 4)",
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "PESO_ANI",
                table: "TBL_ANIMALES",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(15, 4)",
                oldDefaultValue: 0m);
        }
    }
}
