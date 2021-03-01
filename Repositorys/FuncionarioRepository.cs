
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rh_admin.Models;

namespace rh_admin.Repositorys
{
    public class  FuncionarioRepository :Repository<Funcionario,String>, IFuncionarioRepository
    {
        public FuncionarioRepository(FuncionarioContext repositoryPatternDemoContext) :
            base(repositoryPatternDemoContext)
        {
            
        }
        
        public async Task<Funcionario> FindById(string id)
        {
            return await base.Context.Funcionarios
                .Include(x =>x.Telefones)
                .FirstOrDefaultAsync(x => x.NumeroChapa.Equals(id));
        }

        public async Task<List<Funcionario>> Filter(FuncionarioQueryDto funcionarioQueryDto)
        {
            return await Context.Funcionarios
                .Include(x => x.Telefones)
                .Where(item => funcionarioQueryDto.NumeroChapa ==null || funcionarioQueryDto.NumeroChapa.Equals(item.NumeroChapa))
                .Where(item => funcionarioQueryDto.Nome ==null || funcionarioQueryDto.Nome.ToLower().Contains(funcionarioQueryDto.Nome.ToLower()))
                .Where(item => funcionarioQueryDto.Sobrenome ==null || funcionarioQueryDto.Sobrenome.ToLower().Contains(funcionarioQueryDto.Sobrenome.ToLower()))
                .Where(item => funcionarioQueryDto.Email ==null || funcionarioQueryDto.Email.Equals(funcionarioQueryDto.Email))
                .Where(item => funcionarioQueryDto.Lider ==null || false)
                .ToListAsync();
        }
        
        // qualquer implementação de consulta que for necessaria
        

        // public async Task<List<T>> GetAll<T>()
        // {
        //     return await _mFuncionarioContext
        //         .Funcionarios
        //         //.Include(x => x.Telefone)
        //         .ToListAsync();
        // }

        // public async Task<List<T>> GetAll<T>()
        // {
        //     return await _myHotelDbContext
        //         .Reservations
        //         .Include(x => x.Room)
        //         .Include(x => x.Guest)
        //         .ProjectTo<T>()
        //         .ToListAsync();
        // }

        // public async Task<IEnumerable<Reservation>> GetAll()
        // {
        //     return await _myHotelDbContext
        //         .Reservations
        //         .Include(x => x.Room)
        //         .Include(x => x.Guest)
        //         .ToListAsync();
        // }

        //public async Task<List<T>> GetAll<T>()
        // {
        //      return await _mFuncionarioContext
        //         .Funcionarios
        //         .ProjectTo<T>(mapperConfiguration)
        //         .ToListAsync();
        // }
        //
        //
        // public async Task<IEnumerable<Funcionario>> GetAll()
        // {
        //     return await _mFuncionarioContext
        //         .Funcionarios
        //         .Include(x => x.Telefones)
        //         .ToListAsync();
        // }
    }
    
    public interface  IFuncionarioRepository :IRepository<Funcionario,String>
    {
        public Task<List<Funcionario>> Filter(FuncionarioQueryDto funcionarioQueryDto);
    }
    
    
    
    
}