using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using rh_admin.Dtos;
using rh_admin.Exceptions;
using rh_admin.Models;
using rh_admin.Repositorys;

namespace rh_admin.Services
{
    public class FuncionarioService
    {
        private readonly IFuncionarioRepository _repository;
        private readonly ITelefoneRepository _telefoneRepository;

        private readonly MapperConfiguration mapperConfiguration = MapperConfigurationFactory();

        public FuncionarioService(IFuncionarioRepository repository, ITelefoneRepository telefoneRepository)
        {
            _repository = repository;
            _telefoneRepository = telefoneRepository;
        }

        public Mapper Mapper
        {
            get
            {
                var mapper = new Mapper(mapperConfiguration);
                return mapper;
            }
        }

        public async Task<Funcionario> Get(string numero)
        {
            return await _repository.FindById(numero);
        }

        public async Task<List<Funcionario>> FindAll()
        {
            return await _repository.FindAll();
        }

        public async Task<List<Funcionario>> Filter(FuncionarioQueryDto funcionarioQueryDto)
        {
            return await _repository.Filter(funcionarioQueryDto);
        }

        public async Task<Funcionario> Create(FuncionarioCreateDto funcionarioDto)
        {
            if (await _repository.ExistsAsync(funcionarioDto.NumeroChapa))
                throw new ExistsOrNotException($"{funcionarioDto.NumeroChapa} já exisete");


            var funcionario = Mapper.Map<Funcionario>(funcionarioDto);

            await AssociarLider(funcionario);
            DesagregarTelefones(funcionario.Telefones);

            await _repository.CreateAsync(funcionario);

            return funcionario;
        }

        public async Task<Funcionario> Update<T>(T funcionarioDto)
            where T : FuncionarioDto
        {
            if (!await _repository.ExistsAsync(funcionarioDto.NumeroChapa))
                throw new ExistsOrNotException($"{funcionarioDto.NumeroChapa} não exisete");
            // Antes de recuperar funcionário e realizar mapeamento!
            DesagregarTelefones(funcionarioDto.Telefone);

            var funcionarioExistente = await Get(funcionarioDto.NumeroChapa);
            var funcionario = Mapper.Map<FuncionarioDto, Funcionario>(funcionarioDto, funcionarioExistente);
            await AssociarLider(funcionario);
            await _repository.UpdateAsync(funcionario);
            return funcionario;
        }


        public async Task<bool> Delete(string key)
        {
            var funcionario = await Get(key);
            if (funcionario == null) return false;

            DesassociarSuditos(key);

            await _repository.DeleteAsync(funcionario);

            return true;
        }

        private async Task AssociarLider(Funcionario funcionario)
        {
            if (funcionario.Lider != null)
            {
                var lider = await _repository.FindById(funcionario.Lider.NumeroChapa);
                if (lider == null) throw new ExistsOrNotException($"líder {funcionario.Lider.NumeroChapa} não existe");

                funcionario.Lider = lider;
            }
        }

        private async void DesassociarSuditos(string key)
        {
            var suditos = await _repository.Filter(
                new FuncionarioQueryDto {Lider = key}
            );

            if (suditos.Count > 0)
                // desassociar todos os suditos
                suditos.ForAll(async item =>
                {
                    item.Lider = null;
                    await _repository.UpdateAsync(item);
                });
        }

        private void DesagregarTelefones(IEnumerable<Telefone> telefones)
        {
            telefones
                .ForAll(
                    async telefone =>
                        await _telefoneRepository.DeleteAsync(telefone.Numero)
                );
        }

        private void DesagregarTelefones(List<string> telefones)
        {
            telefones
                .ForAll(
                    async telefone =>
                        await _telefoneRepository.DeleteAsync(telefone)
                );
        }

        public static void AddMappings(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Funcionario, FuncionarioDto>()
                .ForMember(
                    dest => dest.Lider,
                    opt =>
                    {
                        opt.MapFrom((item, funcionario) =>
                        {
                            if (item.Lider != null) return item.Lider.NumeroChapa;

                            return null;
                        });
                    })
                ;

            cfg.CreateMap<Funcionario, FuncionarioRetornoDto>()
                .IncludeBase<Funcionario, FuncionarioDto>()
                ;

            cfg.CreateMap<FuncionarioDto, Funcionario>()
                .ForMember(
                    dest => dest.DataCadastro,
                    opt => { opt.MapFrom(item => DateTime.Now); })
                .ForMember(
                    dest => dest.Telefones,
                    opt =>
                    {
                        opt.MapFrom((item, funcionario) =>
                            item.Telefone
                                .Select(numero => new Telefone {Numero = numero, Funcionario = funcionario})
                                .ToList()
                        );
                    })
                .ForMember(
                    dest => dest.Lider,
                    opt =>
                    {
                        opt.MapFrom((item, funcionario) =>
                        {
                            if (item.Lider == null) return null;
                            return new Funcionario
                            {
                                NumeroChapa = item.Lider
                            };
                        });
                    })
                ;

            cfg.CreateMap<FuncionarioCreateDto, Funcionario>()
                .IncludeBase<FuncionarioDto, Funcionario>()
                .AfterMap((funcionarioDto, funcionario) =>
                {
                    var hashSalt = Util.hashPassword(funcionarioDto.Senha);

                    // sets especificios para o cadastro
                    funcionario.Senha = hashSalt.Hash;
                    funcionario.Salt = hashSalt.Salt;
                    funcionario.DataCadastro = DateTime.Now;
                });
        }

        private static MapperConfiguration MapperConfigurationFactory()
        {
            return new MapperConfiguration(AddMappings);
        }
    }
}