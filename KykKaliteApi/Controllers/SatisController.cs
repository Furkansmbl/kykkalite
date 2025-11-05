using HalApi.Dtos;
using HalApi.Repositories.SatisRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatisController : ControllerBase
    {
        private readonly ISatisRepository _repository;

        public SatisController(ISatisRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Satis
        [HttpGet]
        public async Task<ActionResult<List<Satis>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Satis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Satis>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/Satis/byCari/3
        [HttpGet("byCari/{cariId}")]
        public async Task<ActionResult<List<Satis>>> GetByCariId(int cariId)
        {
            var result = await _repository.GetByCariIdAsync(cariId);
            return Ok(result);
        }

        // POST: api/Satis
        [HttpPost]
        public async Task<ActionResult<int>> Create(Satis satis)
        {
            var id = await _repository.CreateAsync(satis);
            return CreatedAtAction(nameof(GetById), new { id = id }, satis);
        }

        // PUT: api/Satis/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Satis satis)
        {
            if (id != satis.SatisID) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(satis);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/Satis/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
