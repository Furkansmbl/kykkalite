using HalApi.Dtos;
using HalApi.Repositories.CariRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CariController : ControllerBase
    {
        private readonly ICariRepository _repository;

        public CariController(ICariRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Cari
        [HttpGet]
        public async Task<ActionResult<List<Cari>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Cari/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cari>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Cari
        [HttpPost]
        public async Task<ActionResult<int>> Create(Cari cari)
        {
            var id = await _repository.CreateAsync(cari);
            return CreatedAtAction(nameof(GetById), new { id = id }, cari);
        }

        // PUT: api/Cari/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Cari cari)
        {
            if (id != cari.CariID) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(cari);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/Cari/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
