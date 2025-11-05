using HalApi.Dtos;
using HalApi.Repositories.StokHareketRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokHareketController : ControllerBase
    {
        private readonly IStokHareketRepository _repository;

        public StokHareketController(IStokHareketRepository repository)
        {
            _repository = repository;
        }

        // GET: api/StokHareket
        [HttpGet]
        public async Task<ActionResult<List<StokHareket>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/StokHareket/5
        [HttpGet("{hareketId}")]
        public async Task<ActionResult<StokHareket>> GetById(int hareketId)
        {
            var result = await _repository.GetByIdAsync(hareketId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/StokHareket/byUrun/3
        [HttpGet("byUrun/{urunId}")]
        public async Task<ActionResult<List<StokHareket>>> GetByUrunId(int urunId)
        {
            var result = await _repository.GetByUrunIdAsync(urunId);
            return Ok(result);
        }

        // POST: api/StokHareket
        [HttpPost]
        public async Task<ActionResult<int>> Create(StokHareket stokHareket)
        {
            var id = await _repository.CreateAsync(stokHareket);
            return CreatedAtAction(nameof(GetById), new { hareketId = id }, stokHareket);
        }

        // PUT: api/StokHareket/5
        [HttpPut("{hareketId}")]
        public async Task<ActionResult> Update(int hareketId, StokHareket stokHareket)
        {
            if (hareketId != stokHareket.HareketID) return BadRequest("HareketID mismatch");

            var updated = await _repository.UpdateAsync(stokHareket);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/StokHareket/5
        [HttpDelete("{hareketId}")]
        public async Task<ActionResult> Delete(int hareketId)
        {
            var deleted = await _repository.DeleteAsync(hareketId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
