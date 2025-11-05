using HalApi.Dtos;
using HalApi.Repositories.KasaTipiRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KasaTipiController : ControllerBase
    {
        private readonly IKasaTipiRepository _repository;

        public KasaTipiController(IKasaTipiRepository repository)
        {
            _repository = repository;
        }

        // GET: api/KasaTipi
        [HttpGet]
        public async Task<ActionResult<List<KasaTipi>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/KasaTipi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KasaTipi>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/KasaTipi
        [HttpPost]
        public async Task<ActionResult<int>> Create(KasaTipi kasaTipi)
        {
            var id = await _repository.CreateAsync(kasaTipi);
            return CreatedAtAction(nameof(GetById), new { id = id }, kasaTipi);
        }

        // PUT: api/KasaTipi/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, KasaTipi kasaTipi)
        {
            if (id != kasaTipi.KasaTipID) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(kasaTipi);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/KasaTipi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
