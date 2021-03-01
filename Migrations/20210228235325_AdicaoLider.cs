using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class AdicaoLider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LiderNumeroChapa",
                table: "Funcionarios",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "NumeroChapa",
                keyValue: "11",
                column: "DataCadastro",
                value: new DateTime(2021, 2, 28, 20, 53, 25, 331, DateTimeKind.Local).AddTicks(1293));

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_LiderNumeroChapa",
                table: "Funcionarios",
                column: "LiderNumeroChapa");

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionarios_Funcionarios_LiderNumeroChapa",
                table: "Funcionarios",
                column: "LiderNumeroChapa",
                principalTable: "Funcionarios",
                principalColumn: "NumeroChapa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Funcionarios_Funcionarios_LiderNumeroChapa",
                table: "Funcionarios");

            migrationBuilder.DropIndex(
                name: "IX_Funcionarios_LiderNumeroChapa",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "LiderNumeroChapa",
                table: "Funcionarios");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "NumeroChapa",
                keyValue: "11",
                column: "DataCadastro",
                value: new DateTime(2021, 2, 27, 16, 21, 7, 654, DateTimeKind.Local).AddTicks(1265));
        }
    }
}
