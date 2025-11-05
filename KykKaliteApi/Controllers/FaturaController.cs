using HalApi.Dtos;
using HalApi.Repositories.FaturaRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaturaController : ControllerBase
    {
        private readonly IFaturaRepository _faturaRepository;

        public FaturaController(IFaturaRepository faturaRepository)
        {
            _faturaRepository = faturaRepository;
        }

        // GET: api/Fatura
        [HttpGet]
        public async Task<ActionResult<List<Fatura>>> GetAll()
        {
            var result = await _faturaRepository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Fatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetById(int id)
        {
            var result = await _faturaRepository.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        // POST: api/Fatura
        [HttpPost]
        public async Task<ActionResult> Create(Fatura fatura)
        {
            await _faturaRepository.CreateAsync(fatura);
            return CreatedAtAction(nameof(GetById), new { id = fatura.FaturaID }, fatura);
        }

        // PUT: api/Fatura/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Fatura fatura)
        {
            if (id != fatura.FaturaID)
                return BadRequest("Fatura ID mismatch.");

            await _faturaRepository.UpdateAsync(fatura);
            return NoContent();
        }

        // DELETE: api/Fatura/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _faturaRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
