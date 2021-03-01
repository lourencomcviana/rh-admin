﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using rh_admin.Repositorys;

namespace rh_admin.Migrations
{
    [DbContext(typeof(FuncionarioContext))]
    internal class FuncionarioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("rh_admin.Models.Funcionario", b =>
            {
                b.Property<string>("NumeroChapa")
                    .HasColumnType("TEXT");

                b.Property<DateTime>("DataCadastro")
                    .HasColumnType("TEXT");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("LiderNumeroChapa")
                    .HasColumnType("TEXT");

                b.Property<string>("Nome")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Salt")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Senha")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.Property<string>("Sobrenome")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("NumeroChapa");

                b.HasIndex("LiderNumeroChapa");

                b.ToTable("Funcionarios");
            });

            modelBuilder.Entity("rh_admin.Models.Telefone", b =>
            {
                b.Property<string>("Numero")
                    .HasColumnType("TEXT");

                b.Property<string>("FuncionarioNumeroChapa")
                    .IsRequired()
                    .HasColumnType("TEXT");

                b.HasKey("Numero");

                b.HasIndex("FuncionarioNumeroChapa");

                b.ToTable("Telefone");
            });

            modelBuilder.Entity("rh_admin.Models.Funcionario", b =>
            {
                b.HasOne("rh_admin.Models.Funcionario", "Lider")
                    .WithMany()
                    .HasForeignKey("LiderNumeroChapa");

                b.Navigation("Lider");
            });

            modelBuilder.Entity("rh_admin.Models.Telefone", b =>
            {
                b.HasOne("rh_admin.Models.Funcionario", "Funcionario")
                    .WithMany("Telefones")
                    .HasForeignKey("FuncionarioNumeroChapa")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("Funcionario");
            });

            modelBuilder.Entity("rh_admin.Models.Funcionario", b => { b.Navigation("Telefones"); });
#pragma warning restore 612, 618
        }
    }
}