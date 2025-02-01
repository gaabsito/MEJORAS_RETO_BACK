using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CineAPI.Repository;

namespace CineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ButacaController : ControllerBase
    {
        private readonly IButacaRepository _butacaRepository;

        public ButacaController(IButacaRepository butacaRepository)
        {
            _butacaRepository = butacaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Butaca>>> GetAll()
        {
            return Ok(await _butacaRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Butaca>> GetById(int id)
        {
            var butaca = await _butacaRepository.GetByIdAsync(id);
            if (butaca == null) return NotFound();
            return Ok(butaca);
        }

        [HttpGet("sala/{salaId}")]
        public async Task<ActionResult<List<Butaca>>> GetBySala(int salaId)
        {
            return Ok(await _butacaRepository.GetBySalaAsync(salaId));
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Butaca butaca)
        {
            var id = await _butacaRepository.AddAsync(butaca);
            return CreatedAtAction(nameof(GetById), new { id }, butaca);
        }

        [HttpPut("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] string estado)
        {
            var updated = await _butacaRepository.UpdateEstadoAsync(id, estado);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _butacaRepository.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
