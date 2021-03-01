using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using rh_admin.Dtos;
using rh_admin.Models;

namespace rh_admin.Repositorys
{
    public class FuncionarioRepository : Repository<Funcionario, string>, IFuncionarioRepository
    {
        public FuncionarioRepository(FuncionarioContext repositoryPatternDemoContext) :
            base(repositoryPatternDemoContext)
        {
        }

        public async Task<Funcionario> FindById(string id)
        {
            return await Context.Funcionarios
                .Include(x => x.Telefones)
                .FirstOrDefaultAsync(x => x.NumeroChapa.Equals(id));
        }

        public async Task<List<Funcionario>> Filter(FuncionarioQueryDto funcionarioQueryDto)
        {
            return await Context.Funcionarios
                .Include(x => x.Telefones)
                .Where(item =>
                    funcionarioQueryDto.NumeroChapa == null || funcionarioQueryDto.NumeroChapa.Equals(item.NumeroChapa))
                .Where(item => funcionarioQueryDto.Nome == null || item.Nome.Contains(funcionarioQueryDto.Nome))
                .Where(item =>
                    funcionarioQueryDto.Sobrenome == null || item.Sobrenome.Contains(funcionarioQueryDto.Sobrenome))
                .Where(item => funcionarioQueryDto.Email == null || funcionarioQueryDto.Email.Equals(item.Email))
                .Where(item =>
                    funcionarioQueryDto.Lider == null ||
                    item.Lider != null && funcionarioQueryDto.Lider.Equals(item.Lider.NumeroChapa))
                .ToListAsync();
        }
    }

    public interface IFuncionarioRepository : IRepository<Funcionario, string>
    {
        public Task<List<Funcionario>> Filter(FuncionarioQueryDto funcionarioQueryDto);
    }
}