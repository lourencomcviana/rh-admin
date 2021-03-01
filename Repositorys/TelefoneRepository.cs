
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using rh_admin.Models;

namespace rh_admin.Repositorys
{
    public class  TelefoneRepository :Repository<Telefone,String>, ITelefoneRepository
    {
        private readonly FuncionarioContext _funcionarioContext;
        public TelefoneRepository(FuncionarioContext context) :
            base(context)
        {
            _funcionarioContext = context;
        }


        private async Task UpdateTelefones(Funcionario funcionario)
        {
            // garantir auto-relacionamento
            funcionario.Telefones.ForAll(telefone => telefone.Funcionario = funcionario);
            
            //atualizar telefones
            List<Telefone> atualizados = funcionario.Telefones
                .Select(async telefone => await FindById(telefone.Numero))
                .Select(t => t.Result)
                .Where(i => i != null)
                .Select(async telefone =>await UpdateFuncionario(telefone,funcionario))
                .Select(t => t.Result)
                .Where(i => i != null)
                .ToList()
                ;
            
            // Telefone não atualizado é um novo telefone
            funcionario.Telefones
                .Where(telefone =>
                    !atualizados.Where(atualizado => atualizado.Numero.Equals(telefone.Numero)).Any())
                .ForAll(async telefone => await CreateAsync(telefone))
                ;

            // remover telefones que não são mais do funcionário
            IEnumerable<Telefone> telefonesDoFuncionario =  await GetAllByNumeroChapa(funcionario.NumeroChapa);

            telefonesDoFuncionario.Where(telefone =>
                    !funcionario.Telefones.Where(telefoneBanco => telefoneBanco.Numero.Equals(telefone.Numero)).Any())
                .ForAll(async telefonePorRemover => await DeleteAsync(telefonePorRemover));

        }

        public async Task<IEnumerable<Telefone>>  GetAllByNumeroChapa(String numeroChapa)
        {
            return await _funcionarioContext.Telefone
                .Where(telelefone => telelefone.Funcionario.NumeroChapa.Equals(numeroChapa))
                .ToListAsync();
        }

        private async Task<Telefone> UpdateFuncionario(Telefone telefone ,Funcionario funcionario)
        {
            telefone.Funcionario = funcionario;
            await UpdateAsync(telefone);

            return telefone;
        }

    }
    
   
    public interface  ITelefoneRepository :IRepository<Telefone,String>
    {
    }

}