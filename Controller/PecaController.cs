using Microsoft.AspNetCore.Mvc;
using NF.DTOs.Peca;
using NF.Services.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PecaController : ControllerBase
{
    private readonly IClienteService _clienteService;
    private readonly IPecaService _service;

    public PecaController(IPecaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var pecas = await _service.GetAll();
        return Ok(pecas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var peca = await _service.GetById(id);
        if (peca == null) return NotFound("Peça não encontrada");
        return Ok(peca);
    }

    [HttpGet("codigo/{cod}")]
    public async Task<IActionResult> GetByCode(string cod)
    {
        var peca = await _service.GetByCod(cod);
        if (peca == null) return NotFound("Peça não encontrada");
        return Ok(peca);
    }

    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> GetByNome(string nome)
    {
        var peca = await _service.GetByNome(nome);
        if (peca == null) return NotFound("Peça não encontrada");
        return Ok(peca);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PecaRequestDTO dto)
    {
        try
        {
            var peca = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = peca.IdPeca }, peca);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PecaRequestDTO dto)
    {
        try
        {
            var peca = await _service.Update(id, dto);
            if (peca == null) return NotFound("Peça não encontrada.");
            return Ok(peca);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound("Peça não encontrada.");
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}