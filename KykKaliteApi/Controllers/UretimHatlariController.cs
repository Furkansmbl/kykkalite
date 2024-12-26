using KykKaliteApi.Dtos.UPatamaDtos;
using KykKaliteApi.Dtos.UPNvalueDtos;
using KykKaliteApi.Dtos.UretimHatlariDtos;
using KykKaliteApi.Repositories.HMPNvalueRepository;
using KykKaliteApi.Repositories.UPNvalueRepository;
using KykKaliteApi.Repositories.UretimHatlariRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UretimHatlariController : ControllerBase
    {
        private readonly IUretimHatlariRepository _uretimHatlariRepository;
        public UretimHatlariController(IUretimHatlariRepository uretimHatlariRepository)
        {
            _uretimHatlariRepository = uretimHatlariRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UretimHatlarList()
        {
            var values = await _uretimHatlariRepository.GetAllUretimHatlariAsync();
            return Ok(values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUretimHatlari(int id)
        {
            _uretimHatlariRepository.DeleteUretimHatlari(id);
            return Ok("Üretim Hattı Başarılı Bir Şekilde Silindi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrunHatlari(CreateUretimHatlariDto createUretimHatlariDto)
        {
            _uretimHatlariRepository.CreateUretimHatlari(createUretimHatlariDto);
            return Ok("Üretim Hattı Başarılı Bir Şekilde Eklendi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUrunHatlari(UpdateUretimHatlariDto updateUretimHatlariDto)
        {
            _uretimHatlariRepository.UpdateUretimHatlari(updateUretimHatlariDto);
            return Ok("Uretim Hatti Başarıyla Güncellendi");
        }
    }
}
