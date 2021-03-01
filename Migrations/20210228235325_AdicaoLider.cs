using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace rh_admin.Migrations
{
    public partial class AdicaoLider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "LiderNumeroChapa",
                "Funcionarios",
                "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                "Funcionarios",
                "NumeroChapa",
                "11",
                "DataCadastro",
                new DateTime(2021, 2, 28, 20, 53, 25, 331, DateTimeKind.Local).AddTicks(1293));

            migrationBuilder.CreateIndex(
                "IX_Funcionarios_LiderNumeroChapa",
                "Funcionarios",
                "LiderNumeroChapa");

            migrationBuilder.AddForeignKey(
                "FK_Funcionarios_Funcionarios_LiderNumeroChapa",
                "Funcionarios",
                "LiderNumeroChapa",
                "Funcionarios",
                principalColumn: "NumeroChapa",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Funcionarios_Funcionarios_LiderNumeroChapa",
                "Funcionarios");

            migrationBuilder.DropIndex(
                "IX_Funcionarios_LiderNumeroChapa",
                "Funcionarios");

            migrationBuilder.DropColumn(
                "LiderNumeroChapa",
                "Funcionarios");

            migrationBuilder.UpdateData(
                "Funcionarios",
                "NumeroChapa",
                "11",
                "DataCadastro",
                new DateTime(2021, 2, 27, 16, 21, 7, 654, DateTimeKind.Local).AddTicks(1265));
        }
    }
}