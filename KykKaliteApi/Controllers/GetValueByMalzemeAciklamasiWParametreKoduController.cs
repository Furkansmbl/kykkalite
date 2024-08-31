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
        public async Task<IActionResult> GetUpatamaKodlariByUrunIDA([FromQuery] string malzemeaciklamasi,[FromQuery] string kontrolparametresi, [FromQuery] string baslangicTarihi, [FromQuery] string bitisTarihi)
        {
            var value = await _getValueRepository.GetValueByMalzemeAciklamasiWParametreKoduAsync(malzemeaciklamasi,  kontrolparametresi, baslangicTarihi, bitisTarihi);
            return Ok(value);
        }
        [HttpGet("hm")]
        public async Task<IActionResult> GetHmpatamaKodları([FromQuery] string malzemeaciklamasi, [FromQuery] string kontrolparametresi, [FromQuery] string baslangicTarihi, [FromQuery] string bitisTarihi)
        {
            var value = await _getValueRepository.GetValueByMalzemeAciklamasiWParametreKoduHammaddeAsync(malzemeaciklamasi, kontrolparametresi, baslangicTarihi, bitisTarihi);
            return Ok(value);
        }
    }
}
