using HalApi.Dtos;
using HalApi.Repositories.AlimRepository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimController : ControllerBase
    {
        private readonly IAlimRepository _alimRepository;

        public AlimController(IAlimRepository alimRepository)
        {
            _alimRepository = alimRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Alim>>> GetAll()
        {
            var result = await _alimRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alim>> GetById(int id)
        {
            var result = await _alimRepository.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(Alim alim)
        {
            var id = await _alimRepository.CreateAsync(alim);
            return CreatedAtAction(nameof(GetById), new { id = id }, id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Alim alim)
        {
            if (id != alim.AlimID)
                return BadRequest("ID mismatch");

            var updated = await _alimRepository.UpdateAsync(alim);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _alimRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
