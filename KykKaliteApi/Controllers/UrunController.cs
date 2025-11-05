using HalApi.Dtos;
using HalApi.Repositories.UrunRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly IUrunRepository _repository;

        public UrunController(IUrunRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Urun
        [HttpGet]
        public async Task<ActionResult<List<Urun>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Urun/5
        [HttpGet("{urunId}")]
        public async Task<ActionResult<Urun>> GetById(int urunId)
        {
            var result = await _repository.GetByIdAsync(urunId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Urun
        [HttpPost]
        public async Task<ActionResult<int>> Create(Urun urun)
        {
            var id = await _repository.CreateAsync(urun);
            return CreatedAtAction(nameof(GetById), new { urunId = id }, urun);
        }

        // PUT: api/Urun/5
        [HttpPut("{urunId}")]
        public async Task<ActionResult> Update(int urunId, Urun urun)
        {
            if (urunId != urun.UrunID) return BadRequest("UrunID mismatch");

            var updated = await _repository.UpdateAsync(urun);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/Urun/5
        [HttpDelete("{urunId}")]
        public async Task<ActionResult> Delete(int urunId)
        {
            var deleted = await _repository.DeleteAsync(urunId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
