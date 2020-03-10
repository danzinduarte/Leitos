using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace api.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Acesso_siaf",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Numero_serie = table.Column<string>(nullable: true),
                    Cpf = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Contrato = table.Column<string>(nullable: true),
                    Data_hora_acesso = table.Column<DateTime>(nullable: false),
                    Android_gourmet = table.Column<string>(nullable: true),
                    Num_dispositivo = table.Column<int>(nullable: false),
                    Android_pedidos = table.Column<string>(nullable: true),
                    Num_dispositivos_pedidos = table.Column<int>(nullable: false),
                    Observacao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Acesso_siaf", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Controle_acesso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Razao_social = table.Column<string>(nullable: true),
                    Nome_fantasia = table.Column<string>(nullable: true),
                    Cnpj = table.Column<string>(nullable: true),
                    Numero_serie = table.Column<string>(nullable: true),
                    Versao_siaf = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true),
                    Versao_ws = table.Column<string>(nullable: true),
                    Status_aesso = table.Column<string>(nullable: true),
                    Data_hora_acesso = table.Column<DateTime>(nullable: false),
                    Tipo_contrato = table.Column<string>(nullable: true),
                    Tipo_versao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controle_acesso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    email = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Administrador = table.Column<double>(nullable: false),
                    Desativado = table.Column<double>(nullable: false),
                    Data_desativacao = table.Column<DateTime>(nullable: false),
                    Log_criacao = table.Column<DateTime>(nullable: false),
                    Log_atualizacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario_permissao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Usuario_id = table.Column<int>(nullable: false),
                    Rotina = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario_permissao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_permissao_Usuario_Usuario_id",
                        column: x => x.Usuario_id,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_permissao_Usuario_id",
                table: "Usuario_permissao",
                column: "Usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Acesso_siaf");

            migrationBuilder.DropTable(
                name: "Controle_acesso");

            migrationBuilder.DropTable(
                name: "Usuario_permissao");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
