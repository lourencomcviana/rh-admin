using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rh_admin.Dtos;
using rh_admin.Exceptions;
using rh_admin.Services;

namespace rh_admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;
        private readonly ILogger<FuncionarioController> _logger;

        public FuncionarioController(ILogger<FuncionarioController> logger, FuncionarioService service)
        {
            _logger = logger;
            _funcionarioService = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FuncionarioRetornoDto>>> Get([FromQuery] FuncionarioQueryDto funcionarioDto)
        {
            var list = (await _funcionarioService.Filter(funcionarioDto))
                .Select(item => _funcionarioService.Mapper.Map<FuncionarioRetornoDto>(item))
                .ToList();

            if (list.Count == 0) return NoContent();

            return list;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioRetornoDto>> GetOne(string id)
        {
            var funcionarioRetorno = _funcionarioService
                .Mapper.Map<FuncionarioRetornoDto>(
                    await _funcionarioService.Get(id)
                );

            if (funcionarioRetorno == null) return NotFound();

            return funcionarioRetorno;
        }

        [HttpPost]
        public async Task<ActionResult<FuncionarioRetornoDto>> PostFuncionario(FuncionarioCreateDto funcionarioDto)
        {
            try
            {
                var funcionarioRetorno = _funcionarioService
                    .Mapper.Map<FuncionarioRetornoDto>(
                        await _funcionarioService.Create(funcionarioDto)
                    );
                return CreatedAtAction(nameof(GetOne), new {id = funcionarioRetorno.NumeroChapa}, funcionarioRetorno);
            }
            catch (ExistsOrNotException e)
            {
                
                _logger.LogError(e,"não foi possivel realizar o cadastro");
                return BadRequest(new ErrorDto(){Message = e.Message});
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutFuncionario(FuncionarioDto funcionarioDto)
        {
            try
            {
                await _funcionarioService.Update(funcionarioDto);
                return Ok();
            }
            catch (ExistsOrNotException e)
            {
                _logger.LogError(e,"não foi possivel realizar o a atualização");
                return BadRequest(new ErrorDto(){Message = e.Message});
            }
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteFuncionario(string key)
        {
            var result = await _funcionarioService.Delete(key);
            if (!result) return NotFound();
            return Ok();
        }
    }
}