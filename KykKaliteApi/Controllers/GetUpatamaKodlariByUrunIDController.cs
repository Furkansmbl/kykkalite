using KykKaliteApi.Dtos.GetUpatamaKodlariByUrunIDDtos;
using KykKaliteApi.Dtos.UrunlerDtos;
using KykKaliteApi.Repositories.CihazlarRepository;
using KykKaliteApi.Repositories.GetUpatamaKodlariByUrunIDRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KykKaliteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetUpatamaKodlariByUrunIDController : ControllerBase
    {
        private readonly IGetUpatamaKodlariByUrunIDRepository _getUpatamaKodlariByUrunIDRepository ;

        public GetUpatamaKodlariByUrunIDController(IGetUpatamaKodlariByUrunIDRepository getUpatamaKodlariByUrunIDRepository )
        {
            _getUpatamaKodlariByUrunIDRepository = getUpatamaKodlariByUrunIDRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetUpatamaKodlariByUrunIDA([FromQuery]  string MalzemeAciklamasi , int FabrikId, [FromQuery] string HatAdiAciklamasi)
        {
            var value = await _getUpatamaKodlariByUrunIDRepository.GetUpatamaKodlariByUrunIDAsync(MalzemeAciklamasi, FabrikId, HatAdiAciklamasi);
            return Ok(value);
        }
        [HttpGet("Uh")]
        public async Task<IActionResult> GetUretimHatlari()
        {
            var values = await _getUpatamaKodlariByUrunIDRepository.GetUretimHatlari();
            return Ok(values);
        }
    }
}
