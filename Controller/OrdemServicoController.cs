using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.DTOs.OrdemServico;
using NF.Models;
using NF.Repositories.Interfaces;
using NF.Services.Interfaces;

namespace NF.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemServicoController : ControllerBase
    {
        private readonly IOrdemServicoService _service;
        //private readonly IOrdemServicoPecaService _OsPecaService;

        public OrdemServicoController(IOrdemServicoService service)
        {
            _service = service;
            //_OsPecaService = OsPecaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var os =  await _service.GetAll();
            return Ok(os);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var os = await _service.GetById(id);
            if (os == null) return NotFound("Peça não encontrada");
            return Ok(os);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComPeca(OrdemServicoRequestDTO dto)
        {
            try
            {
                var os = await _service.CreateComPeca(dto);
                return CreatedAtAction(nameof(GetById), new { id = os.IdOs }, os);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public async Task<IActionResult> GetByCliente()
        //{

        //}

        //public async Task<IActionResult> Update()
        //{

        //}

        //public async Task<IActionResult> Delete(int id)
        //{
        //    var ordem = await _repository.GetById(id);
        //    if (ordem == null) return false;

        //    if (ordem.Status != Status.Aberto)
        //        throw new Exception("Só é possível excluir uma OS com status Aberto.");

        //    await _repository.Delete(id);
        //    return true;
        //}




    }
}
