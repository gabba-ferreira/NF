using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NF.DTOs.User;
using NF.Models;
using NF.Services.Interfaces;

namespace NF.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var users = await _service.GetAll();
            return Ok(users);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var user = await _service.GetById(id);
            if (user == null) return NotFound("Usuário nao encontrado ");
            return Ok(user);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserRequestDTO dto)
        {
            try
            {
                var user = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = user.IdUser }, user);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserRequestDTO dto)
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
