using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.ParametrelerDtos;
using KykKaliteApi.Repositories.FabrikalarRepository;
using KykKaliteApi.Repositories.ParametrelerRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrelerController : ControllerBase
    {
        private readonly IParametrelerRepository _parametrelerRepository;
        public ParametrelerController(IParametrelerRepository parametrelerRepository )
        {
            _parametrelerRepository = parametrelerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> ParametrelerList()
        {
            var values = await _parametrelerRepository.GetAllParametrelerAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateParametreler(CreateParametrelerDto createParametrelerDto)
        {
            _parametrelerRepository.CreateParametreler(createParametrelerDto);
            return Ok("Parametre Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametreler(int id)
        {
            _parametrelerRepository.DeleteParametreler(id);
            return Ok("Parametre Başarılı Bir Şekilde Silindi");
        }
    }
}
