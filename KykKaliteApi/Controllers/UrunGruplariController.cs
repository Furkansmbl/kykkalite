using KykKaliteApi.Dtos.UretimHatlariDtos;
using KykKaliteApi.Dtos.UrunGruplariDtos;
using KykKaliteApi.Repositories.HMPNvalueRepository;
using KykKaliteApi.Repositories.UPNvalueRepository;
using KykKaliteApi.Repositories.UrunGruplariRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunGruplariController : ControllerBase
    {
        private readonly IUrunGruplariRepository _urunGruplariRepository;
        public UrunGruplariController(IUrunGruplariRepository urunGruplariRepository)
        {
            _urunGruplariRepository = urunGruplariRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UrunGruplariList()
        {
            var values = await _urunGruplariRepository.GetAllUrunGruplariAsync();
            return Ok(values);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUrunGruplari(int id)
        {
            _urunGruplariRepository.DeleteUrunGruplari(id);
            return Ok("Ürün grubu Başarılı Bir Şekilde Silindi");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUrunGruplari(UpdateUrunGruplariDto updateUrunGruplariDto)
        {
            _urunGruplariRepository.UpdateUrunGruplari(updateUrunGruplariDto);
            return Ok("Urun Grubu Başarıyla Güncellendi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrunGruplari(CreateUrunGruplariDto createUrunGruplariDto )
        {
            _urunGruplariRepository.CreateUrunGruplari(createUrunGruplariDto );
            return Ok("Ürün grubu Başarılı Bir Şekilde Eklendi");
        }
    }
}
