using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class AdicaoTelefones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Funcionarios");

            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    Numero = table.Column<string>(type: "TEXT", nullable: false),
                    FuncionarioNumeroChapa = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_Telefone_Funcionarios_FuncionarioNumeroChapa",
                        column: x => x.FuncionarioNumeroChapa,
                        principalTable: "Funcionarios",
                        principalColumn: "NumeroChapa",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "NumeroChapa",
                keyValue: "11",
                column: "DataCadastro",
                value: new DateTime(2021, 2, 27, 16, 21, 7, 654, DateTimeKind.Local).AddTicks(1265));

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_FuncionarioNumeroChapa",
                table: "Telefone",
                column: "FuncionarioNumeroChapa");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefone");

            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Funcionarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "NumeroChapa",
                keyValue: "11",
                column: "DataCadastro",
                value: new DateTime(2021, 2, 27, 15, 48, 50, 199, DateTimeKind.Local).AddTicks(7183));
        }
    }
}
