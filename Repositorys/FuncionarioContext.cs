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
        

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
    }
    
}