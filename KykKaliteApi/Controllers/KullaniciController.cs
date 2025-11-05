using HalApi.Dtos;
using HalApi.Repositories.KullaniciRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciRepository _repository;

        public KullaniciController(IKullaniciRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Kullanici
        [HttpGet]
        public async Task<ActionResult<List<Kullanici>>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Kullanici/{sicilNo}
        [HttpGet("{sicilNo}")]
        public async Task<ActionResult<Kullanici>> GetById(string sicilNo)
        {
            var result = await _repository.GetByIdAsync(sicilNo);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Kullanici
        [HttpPost]
        public async Task<ActionResult> Create(Kullanici kullanici)
        {
            await _repository.CreateAsync(kullanici);
            return CreatedAtAction(nameof(GetById), new { sicilNo = kullanici.PersonelSicilNo }, kullanici);
        }

        // PUT: api/Kullanici/{sicilNo}
        [HttpPut("{sicilNo}")]
        public async Task<ActionResult> Update(string sicilNo, Kullanici kullanici)
        {
            if (sicilNo != kullanici.PersonelSicilNo) return BadRequest("SicilNo mismatch");

            var updated = await _repository.UpdateAsync(kullanici);
            if (!updated) return NotFound();

            return NoContent();
        }

        // DELETE: api/Kullanici/{sicilNo}
        [HttpDelete("{sicilNo}")]
        public async Task<ActionResult> Delete(string sicilNo)
        {
            var deleted = await _repository.DeleteAsync(sicilNo);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
