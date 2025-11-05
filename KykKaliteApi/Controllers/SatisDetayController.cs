using HalApi.Dtos;
using HalApi.Repositories.SatisDetayRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatisDetayController : ControllerBase
    {
        private readonly ISatisDetayRepository _repository;

        public SatisDetayController(ISatisDetayRepository repository)
        {
            _repository = repository;
        }

        // GET: api/SatisDetay
        [HttpGet]
        public async Task<ActionResult<List<SatisDetay>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/SatisDetay/5
        [HttpGet("{detayId}")]
        public async Task<ActionResult<SatisDetay>> GetById(int detayId)
        {
            var result = await _repository.GetByIdAsync(detayId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/SatisDetay/bySatis/3
        [HttpGet("bySatis/{satisId}")]
        public async Task<ActionResult<List<SatisDetay>>> GetBySatisId(int satisId)
        {
            var result = await _repository.GetBySatisIdAsync(satisId);
            return Ok(result);
        }

        // POST: api/SatisDetay
        [HttpPost]
        public async Task<ActionResult<int>> Create(SatisDetay satisDetay)
        {
            var id = await _repository.CreateAsync(satisDetay);
            return CreatedAtAction(nameof(GetById), new { detayId = id }, satisDetay);
        }

        // PUT: api/SatisDetay/5
        [HttpPut("{detayId}")]
        public async Task<ActionResult> Update(int detayId, SatisDetay satisDetay)
        {
            if (detayId != satisDetay.DetayID) return BadRequest("DetayID mismatch");

            var updated = await _repository.UpdateAsync(satisDetay);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/SatisDetay/5
        [HttpDelete("{detayId}")]
        public async Task<ActionResult> Delete(int detayId)
        {
            var deleted = await _repository.DeleteAsync(detayId);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
