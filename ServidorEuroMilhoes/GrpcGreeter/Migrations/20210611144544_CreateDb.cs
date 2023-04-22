using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Servidor.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApostasArquivadas",
                columns: table => new
                {
                    nif = table.Column<int>(type: "int", nullable: false),
                    DataAposta = table.Column<DateTime>(type: "datetime", nullable: false),
                    numeros = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    estrelas = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ApostasA__839469AF50F0464C", x => new { x.nif, x.DataAposta });
                });

            migrationBuilder.CreateTable(
                name: "ApostasAtuais",
                columns: table => new
                {
                    nif = table.Column<int>(type: "int", nullable: false),
                    DataAposta = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    numeros = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    estrelas = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SorteioAtual = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ApostasA__839469AF90EABA56", x => new { x.nif, x.DataAposta });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApostasArquivadas");

            migrationBuilder.DropTable(
                name: "ApostasAtuais");
        }
    }
}
