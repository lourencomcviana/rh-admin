using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class AdicionaSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "Funcionarios",
                "NumeroChapa",
                "11");

            migrationBuilder.AddColumn<string>(
                "Salt",
                "Funcionarios",
                "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                "Senha",
                "Funcionarios",
                "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Salt",
                "Funcionarios");

            migrationBuilder.DropColumn(
                "Senha",
                "Funcionarios");

            migrationBuilder.InsertData(
                "Funcionarios",
                new[] {"NumeroChapa", "DataCadastro", "Email", "LiderNumeroChapa", "Nome", "Sobrenome"},
                new object[]
                {
                    "11", new DateTime(2021, 2, 28, 20, 53, 25, 331, DateTimeKind.Local).AddTicks(1293),
                    "teste@gmail.com", null, "teste", "Testoso"
                });
        }
    }
}