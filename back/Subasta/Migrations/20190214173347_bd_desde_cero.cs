using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Subasta.Migrations
{
    public partial class bd_desde_cero : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CATEGORIAS",
                columns: table => new
                {
                    CODIGO_CAT = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_CAT = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CATEGORIAS", x => x.CODIGO_CAT);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DEPARTAMENTOS",
                columns: table => new
                {
                    CODIGO_DPTO = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_DPTO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DEPARTAMENTOS", x => x.CODIGO_DPTO);
                });

            migrationBuilder.CreateTable(
                name: "TBL_RAZAS",
                columns: table => new
                {
                    CODIGO_RAZA = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_RAZA = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RAZAS", x => x.CODIGO_RAZA);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ROLES",
                columns: table => new
                {
                    CODIGO_ROL = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_ROL = table.Column<string>(nullable: true),
                    DESCRIPCION_ROL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ROLES", x => x.CODIGO_ROL);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SEXOS",
                columns: table => new
                {
                    CODIGO_SEXO = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_SEXO = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SEXOS", x => x.CODIGO_SEXO);
                });

            migrationBuilder.CreateTable(
                name: "TBL_TIPO_DOCUMENTOS",
                columns: table => new
                {
                    CODIGO_TD = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_TD = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_TIPO_DOCUMENTOS", x => x.CODIGO_TD);
                });

            migrationBuilder.CreateTable(
                name: "TBL_MUNICIPIOS",
                columns: table => new
                {
                    CODIGO_MUN = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_MUN = table.Column<string>(nullable: true),
                    COD_DPTO_MUN = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_MUNICIPIOS", x => x.CODIGO_MUN);
                    table.ForeignKey(
                        name: "FK_TBL_MUNICIPIOS_TBL_DEPARTAMENTOS_COD_DPTO_MUN",
                        column: x => x.COD_DPTO_MUN,
                        principalTable: "TBL_DEPARTAMENTOS",
                        principalColumn: "CODIGO_DPTO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USUARIOS",
                columns: table => new
                {
                    CODIGO_USU = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_USUARIO = table.Column<string>(nullable: true),
                    CORREO_USUARIO = table.Column<string>(nullable: true),
                    PASS_USUARIO = table.Column<string>(nullable: true),
                    ROL_USUARIO = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USUARIOS", x => x.CODIGO_USU);
                    table.ForeignKey(
                        name: "FK_TBL_USUARIOS_TBL_ROLES_ROL_USUARIO",
                        column: x => x.ROL_USUARIO,
                        principalTable: "TBL_ROLES",
                        principalColumn: "CODIGO_ROL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_CLIENTES",
                columns: table => new
                {
                    ID_CLI = table.Column<string>(nullable: false),
                    NOMBRE_CLI = table.Column<string>(nullable: true),
                    CORREO_CLI = table.Column<string>(nullable: true),
                    TELEFONO_CLI = table.Column<string>(nullable: true),
                    DIRECCION_CLI = table.Column<string>(nullable: true),
                    REPRESENTANTE_LEGAL_CLI = table.Column<string>(nullable: true),
                    USUARIO_CLI = table.Column<string>(nullable: true),
                    TIPO_CLI = table.Column<string>(nullable: true),
                    ACTIVO_CLI = table.Column<bool>(nullable: false),
                    CODIGO_TD_CLI = table.Column<int>(nullable: false),
                    CODIGO_MUN_CLI = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CLIENTES", x => x.ID_CLI);
                    table.ForeignKey(
                        name: "FK_TBL_CLIENTES_TBL_MUNICIPIOS_CODIGO_MUN_CLI",
                        column: x => x.CODIGO_MUN_CLI,
                        principalTable: "TBL_MUNICIPIOS",
                        principalColumn: "CODIGO_MUN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_CLIENTES_TBL_TIPO_DOCUMENTOS_CODIGO_TD_CLI",
                        column: x => x.CODIGO_TD_CLI,
                        principalTable: "TBL_TIPO_DOCUMENTOS",
                        principalColumn: "CODIGO_TD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_EVENTOS",
                columns: table => new
                {
                    CODIGO_EVEN = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_EVEN = table.Column<string>(nullable: true),
                    FECHA_INI_EVEN = table.Column<DateTime>(nullable: false),
                    FECHA_FIN_EVEN = table.Column<DateTime>(nullable: false),
                    ACTIVO_EVEN = table.Column<bool>(nullable: false),
                    UBICACION_EVEN = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_EVENTOS", x => x.CODIGO_EVEN);
                    table.ForeignKey(
                        name: "FK_TBL_EVENTOS_TBL_MUNICIPIOS_UBICACION_EVEN",
                        column: x => x.UBICACION_EVEN,
                        principalTable: "TBL_MUNICIPIOS",
                        principalColumn: "CODIGO_MUN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SUBASTAS",
                columns: table => new
                {
                    CODIGO_SUB = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DESCRIPCION_SUB = table.Column<string>(nullable: true),
                    VALOR_ANTICIPO_SUB = table.Column<decimal>(nullable: false),
                    FECHA_HORA_INI_SUB = table.Column<DateTime>(nullable: false),
                    FECHA_LIMITE_FIN_SUB = table.Column<DateTime>(nullable: false),
                    ACTIVO_SUB = table.Column<bool>(nullable: false),
                    CODIGO_EVENTO_SUB = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SUBASTAS", x => x.CODIGO_SUB);
                    table.ForeignKey(
                        name: "FK_TBL_SUBASTAS_TBL_EVENTOS_CODIGO_EVENTO_SUB",
                        column: x => x.CODIGO_EVENTO_SUB,
                        principalTable: "TBL_EVENTOS",
                        principalColumn: "CODIGO_EVEN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_LOTES",
                columns: table => new
                {
                    CODIGO_LOTE = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOMBRE_LOTE = table.Column<string>(nullable: true),
                    DESCRIPCION_LOTE = table.Column<string>(nullable: true),
                    CANTIDAD_ELEMENTOS_LOTE = table.Column<int>(nullable: false),
                    PESO_PROMEDIO_LOTE = table.Column<decimal>(nullable: false),
                    PESO_TOTAL_LOTE = table.Column<decimal>(nullable: false),
                    PRECIO_BASE_LOTE = table.Column<decimal>(nullable: false),
                    FOTO_LOTE = table.Column<string>(nullable: true),
                    ACTIVO_LOTE = table.Column<bool>(nullable: false),
                    ID_CLIENTE_LOTE = table.Column<string>(nullable: true),
                    COD_MUN_UBI_LOTE = table.Column<int>(nullable: false),
                    COD_SUBASTA_LOTE = table.Column<int>(nullable: false),
                    PRECIO_INICIAL_LOTE = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_LOTES", x => x.CODIGO_LOTE);
                    table.ForeignKey(
                        name: "FK_TBL_LOTES_TBL_CLIENTES_ID_CLIENTE_LOTE",
                        column: x => x.ID_CLIENTE_LOTE,
                        principalTable: "TBL_CLIENTES",
                        principalColumn: "ID_CLI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_LOTES_TBL_MUNICIPIOS_COD_MUN_UBI_LOTE",
                        column: x => x.COD_MUN_UBI_LOTE,
                        principalTable: "TBL_MUNICIPIOS",
                        principalColumn: "CODIGO_MUN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_LOTES_TBL_SUBASTAS_COD_SUBASTA_LOTE",
                        column: x => x.COD_SUBASTA_LOTE,
                        principalTable: "TBL_SUBASTAS",
                        principalColumn: "CODIGO_SUB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_SOLICITUDES",
                columns: table => new
                {
                    CODIGO_SOLI = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ESTADO_SOLI = table.Column<string>(nullable: true),
                    SUBASTA_SOLI = table.Column<int>(nullable: false),
                    CLIENTE_SOLI = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_SOLICITUDES", x => x.CODIGO_SOLI);
                    table.ForeignKey(
                        name: "FK_TBL_SOLICITUDES_TBL_CLIENTES_CLIENTE_SOLI",
                        column: x => x.CLIENTE_SOLI,
                        principalTable: "TBL_CLIENTES",
                        principalColumn: "ID_CLI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_SOLICITUDES_TBL_SUBASTAS_SUBASTA_SOLI",
                        column: x => x.SUBASTA_SOLI,
                        principalTable: "TBL_SUBASTAS",
                        principalColumn: "CODIGO_SUB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_ANIMALES",
                columns: table => new
                {
                    CODIGO_ANI = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FOTO_ANI = table.Column<string>(nullable: true),
                    PESO_ANI = table.Column<decimal>(nullable: false),
                    DESCRIPCION_ANI = table.Column<string>(nullable: true),
                    ACTIVO_ANI = table.Column<bool>(nullable: false),
                    COD_CATEGORIA_ANI = table.Column<int>(nullable: false),
                    COD_RAZA_ANI = table.Column<int>(nullable: false),
                    COD_SEXO_ANI = table.Column<int>(nullable: false),
                    COD_MUN_PROCE_ANI = table.Column<int>(nullable: false),
                    COD_LOTE_ANI = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_ANIMALES", x => x.CODIGO_ANI);
                    table.ForeignKey(
                        name: "FK_TBL_ANIMALES_TBL_CATEGORIAS_COD_CATEGORIA_ANI",
                        column: x => x.COD_CATEGORIA_ANI,
                        principalTable: "TBL_CATEGORIAS",
                        principalColumn: "CODIGO_CAT",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ANIMALES_TBL_LOTES_COD_LOTE_ANI",
                        column: x => x.COD_LOTE_ANI,
                        principalTable: "TBL_LOTES",
                        principalColumn: "CODIGO_LOTE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ANIMALES_TBL_MUNICIPIOS_COD_MUN_PROCE_ANI",
                        column: x => x.COD_MUN_PROCE_ANI,
                        principalTable: "TBL_MUNICIPIOS",
                        principalColumn: "CODIGO_MUN",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ANIMALES_TBL_RAZAS_COD_RAZA_ANI",
                        column: x => x.COD_RAZA_ANI,
                        principalTable: "TBL_RAZAS",
                        principalColumn: "CODIGO_RAZA",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBL_ANIMALES_TBL_SEXOS_COD_SEXO_ANI",
                        column: x => x.COD_SEXO_ANI,
                        principalTable: "TBL_SEXOS",
                        principalColumn: "CODIGO_SEXO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_PUJADORES",
                columns: table => new
                {
                    ID_PUJADOR = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CODIGO_LOTE_PUJADOR = table.Column<int>(nullable: false),
                    ID_CLI_PUJADOR = table.Column<string>(nullable: true),
                    NRO_CONSIGNACION_PUJADOR = table.Column<string>(nullable: true),
                    BANCO_CONSIGNACION_PUJADOR = table.Column<string>(nullable: true),
                    VALOR_CONSIGNACION_PUJADOR = table.Column<decimal>(nullable: false),
                    ESTADO_PUJADOR = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PUJADORES", x => x.ID_PUJADOR);
                    table.ForeignKey(
                        name: "FK_TBL_PUJADORES_TBL_CLIENTES_ID_CLI_PUJADOR",
                        column: x => x.ID_CLI_PUJADOR,
                        principalTable: "TBL_CLIENTES",
                        principalColumn: "ID_CLI",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_PUJADORES_TBL_LOTES_CODIGO_LOTE_PUJADOR",
                        column: x => x.CODIGO_LOTE_PUJADOR,
                        principalTable: "TBL_LOTES",
                        principalColumn: "CODIGO_LOTE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TBL_PUJAS",
                columns: table => new
                {
                    CODIGO_PUJA = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HORA_PUJA = table.Column<DateTime>(nullable: false),
                    VALOR_PUJA = table.Column<decimal>(nullable: false),
                    COD_SUBASTA_PUJADOR = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_PUJAS", x => x.CODIGO_PUJA);
                    table.ForeignKey(
                        name: "FK_TBL_PUJAS_TBL_PUJADORES_COD_SUBASTA_PUJADOR",
                        column: x => x.COD_SUBASTA_PUJADOR,
                        principalTable: "TBL_PUJADORES",
                        principalColumn: "ID_PUJADOR",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ANIMALES_COD_CATEGORIA_ANI",
                table: "TBL_ANIMALES",
                column: "COD_CATEGORIA_ANI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ANIMALES_COD_LOTE_ANI",
                table: "TBL_ANIMALES",
                column: "COD_LOTE_ANI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ANIMALES_COD_MUN_PROCE_ANI",
                table: "TBL_ANIMALES",
                column: "COD_MUN_PROCE_ANI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ANIMALES_COD_RAZA_ANI",
                table: "TBL_ANIMALES",
                column: "COD_RAZA_ANI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_ANIMALES_COD_SEXO_ANI",
                table: "TBL_ANIMALES",
                column: "COD_SEXO_ANI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CLIENTES_CODIGO_MUN_CLI",
                table: "TBL_CLIENTES",
                column: "CODIGO_MUN_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CLIENTES_CODIGO_TD_CLI",
                table: "TBL_CLIENTES",
                column: "CODIGO_TD_CLI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_EVENTOS_UBICACION_EVEN",
                table: "TBL_EVENTOS",
                column: "UBICACION_EVEN");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_ID_CLIENTE_LOTE",
                table: "TBL_LOTES",
                column: "ID_CLIENTE_LOTE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_COD_MUN_UBI_LOTE",
                table: "TBL_LOTES",
                column: "COD_MUN_UBI_LOTE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_LOTES_COD_SUBASTA_LOTE",
                table: "TBL_LOTES",
                column: "COD_SUBASTA_LOTE");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_MUNICIPIOS_COD_DPTO_MUN",
                table: "TBL_MUNICIPIOS",
                column: "COD_DPTO_MUN");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PUJADORES_ID_CLI_PUJADOR",
                table: "TBL_PUJADORES",
                column: "ID_CLI_PUJADOR");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PUJADORES_CODIGO_LOTE_PUJADOR",
                table: "TBL_PUJADORES",
                column: "CODIGO_LOTE_PUJADOR");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_PUJAS_COD_SUBASTA_PUJADOR",
                table: "TBL_PUJAS",
                column: "COD_SUBASTA_PUJADOR");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SOLICITUDES_CLIENTE_SOLI",
                table: "TBL_SOLICITUDES",
                column: "CLIENTE_SOLI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SOLICITUDES_SUBASTA_SOLI",
                table: "TBL_SOLICITUDES",
                column: "SUBASTA_SOLI");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_SUBASTAS_CODIGO_EVENTO_SUB",
                table: "TBL_SUBASTAS",
                column: "CODIGO_EVENTO_SUB");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USUARIOS_ROL_USUARIO",
                table: "TBL_USUARIOS",
                column: "ROL_USUARIO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_ANIMALES");

            migrationBuilder.DropTable(
                name: "TBL_PUJAS");

            migrationBuilder.DropTable(
                name: "TBL_SOLICITUDES");

            migrationBuilder.DropTable(
                name: "TBL_USUARIOS");

            migrationBuilder.DropTable(
                name: "TBL_CATEGORIAS");

            migrationBuilder.DropTable(
                name: "TBL_RAZAS");

            migrationBuilder.DropTable(
                name: "TBL_SEXOS");

            migrationBuilder.DropTable(
                name: "TBL_PUJADORES");

            migrationBuilder.DropTable(
                name: "TBL_ROLES");

            migrationBuilder.DropTable(
                name: "TBL_LOTES");

            migrationBuilder.DropTable(
                name: "TBL_CLIENTES");

            migrationBuilder.DropTable(
                name: "TBL_SUBASTAS");

            migrationBuilder.DropTable(
                name: "TBL_TIPO_DOCUMENTOS");

            migrationBuilder.DropTable(
                name: "TBL_EVENTOS");

            migrationBuilder.DropTable(
                name: "TBL_MUNICIPIOS");

            migrationBuilder.DropTable(
                name: "TBL_DEPARTAMENTOS");
        }
    }
}
