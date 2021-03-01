using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class AdicaoTelefones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Telefone",
                "Funcionarios");

            migrationBuilder.CreateTable(
                "Telefone",
                table => new
                {
                    Numero = table.Column<string>("TEXT", nullable: false),
                    FuncionarioNumeroChapa = table.Column<string>("TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Numero);
                    table.ForeignKey(
                        "FK_Telefone_Funcionarios_FuncionarioNumeroChapa",
                        x => x.FuncionarioNumeroChapa,
                        "Funcionarios",
                        "NumeroChapa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                "Funcionarios",
                "NumeroChapa",
                "11",
                "DataCadastro",
                new DateTime(2021, 2, 27, 16, 21, 7, 654, DateTimeKind.Local).AddTicks(1265));

            migrationBuilder.CreateIndex(
                "IX_Telefone_FuncionarioNumeroChapa",
                "Telefone",
                "FuncionarioNumeroChapa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Telefone");

            migrationBuilder.AddColumn<string>(
                "Telefone",
                "Funcionarios",
                "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                "Funcionarios",
                "NumeroChapa",
                "11",
                "DataCadastro",
                new DateTime(2021, 2, 27, 15, 48, 50, 199, DateTimeKind.Local).AddTicks(7183));
        }
    }
}