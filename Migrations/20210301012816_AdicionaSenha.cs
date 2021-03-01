using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class AdicionaSenha : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "NumeroChapa",
                keyValue: "11");

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "Funcionarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Funcionarios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salt",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Funcionarios");

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "NumeroChapa", "DataCadastro", "Email", "LiderNumeroChapa", "Nome", "Sobrenome" },
                values: new object[] { "11", new DateTime(2021, 2, 28, 20, 53, 25, 331, DateTimeKind.Local).AddTicks(1293), "teste@gmail.com", null, "teste", "Testoso" });
        }
    }
}
