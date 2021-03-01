using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Funcionarios",
                table => new
                {
                    NumeroChapa = table.Column<string>("TEXT", nullable: false),
                    Nome = table.Column<string>("TEXT", nullable: false),
                    Sobrenome = table.Column<string>("TEXT", nullable: false),
                    Email = table.Column<string>("TEXT", nullable: false),
                    Telefone = table.Column<string>("TEXT", nullable: true),
                    DataCadastro = table.Column<DateTime>("TEXT", nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Funcionarios", x => x.NumeroChapa); });

            migrationBuilder.InsertData(
                "Funcionarios",
                new[] {"NumeroChapa", "DataCadastro", "Email", "Nome", "Sobrenome", "Telefone"},
                new object[]
                {
                    "11", new DateTime(2021, 2, 27, 15, 48, 50, 199, DateTimeKind.Local).AddTicks(7183),
                    "teste@gmail.com", "teste", "Testoso", null
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Funcionarios");
        }
    }
}