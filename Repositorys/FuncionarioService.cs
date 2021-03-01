
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using rh_admin.Exceptions;
using rh_admin.Models;

namespace rh_admin.Repositorys
{
    public class FuncionarioService 
    {
        private readonly IFuncionarioRepository _repository;
        private readonly ITelefoneRepository _telefoneRepository;

        public FuncionarioService(IFuncionarioRepository repository, ITelefoneRepository telefoneRepository)
        {
            _repository = repository;
            _telefoneRepository = telefoneRepository;
        }

        private MapperConfiguration mapperConfiguration = MapperConfigurationFactory();
        public Mapper Mapper
        {
            get
            {
                Mapper mapper = new Mapper(mapperConfiguration);
                return mapper;
            }
        }

        public async Task<Funcionario> Get(String numero)
        {
            return await _repository.FindById(numero);
        }

        public async Task<List<Funcionario>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async  Task<Funcionario> Create<T> (T funcionarioDto)
            where T : FuncionarioDto
        {
            if (await _repository.ExistsAsync(funcionarioDto.NumeroChapa))
            {
                throw  new ExistsOrNotException($"{funcionarioDto.NumeroChapa} já exisete");
            }

            var funcionario = Mapper.Map<Funcionario>(funcionarioDto);
            DesagregarTelefones(funcionario.Telefones);

            funcionario.DataCadastro = DateTime.Now;
            await _repository.CreateAsync(funcionario);

            return funcionario;
        }
        
        public async  Task<Funcionario> Update<T> (T funcionarioDto)
            where T : FuncionarioDto
        {
            if (!await _repository.ExistsAsync(funcionarioDto.NumeroChapa))
            {
                throw  new ExistsOrNotException($"{funcionarioDto.NumeroChapa} não exisete");
            }
            // Antes de recuperar funcionário e realizar mapeamento!
            DesagregarTelefones(funcionarioDto.Telefone);
            
            Funcionario funcionarioExistente = await Get(funcionarioDto.NumeroChapa);
            Funcionario funcionario= Mapper.Map<FuncionarioDto,Funcionario>(funcionarioDto,funcionarioExistente);
            
            await _repository.UpdateAsync(funcionario);
            return funcionario;
        }
        

        public async Task<Boolean> Delete(String key)
        {
            Funcionario funcionario = await Get(key);
            if (funcionario == null)
            {
                return false;
            }

            await _repository.DeleteAsync(key);

            return true;
        }

        private void DesagregarTelefones(IEnumerable<Telefone> telefones)
        {
            telefones
                .ForAll(
                    async telefone =>
                        await  _telefoneRepository.DeleteAsync(telefone.Numero)
                    );
        }
        private void DesagregarTelefones(List<String> telefones)
        {
            telefones
                .ForAll(
                    async telefone =>
                        await  _telefoneRepository.DeleteAsync(telefone)
                );
        }
        
        public static void AddMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Funcionario,FuncionarioDto>();
            cfg.CreateMap<FuncionarioDto, Funcionario>()
                .ForMember(
                dest => dest.DataCadastro,
                    opt =>
                    {
                        opt.MapFrom(item => DateTime.Now);
                    })
                .ForMember(
                    dest => dest.Telefones,
                    opt =>
                    {
                        opt.MapFrom((item,funcionario) =>
                            item.Telefone
                                .Select(numero => new Telefone(){ Numero = numero, Funcionario = funcionario})
                                .ToList()
                            );
                    });

            cfg.CreateMap<Funcionario, FuncionarioRetornoDto>()
                .IncludeBase<Funcionario, FuncionarioDto>();
        }
        
        private static MapperConfiguration MapperConfigurationFactory()
        {
            return new MapperConfiguration(AddMappings);
        }
    }
    
}