using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.DTOs.Cliente;
using NF.Services.Interfaces;

namespace NF.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _service;

        public VeiculoController(IVeiculoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var veiculos = await _service.GetAll();
            return Ok(veiculos);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var veiculo = await _service.GetById(id);
            if (veiculo == null) return NotFound("Veiculo não encontrado ");
            return Ok(veiculo);

        }

        [HttpGet("Cliente/{id}")]
        public async Task<IActionResult> GetByIdCliente(int id)
        {

            var veiculos = await _service.GetByCliente(id);
            if (veiculos == null) return NotFound("Esse cliente não tem veiculos vinculados");
            return Ok(veiculos);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VeiculoRequestDTO dto)
        {
            try
            {
                var veiculo = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = veiculo.IdVeiculo }, veiculo);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, VeiculoRequestDTO dto)
        {
            try
            {
                var user = await _service.Update(id, dto);
                if (user == null) return NotFound("Veículo não encontrado.");
                return Ok(user);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound("Usuário não encontrado.");
            return NoContent();
        }
    }
}
