using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.DTOs.Cliente;
using NF.DTOs.User;
using NF.Services.Interfaces;

namespace NF.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClienteController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var cliente = await _service.GetAll();
            return Ok(cliente);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var cliente = await _service.GetById(id);
            if (cliente == null) return NotFound("Usuário nao encontrado ");
            return Ok(cliente);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteRequestDTO dto)
        {
            try
            {
                var cliente = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = cliente.IdCliente }, cliente);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClienteRequestDTO dto)
        {
            try
            {
                var user = await _service.Update(id, dto);
                if (user == null) return NotFound("Usuário não encontrado.");
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
