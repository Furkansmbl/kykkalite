using HalApi.Dtos;
using HalApi.Repositories.AlimDetayRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimDetayController : ControllerBase
    {
        private readonly IAlimDetayRepository _repository;

        public AlimDetayController(IAlimDetayRepository repository)
        {
            _repository = repository;
        }

        // GET: api/AlimDetay
        [HttpGet]
        public async Task<ActionResult<List<AlimDetay>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/AlimDetay/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlimDetay>> GetById(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // GET: api/AlimDetay/byAlim/3
        [HttpGet("byAlim/{alimId}")]
        public async Task<ActionResult<List<AlimDetay>>> GetByAlimId(int alimId)
        {
            var result = await _repository.GetByAlimIdAsync(alimId);
            return Ok(result);
        }

        // POST: api/AlimDetay
        [HttpPost]
        public async Task<ActionResult<int>> Create(AlimDetay detay)
        {
            var id = await _repository.CreateAsync(detay);
            return CreatedAtAction(nameof(GetById), new { id = id }, detay);
        }

        // PUT: api/AlimDetay/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AlimDetay detay)
        {
            if (id != detay.DetayID) return BadRequest("ID mismatch");

            var updated = await _repository.UpdateAsync(detay);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/AlimDetay/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
