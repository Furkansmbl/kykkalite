using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.HammaddelerDtos;
using KykKaliteApi.Dtos.UrunlerDtos;
using KykKaliteApi.Repositories.FabrikalarRepository;
using KykKaliteApi.Repositories.HammaddelerRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HammaddelerController : ControllerBase
    {
        private readonly IHammaddelerRepository _hammaddelerRepository;
        public HammaddelerController(IHammaddelerRepository hammaddelerRepository )
        {
            _hammaddelerRepository = hammaddelerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> HammaddelerList()
        {
            var values = await _hammaddelerRepository.GetAllHammaddelerAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateHammaddeler(CreateHammaddelerDto createHammaddelerDto)
        {
            _hammaddelerRepository.CreateHammaddeler(createHammaddelerDto);
            return Ok("Hammadde Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateHammaddeler(UpdateHammaddelerDto updateHammaddelerDto)
        {
            _hammaddelerRepository.UpdateHammadde(updateHammaddelerDto);
            return Ok("Hammadde Başarıyla Güncellendi");
        }
    }
}
