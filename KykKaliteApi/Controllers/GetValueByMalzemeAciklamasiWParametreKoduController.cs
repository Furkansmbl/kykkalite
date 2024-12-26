using KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository;
using KykKaliteApi.Repositories.GetValueByMalzemeAciklamasiWParametreKodu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetValueByMalzemeAciklamasiWParametreKoduController : ControllerBase
    {
        private readonly IGetValueRepository _getValueRepository ;

        public GetValueByMalzemeAciklamasiWParametreKoduController(IGetValueRepository getValueRepository )
        {
            _getValueRepository = getValueRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUpatamaKodlariByUrunIDA([FromQuery] string malzemeaciklamasi,[FromQuery] string kontrolparametresi, [FromQuery] string baslangicTarihi, [FromQuery] string bitisTarihi, [FromQuery] int fabrikaId)
        {
            var value = await _getValueRepository.GetValueByMalzemeAciklamasiWParametreKoduAsync(malzemeaciklamasi,  kontrolparametresi, baslangicTarihi, bitisTarihi, fabrikaId);
            return Ok(value);
        }
        [HttpGet("hm")]
        public async Task<IActionResult> GetValueByMalzemeAciklamasiWParametreKoduHammaddeAsync([FromQuery] string malzemeaciklamasi, [FromQuery] string kontrolparametresi, [FromQuery] string baslangicTarihi, [FromQuery] string bitisTarihi, [FromQuery] string UNVANI)
        {
            var value = await _getValueRepository.GetValueByMalzemeAciklamasiWParametreKoduHammaddeAsync(malzemeaciklamasi, kontrolparametresi, baslangicTarihi, bitisTarihi, UNVANI);
            return Ok(value);
        }
        [HttpGet("Pa")]
        public async Task<IActionResult> GetKontrolParametreleri()
        {
          
            var value = await _getValueRepository.GetKontrolParametresi();
            return Ok(value);
        }
        [HttpGet("Hmk")]
        public async Task<IActionResult> GetKontrolParametreleriHammadde()
        {
            var value = await _getValueRepository.GetKontrolParametresiHammadde();
            return Ok(value);
        }
    }
}
