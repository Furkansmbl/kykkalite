using KykKaliteApi.Dtos.CihazlarDtos;
using KykKaliteApi.Dtos.FabrikalarDtos;
using KykKaliteApi.Dtos.KullaniciDtos;
using KykKaliteApi.Repositories.CihazlarRepository;
using KykKaliteApi.Repositories.FabrikalarRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CihazlarController : ControllerBase
    {
        private readonly ICihazlarRepository _cihazlarRepository ;
        public CihazlarController(ICihazlarRepository cihazlarRepository )
        {
            _cihazlarRepository = cihazlarRepository;
        }
        [HttpGet]
        public async Task<IActionResult> CihazlarList()
        {
            var values = await _cihazlarRepository.GetAllCihazlarAsync();
            return Ok(values);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCihazlar(CreateCihazlarDto createCihazlarDto)
        {
            _cihazlarRepository.CreateCihazlar(createCihazlarDto);
            return Ok("Cihaz Başarılı Bir Şekilde Eklendi");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCihazlar(int id)
        {
            _cihazlarRepository.DeleteCihazlar(id);
            return Ok("Cihaz Başarılı Bir Şekilde Silindi");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCihazlar(int id)
        {
            var value = await _cihazlarRepository.GetCihazlar(id); 
            return Ok(value);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCihazlar(UpdateCihazlarDto updateCihazlarDto )
        {
            _cihazlarRepository.UpdateCihazlar(updateCihazlarDto);
            return Ok("Cihaz Başarıyla Güncellendi");
        }
    }
}
