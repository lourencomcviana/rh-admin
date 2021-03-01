using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using rh_admin.Exceptions;
using rh_admin.Models;
using rh_admin.Repositorys;

namespace rh_admin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly ILogger<FuncionarioController> _logger;
        private readonly FuncionarioService _funcionarioService;
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

            if (list.Count == 0)
            {
                return NoContent();
            }

            return list;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<FuncionarioRetornoDto>> GetOne(String id)
        {
            FuncionarioRetornoDto funcionarioRetorno = _funcionarioService
                .Mapper.Map<FuncionarioRetornoDto>(
                    await _funcionarioService.Get(id)
                );

            if (funcionarioRetorno == null)
            {
                return NotFound();
            }
            
            return funcionarioRetorno;
        }
        
        [HttpPost]
        public async Task<ActionResult<FuncionarioRetornoDto>> PostFuncionario(FuncionarioDto funcionarioDto)
        {
            try
            {
                var funcionarioRetorno = _funcionarioService
                    .Mapper.Map<FuncionarioRetornoDto>(
                        await _funcionarioService.Create(funcionarioDto)
                    );
                return CreatedAtAction(nameof(GetOne), new { id = funcionarioRetorno.NumeroChapa }, funcionarioRetorno);
            }
            catch (ExistsOrNotException e)
            {
                return BadRequest(e.Message);
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
                return BadRequest(e.Message);
            }
        }
        
        [HttpDelete("{key}")]
        public async Task<IActionResult> DeleteFuncionario(String key)
        {
            Boolean result = await _funcionarioService.Delete(key);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}