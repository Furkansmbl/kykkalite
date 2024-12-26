using KykKaliteApi.Repositories.GetRaporRepository;
using KykKaliteApi.Repositories.GetValueByMalzemeAciklamasiWParametreKodu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetRaporController : ControllerBase
    {
        private readonly IGetRaporRepository _getRaporRepository  ;

        public GetRaporController(IGetRaporRepository getRaporRepository )
        {
            _getRaporRepository = getRaporRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetRaporDtosAsync([FromQuery] int FabrikaId, [FromQuery] string malzemeAciklamasi, [FromQuery] string OlusturmaTarihi,[FromQuery] string BitisTarihi)
        {
            var value = await _getRaporRepository.GetRaporDtosAsync(FabrikaId, malzemeAciklamasi, OlusturmaTarihi, BitisTarihi);
            return Ok(value);
        }
        [HttpGet("hm")]
        public async Task<IActionResult> GetRaporHammaddeDtosAsync([FromQuery] int FabrikaId, [FromQuery] string malzemeAciklamasi, [FromQuery] string OlusturmaTarihi, [FromQuery] string BitisTarihi, [FromQuery] string UNVANI)
        {
            var value = await _getRaporRepository.GetRaporHammaddeDtosAsync(FabrikaId, malzemeAciklamasi, OlusturmaTarihi, BitisTarihi, UNVANI);
            return Ok(value);
        }
        [HttpGet("not")]
        public async Task<IActionResult> GetRaporDtosNotKontrolAsync([FromQuery] int FabrikaId, [FromQuery] string malzemeAciklamasi, [FromQuery] string OlusturmaTarihi, [FromQuery] string BitisTarihi,  [FromQuery] string amirOnayDurumu)
        {
            var value = await _getRaporRepository.GetRaporDtosNotKontrolAsync(FabrikaId, malzemeAciklamasi, OlusturmaTarihi, BitisTarihi, amirOnayDurumu);
            return Ok(value);
        }
    }
}
