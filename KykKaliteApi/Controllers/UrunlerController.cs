using KykKaliteApi.Dtos.UrunGruplariDtos;
using KykKaliteApi.Dtos.UrunlerDtos;
using KykKaliteApi.Repositories.UrunGruplariRepository;
using KykKaliteApi.Repositories.UrunlerRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunlerController : ControllerBase
    {
        private readonly IUrunlerRepository _urunlerRepository;
        public UrunlerController(IUrunlerRepository urunlerRepository)
        {
            _urunlerRepository = urunlerRepository;
        }
        [HttpGet]
        public async Task<IActionResult> UrunlerList()
        {
            var values = await _urunlerRepository.GetAllUrunlerAsync();
            return Ok(values);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUrunler(UpdateUrunlerDto updateUrunlerDto)
        {
            _urunlerRepository.UpdateUrunler(updateUrunlerDto);
            return Ok("Urun Başarıyla Güncellendi");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUrunler(CreateUrunlerDto createUrunlerDto)
        {
            _urunlerRepository.CreateUrunler(createUrunlerDto);
            return Ok("Ürün Başarılı Bir Şekilde Eklendi");
        }
        [HttpGet("{urunId,malzemeAciklamasi}")]
        public async Task<IActionResult> GetUpatamaKodlariByUrunIDA(int urunId, string malzemeAciklamasi)
        {
            var value = await _urunlerRepository.GetUrunlerByMalzemeAciklamasi(urunId, malzemeAciklamasi);
            return Ok(value);
        }
    }
}
