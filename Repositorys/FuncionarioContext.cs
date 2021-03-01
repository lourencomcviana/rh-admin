using System;
using Microsoft.EntityFrameworkCore;
using rh_admin.Models;

namespace rh_admin.Repositorys
{

    public class FuncionarioContext : DbContext
    {
        public FuncionarioContext()
        {
        }
        public FuncionarioContext(DbContextOptions<FuncionarioContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            Telefone telefone = new Telefone();
            telefone.Numero = "9988623028";
            //GUESTS
            modelBuilder.Entity<Funcionario>().HasData(new Funcionario()
            {
                NumeroChapa = "11",
                Nome = "teste",
                Email = "teste@gmail.com",
                Sobrenome = "Testoso",
                DataCadastro = DateTime.Now
            });
 
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
    }
    
}